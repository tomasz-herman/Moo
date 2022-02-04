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
    public int Number = -1;
    public bool IsLast = false;
    private Dictionary<Direction, ChamberNode> children;

    public ChamberNode(ChamberType type, Vector2Int location)
    {
        Type = type;
        Location = location;
        children = new Dictionary<Direction, ChamberNode>();
        foreach (Direction item in Enum.GetValues(typeof(Direction)))
            children.Add(item, null);
    }

    public void AddParent(ChamberNode parent)
    {
        Parent = parent;
        ParentDirection = Directions.DirectionFromVector2Int(parent.Location - Location);
        if (children.Count == 4)
            children.Remove(ParentDirection);
        else
        {
            foreach (Direction item in Enum.GetValues(typeof(Direction)))
                if (item == ParentDirection)
                {
                    if (children.ContainsKey(ParentDirection))
                        children.Remove(ParentDirection);
                }
                else if (!children.ContainsKey(item))
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
        Direction direction = Directions.DirectionFromVector2Int(vector);
        return GetChildFromDirection(direction);
    }

    public ChamberNode CreateChild(ChamberType type, Direction direction)
    {
        children[direction] = new ChamberNode(type, Location + Directions.Vector2IntFromDirection(direction));
        children[direction].AddParent(this);
        return children[direction];
    }

    public IEnumerable<ChamberNode> Children()
    {
        foreach (var item in children)
        {
            if (item.Value == null)
                continue;
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
        Direction direction = Directions.DirectionFromVector2Int(vector);
        return GetNextNodeFromDirecion(direction);
    }

    public void SetColors()
    {
        foreach (var item in ChildrenWithDirections())
        {
            if (item.Value != null)
            {
                if (item.Value.Type != ChamberType.Optional)
                {
                    ChamberControl.symbol.SetBossDirection(item.Key);
                    ChamberControl.symbol.materials[item.Key] = PathMaterials.GetMaterialFromType(PathTypes.Main);
                }
                else
                    ChamberControl.symbol.materials[item.Key] = PathMaterials.GetMaterialFromType(PathTypes.Optional);
            }
            else
            {
                ChamberControl.symbol.materials[item.Key] = PathMaterials.GetMaterialFromType(PathTypes.None);
                //ChamberControl.symbol.SetActive(item.Key, false); //path symbol visibility when there is no path in this direction
            }
        }

        if (Parent != null)
        {
            if (Type == ChamberType.Optional)
                ChamberControl.symbol.SetBossDirection(ParentDirection);
            ChamberControl.symbol.materials[ParentDirection] = PathMaterials.GetMaterialFromType(PathTypes.Main);
        }

        ChamberControl.SetNonActivePathsColors();
    }

    public void CreateBlocades()
    {
        foreach (var item in children)
            ChamberControl.SetBlocadeActive(item.Key, item.Value == null);
        if (Parent != null)
            ChamberControl.SetBlocadeActive(ParentDirection, false);

    }
    private void ChamberClearedHandler()
    {
        Parent.ChamberControl.SetClearedPathColor(Directions.GetOpposite(ParentDirection));
        Parent.ChamberControl.SetDefaultPathsColors();
        ChamberControl.SetClearedPathColor(ParentDirection);
    }

    public void ActivateClearedHandler()
    {
        ChamberControl.AddAllEnemiesKilledListener(ChamberClearedHandler);
    }

    public void ActivateNode(ChamberControl control)
    {
        ChamberControl = control;
        ChamberControl.node = this;
        CreateBlocades();
        SetColors();
        ActivateClearedHandler();
    }
}
