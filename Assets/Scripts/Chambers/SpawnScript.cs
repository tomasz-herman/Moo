using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] public float ChamberSize = 60;
    [SerializeField] int NumberOfOptionalChambers = 10;
    [SerializeField] int NumberOfBossChambers = 3;
    [SerializeField] int NumbersOfChambersBeforeBoss = 2;
    [SerializeField] int NumberOfTryBeforeOnlyForwardMode = 3;

    private List<GameObject> StartChambers = new List<GameObject>();
    private List<GameObject> NormalChambers = new List<GameObject>();
    private List<GameObject> OptionalChambers = new List<GameObject>();
    private List<GameObject> BossChambers = new List<GameObject>();
    [HideInInspector] public ChamberNode ChamersTreeRoot { get { return chambersTreeRoot; } }

    private Dictionary<Vector2Int, ChamberType> taken = new Dictionary<Vector2Int, ChamberType>();
    private int remainig;
    private bool OnlyForward = false;
    private ChamberNode chambersTreeRoot;
    private List<(ChamberNode node, Direction direction)> possibleOptional = new List<(ChamberNode node, Direction direction)>();

    private void Awake()
    {
        LoadChamberPrefabs();
    }

    public ChamberNode GenerateTree()
    {
        int i = 0;
        while (!GenerateChamberTree())
        {
            i++;
            if (i > NumberOfTryBeforeOnlyForwardMode)
                OnlyForward = true;
        }
        return chambersTreeRoot;
    }

    private bool GenerateChamberTree()
    {
        try
        {
            chambersTreeRoot = null;
            possibleOptional = new List<(ChamberNode node, Direction direction)>();
            taken = new Dictionary<Vector2Int, ChamberType>();

            chambersTreeRoot = new ChamberNode(ChamberType.Start, new Vector2Int(0, 0));
            taken.Add(chambersTreeRoot.Location, ChamberType.Start);
            ChamberNode currentNode = chambersTreeRoot;
            ChamberNode tempNode = null;
            Direction nextChamberDirection;

            for (int i = NumberOfBossChambers, j = NumbersOfChambersBeforeBoss; i > 0 || j > 0;)
            {
                nextChamberDirection = randomNext(currentNode);
                if (j <= 0)
                {
                    tempNode = currentNode.CreateChild(ChamberType.Boss, nextChamberDirection);
                    taken.Add(tempNode.Location, ChamberType.Boss);
                    i--;
                    if (i > 0)
                        j = NumbersOfChambersBeforeBoss;
                }
                else
                {
                    tempNode = currentNode.CreateChild(ChamberType.Normal, nextChamberDirection);
                    taken.Add(tempNode.Location, ChamberType.Normal);
                    j--;
                }
                currentNode = tempNode;
            }

            currentNode = chambersTreeRoot;
            tempNode = null;
            while (currentNode != null)
            {
                if (currentNode.Type == ChamberType.Normal)
                    FindOptionalFromNode(currentNode);

                tempNode = null;
                foreach (var item in currentNode.Children())
                    tempNode ??= item;
                currentNode = tempNode;
            }

            remainig = NumberOfOptionalChambers;
            while (remainig > 0)
            {
                var tempOptionals = possibleOptional.OrderBy(x => Utils.RandomNumber()).ToList();

                foreach (var item in tempOptionals)
                {
                    if (remainig <= 0)
                        break;
                    if (!taken.ContainsKey(moveFromDirection(item.node.Location, item.direction)))
                        if (Random.value >= 0.5)
                        {
                            var newChamber = item.node.CreateChild(ChamberType.Optional, item.direction);
                            taken.Add(newChamber.Location, newChamber.Type);
                            remainig--;
                            FindOptionalFromNode(newChamber);
                        }
                    possibleOptional.Remove(item);
                }

                if (remainig > 0 && possibleOptional.Count == 0)
                    FindOptionalPositionsRec(chambersTreeRoot);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public void BuildChambersRec(ChamberNode root)
    {
        if (root == null)
            return;

        GameObject newRoom = null;

        switch (root.Type)
        {
            case ChamberType.Normal:
                newRoom = Instantiate(NormalChambers[Utils.NumberBetween(0, NormalChambers.Count - 1)], Vector3.zero, Quaternion.identity);
                break;
            case ChamberType.Boss:
                newRoom = Instantiate(BossChambers[Utils.NumberBetween(0, BossChambers.Count - 1)], Vector3.zero, Quaternion.identity);
                break;
            case ChamberType.Optional:
                newRoom = Instantiate(OptionalChambers[Utils.NumberBetween(0, OptionalChambers.Count - 1)], Vector3.zero, Quaternion.identity);
                break;
            case ChamberType.Start:
                int dupa = Utils.NumberBetween(0, StartChambers.Count - 1);
                newRoom = Instantiate(StartChambers[Utils.NumberBetween(0, StartChambers.Count - 1)], Vector3.zero, Quaternion.identity);
                break;
            default:
                break;
        }
        newRoom.transform.position = new Vector3(root.Location.x * ChamberSize, 0, root.Location.y * ChamberSize);
        root.ChamberControl = newRoom.GetComponent<ChamberControl>();
        root.CreateBlocades(); // TODO: Delete

        foreach (var item in root.Children())
        {
            if (item != null)
                BuildChambersRec(item);
        }

    }

    private Direction randomNext(ChamberNode current)
    {
        Direction returnvalue;
        foreach (var item in current.ChildrenWithDirections().OrderBy(x => Utils.RandomNumber()))
        {
            if (OnlyForward && item.Key == Direction.Down)
                continue;
            if (item.Value == null)
            {
                returnvalue = item.Key;
                if (taken.ContainsKey(moveFromDirection(current.Location, item.Key)))
                    continue;
                return returnvalue;
            }
        }
        return Direction.Up;
    }

    private Vector2Int moveFromDirection(Vector2Int current, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return current + new Vector2Int(0, -1);
            case Direction.Down:
                return current + new Vector2Int(0, 1);
            case Direction.Left:
                return current + new Vector2Int(-1, 0);
            case Direction.Right:
                return current + new Vector2Int(1, 0);
            default:
                return new Vector2Int(0, 0);
        }
    }

    private void FindOptionalPositionsRec(ChamberNode root)
    {
        if (root == null)
            return;

        switch (root.Type)
        {
            case ChamberType.Normal:
            case ChamberType.Optional:
                FindOptionalFromNode(root);
                break;
            default:
                break;
        }

        foreach (var item in root.Children())
        {
            if (item != null)
                FindOptionalPositionsRec(item);
        }
    }

    private void FindOptionalFromNode(ChamberNode current)
    {
        foreach (var item in current.ChildrenWithDirections())
        {
            if (item.Value == null)
            {
                if (!taken.ContainsKey(moveFromDirection(current.Location, item.Key)))
                    possibleOptional.Add((current, item.Key));
            }
        }
    }

    private void LoadChamberPrefabs()
    {
        ChambersContainer ChambersContainer = Resources.Load<ChambersContainer>("ScriptableObjects/TestChambers"); // TODO: change to finished chambers
        foreach (var item in ChambersContainer.Chambers)
        {
            switch (item.type)
            {
                case ChamberType.Normal:
                    NormalChambers.Add(item.chamber);
                    break;
                case ChamberType.Boss:
                    BossChambers.Add(item.chamber);
                    break;
                case ChamberType.Optional:
                    OptionalChambers.Add(item.chamber);
                    break;
                case ChamberType.Start:
                    StartChambers.Add(item.chamber);
                    break;
                default:
                    break;
            }
        }
    }
}
