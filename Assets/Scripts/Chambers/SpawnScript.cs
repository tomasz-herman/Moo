using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] public float ChamberSize = 60;
    [SerializeField] int NumberOfOptionalChambersBeforeBoss = 5;
    [SerializeField] int NumberOfBossChambers = 3;
    [SerializeField] int NumbersOfChambersBeforeBoss = 2;
    [SerializeField] int NumberOfTryBeforeOnlyForwardMode = 3;
    [SerializeField] float OptionalChamberSpawnPossibilityPercent = 50;

    private List<GameObject> StartChambers = new List<GameObject>();
    private List<GameObject> NormalChambers = new List<GameObject>();
    private List<GameObject> OptionalChambers = new List<GameObject>();
    private List<GameObject> BossChambers = new List<GameObject>();
    [HideInInspector] public ChamberNode ChambersTreeRoot { get { return chambersTreeRoot; } }

    private Dictionary<Vector2Int, ChamberType> taken = new Dictionary<Vector2Int, ChamberType>();
    private int remaining;
    private bool OnlyForward = false;
    private ChamberNode chambersTreeRoot;
    private List<(ChamberNode node, Direction direction)> possibleOptional = new List<(ChamberNode node, Direction direction)>();
    private float optionalSpawnFloat = 0;

    private void Awake()
    {
        optionalSpawnFloat = 1 - (float)OptionalChamberSpawnPossibilityPercent / 100;
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
        NumberTheChambers();
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
                    else
                        tempNode.IsLast = true;
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
            remaining = 0;
            while (currentNode != null)
            {
                if (remaining <= 0)
                {
                    if (currentNode.Type == ChamberType.Start || currentNode.Type == ChamberType.Boss)
                        remaining = NumberOfOptionalChambersBeforeBoss;
                    tempNode = null;
                    foreach (var item in currentNode.Children())
                        if (item.Type != ChamberType.Optional)
                            tempNode = item;
                    currentNode = tempNode;

                    if (remaining > 0 && currentNode != null)
                    {
                        possibleOptional.Clear();
                        FindOptionalPositionsRec(currentNode);
                    }
                }
                else
                {
                    var tempOptionals = possibleOptional.OrderBy(x => Utils.RandomNumber()).ToList();

                    foreach (var item in tempOptionals)
                    {
                        if (remaining <= 0)
                            break;
                        if (!taken.ContainsKey(moveFromDirection(item.node.Location, item.direction)))
                            if (Random.value >= optionalSpawnFloat)
                            {
                                var newChamber = item.node.CreateChild(ChamberType.Optional, item.direction);
                                taken.Add(newChamber.Location, newChamber.Type);
                                remaining--;
                                FindOptionalFromNode(newChamber);
                            }
                        possibleOptional.Remove(item);
                    }

                    if (remaining > 0 && possibleOptional.Count == 0)
                    {
                        FindOptionalPositionsRec(currentNode);
                        if (possibleOptional.Count <= 0)
                            throw new System.Exception("Infinite Loop"); // Highly improbable but this happened to me once so this stays :)
                    }
                }
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
                newRoom = Instantiate(StartChambers[Utils.NumberBetween(0, StartChambers.Count - 1)], Vector3.zero, Quaternion.identity);
                break;
            default:
                break;
        }
        newRoom.transform.position = new Vector3(root.Location.x * ChamberSize, 0, root.Location.y * ChamberSize);
        root.ActivateNode(newRoom.GetComponent<ChamberControl>());

        foreach (var item in root.Children())
            BuildChambersRec(item);
    }

    private void NumberTheChambers()
    {
        int currNumber = 0;
        ChamberNode currentNode = chambersTreeRoot;
        ChamberNode tempNode;
        while (currentNode != null)
        {
            if (currentNode.Type == ChamberType.Normal)
                NumberTheOptionalChambersRec(currentNode);
            else
            {
                currentNode.Number = currNumber;
                currNumber++;
            }

            tempNode = null;
            foreach (var item in currentNode.Children())
            {
                if (item.Type == ChamberType.Boss || item.Type == ChamberType.Normal)
                    tempNode = item;
            }
            currentNode = tempNode;
        }
        EnemySpawner.MaxNumber = currNumber - 1;

        void NumberTheOptionalChambersRec(ChamberNode root)
        {
            if (root == null)
                return;

            root.Number = currNumber;
            currNumber++;
            foreach (var item in root.Children())
            {
                if (item.Type == ChamberType.Optional)
                    NumberTheOptionalChambersRec(item);
            }
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
        return current + Directions.Vector2IntFromDirection(direction);
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
            case ChamberType.Boss:
                return;
            default:
                break;
        }

        foreach (var item in root.Children())
            FindOptionalPositionsRec(item);
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
        ChambersContainer ChambersContainer = Resources.Load<ChambersContainer>("ScriptableObjects/Chambers");
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
