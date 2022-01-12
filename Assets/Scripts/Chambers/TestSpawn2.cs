using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestSpawn2 : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] List<GameObject> StartChambers;
    [SerializeField] List<GameObject> NormalChambers;
    [SerializeField] List<GameObject> OptionalChambers;
    [SerializeField] List<GameObject> BossChambers;
    [SerializeField] float ChamberSize = 60;
    [SerializeField] int NumberOfOptionalChambers = 10;
    private Dictionary<Vector2Int, ChamberType> taken = new Dictionary<Vector2Int, ChamberType>();
    private int remainig = int.MaxValue;

    private ChamberNode chamberTreeHead;
    private List<(ChamberNode node, Direction direction)> possibleOptional = new List<(ChamberNode node, Direction direction)>();

    // TODO: jak siê lista optionals skoñczy a nie wstawiliœmy jeszcze wszystkich stworzyæ od nowa
    void Start()
    {
        Player.transform.position = new Vector3(10, 10, 10);
        GenerateChamberTree();
        BuildChambersRec(chamberTreeHead);
    }

    private Vector2Int randomNext(Vector2Int current, bool isMainPath)
    {
        Vector2Int returnvalue;
        foreach (int index in GetRandomOrder(4))
        {
            returnvalue = MoveFromeIndex(current, index);
            if (taken.ContainsKey(returnvalue))
                continue;
            return returnvalue;
        }
        return new Vector2Int(0, 0);
    }

    private int[] GetRandomOrder(int numberOfElements)
    {
        int[] ind = Enumerable.Range(0, numberOfElements).ToArray();

        int[] RandomInd = ind.OrderBy(x => Utils.RandomNumber()).ToArray();
        return RandomInd;
    }

    private Vector2Int MoveFromeIndex(Vector2Int current, int index)
    {
        switch (index)
        {
            case 0:
                return current + new Vector2Int(0, 1);
            case 1:
                return current + new Vector2Int(1, 0);
            case 2:
                return current + new Vector2Int(0, -1);
            case 3:
                return current + new Vector2Int(-1, 0);
            default:
                break;
        }
        return new Vector2Int(0, 0);
    }

    private void GenerateChamberTree()
    {
        Vector2Int current = new Vector2Int(0, 0);
        taken.Add(current, ChamberType.Start);
        chamberTreeHead = new ChamberNode(ChamberType.Start, current);
        ChamberNode currentNode = chamberTreeHead;
        ChamberNode tempNode = null;
        for (int i = 1; i < 10; i++)
        {
            current = randomNext(current, true);
            if (i % 3 == 0)
            {
                taken.Add(current, ChamberType.Boss);
                tempNode = new ChamberNode(ChamberType.Boss, current);
                currentNode.AddChild(tempNode);
            }
            else
            {
                taken.Add(current, ChamberType.Normal);
                tempNode = new ChamberNode(ChamberType.Normal, current);
                currentNode.AddChild(tempNode);
            }
            currentNode = tempNode;
        }

        ChamberNode chamberNode = chamberTreeHead;
        ChamberNode chamberNext = null;
        while (chamberNode != null)
        {
            if (chamberNode.Type == ChamberType.Normal)
            {
                Vector2Int nextChamber;
                for (int i = 0; i < 4; i++)
                {
                    nextChamber = MoveFromeIndex(chamberNode.Location, i);
                    if (!taken.ContainsKey(nextChamber))
                        possibleOptional.Add((chamberNode, ChamberNode.DirectionFromVector2Int(nextChamber - chamberNode.Location)));
                }
            }
            chamberNext = null;
            foreach (var item in chamberNode.Children())
                chamberNext ??= item;
            chamberNode = chamberNext;
        }

        remainig = NumberOfOptionalChambers;
        while (remainig > 0)
        {
            var tempOptionals = possibleOptional.OrderBy(x => Utils.RandomNumber()).ToList();

            foreach (var item in tempOptionals)
            {
                if (remainig <= 0)
                    break;
                if (!taken.ContainsKey(item.node.Location + ChamberNode.Vector2IntFromDirection(item.direction)))
                    if (Random.value >= 0.5)
                    {
                        var newChamber = item.node.CreateChild(ChamberType.Optional, item.direction);
                        taken.Add(newChamber.Location, newChamber.Type);
                        remainig--;
                        Vector2Int nextChamber;
                        for (int i = 0; i < 4; i++)
                        {
                            nextChamber = MoveFromeIndex(newChamber.Location, i);
                            if (!taken.ContainsKey(nextChamber))
                                possibleOptional.Add((newChamber, ChamberNode.DirectionFromVector2Int(nextChamber - newChamber.Location)));
                        }
                    }
                possibleOptional.Remove(item);
            }

            if (remainig > 0 && possibleOptional.Count == 0)
                FindOptionalPositionsRec(chamberTreeHead);
        }
    }

    private void BuildChambersRec(ChamberNode root)
    {
        if (root == null)
            return;

        GameObject newRoom = null;

        switch (root.Type)
        {
            case ChamberType.Normal:
                newRoom = Instantiate(NormalChambers[Utils.NumberBetween(0, NormalChambers.Count-1)], Vector3.zero, Quaternion.identity);
                break;
            case ChamberType.Boss:
                newRoom = Instantiate(BossChambers[Utils.NumberBetween(0, BossChambers.Count-1)], Vector3.zero, Quaternion.identity);
                break;
            case ChamberType.Optional:
                newRoom = Instantiate(OptionalChambers[Utils.NumberBetween(0, OptionalChambers.Count-1)], Vector3.zero, Quaternion.identity);
                break;
            case ChamberType.Start:
                newRoom = Instantiate(StartChambers[Utils.NumberBetween(0, StartChambers.Count-1)], Vector3.zero, Quaternion.identity);
                break;
            default:
                break;
        }
        newRoom.transform.position = new Vector3(root.Location.x * ChamberSize, 0, root.Location.y * ChamberSize);
        root.CreateBlocades(); // TODO: Delete

        foreach (var item in root.Children())
        {
            if (item != null)
                BuildChambersRec(item);
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
                {
                    Vector2Int nextChamber;
                    for (int i = 0; i < 4; i++)
                    {
                        nextChamber = MoveFromeIndex(root.Location, i);
                        if (!taken.ContainsKey(nextChamber))
                            possibleOptional.Add((root, ChamberNode.DirectionFromVector2Int(nextChamber - root.Location)));
                    }
                }
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

}
