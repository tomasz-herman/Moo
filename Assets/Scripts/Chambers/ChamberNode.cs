using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ChamberNode
{
    public ChamberType Type;
    public Vector2Int Location;
    public ChamberNode Parent = null;
    public Direction ParentDirection;
    public ChamberControl ChamberControl;
    public bool IsLast = false;
    private Dictionary<Direction, ChamberNode> children;

    public ChamberNode(ChamberType type, Vector2Int location)
    {
        Type = type;
        Location = location;
        children = new Dictionary<Direction, ChamberNode>();
        foreach (Direction item in Enumerable.Range(0, 4))
            children.Add(item, null);
    }

    public void AddParent(ChamberNode parent)
    {
        Parent = parent;
        ParentDirection = DirectionFromVector2Int(parent.Location - Location);
        if (children.Count == 4)
            children.Remove(ParentDirection);
        else
        {
            children.Clear();
            foreach (Direction item in Enumerable.Range(0, 4))
                if (item == ParentDirection)
                    continue;
                else
                    children.Add(item, null);
        }
    }

    public ChamberNode GetChildFromDirection(Direction direction)
    {
        if (children.ContainsKey(direction))
            return children[direction];
        return null;
    }

    public ChamberNode GetChildFromDirection(Vector2Int vector)
    {
        Direction direction = ChamberNode.DirectionFromVector2Int(vector);
        if (children.ContainsKey(direction))
            return children[direction];
        return null;
    }

    public void AddChild(ChamberNode child)
    {
        children[DirectionFromVector2Int(child.Location - Location)] = child;
        child.AddParent(this);
    }

    public ChamberNode CreateChild(ChamberType type, Direction direction)
    {
        children[direction] = new ChamberNode(type, Location + Vector2IntFromDirection(direction));
        children[direction].AddParent(this);
        return children[direction];
    }

    public IEnumerable<ChamberNode> Children()
    {
        foreach (var item in children)
        {
            yield return item.Value;
        }
    }

    public IEnumerable<KeyValuePair<Direction, ChamberNode>> ChildrenWithDirections()
    {
        foreach (var item in children)
        {
            yield return item;
        }
    }

    public ChamberNode GetNextNodeFromDirecion(Direction direction)
    {
        if (children.ContainsKey(direction))
            return children[direction];
        else if (direction == ParentDirection)
            return Parent;
        return null;
    }

    public ChamberNode GetNextNodeFromDirecion(Vector2Int vector)
    {
        Direction direction = ChamberNode.DirectionFromVector2Int(vector);
        return GetNextNodeFromDirecion(direction);
    }

    private static Direction DirectionFromVector2Int(Vector2Int vector)
    {
        if (vector.x == 0 && vector.y == -1)
            return Direction.Up;
        else if (vector.x == 0 && vector.y == 1)
            return Direction.Down;
        else if (vector.x == 1 && vector.y == 0)
            return Direction.Left;
        return Direction.Right;
    }

    private static Vector2Int Vector2IntFromDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector2Int(0, -1);
            case Direction.Down:
                return new Vector2Int(0, 1);
            case Direction.Left:
                return new Vector2Int(1, 0);
            case Direction.Right:
                return new Vector2Int(-1, 0);
            default:
                return new Vector2Int(0, 0);
        }
    }

    public void SetColors()
    {
        foreach (var item in ChildrenWithDirections())
        {
            if(item.Value!=null)
            {
                if (item.Value.Type != ChamberType.Optional)
                    ChamberControl.symbol.materials[item.Key] = PathMatirials.GetMaterialFromType(PathTypes.Main);
                else
                    ChamberControl.symbol.materials[item.Key] = PathMatirials.GetMaterialFromType(PathTypes.Optional);
            }
            else
                ChamberControl.symbol.materials[item.Key] = PathMatirials.GetMaterialFromType(PathTypes.None);
        }

        if(Parent!=null)
        {
            ChamberControl.symbol.materials[ParentDirection] = PathMatirials.GetMaterialFromType(PathTypes.Main);
        }

        ChamberControl.SetNonActivePathsColors();
    }

    // TODO: Delete
    public void CreateBlocades()
    {
        foreach (var item in children)
        {
            if (item.Value == null)
            {
                var blocade = GameObject.CreatePrimitive(PrimitiveType.Cube);
                blocade.transform.localScale = new Vector3(6, 6, 6);
                blocade.transform.position = new Vector3(Location.x * 60 + 30 + Vector2IntFromDirection(item.Key).x * 27, 0, Location.y * 60 + 30 + Vector2IntFromDirection(item.Key).y * 27);
            }
        }
    }
}
