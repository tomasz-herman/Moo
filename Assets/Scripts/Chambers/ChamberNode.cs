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
    public int Level = 1;
    public bool IsLast = false;
    private Dictionary<Direction, ChamberNode> children;
    private bool isCleared = false;
    private bool isPathCleared = false;

    public ChamberNode(ChamberType type, Vector2Int location)
    {
        Type = type;
        if (Type == ChamberType.Start)
        {
            isCleared = true;
            isPathCleared = true;
        }
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
        isCleared = true;
        ClearedPathSet(this);
    }

    public void ActivateClearedHandler()
    {
        ChamberControl.AddDoorsOpenListener(ChamberClearedHandler);
    }

    public void ActivateNode(ChamberControl control)
    {
        ChamberControl = control;
        ChamberControl.node = this;
        CreateBlocades();
        SetColors();
        ActivateClearedHandler();
    }

    private void ClearedPathSet(ChamberNode root)
    {
        if (!root.isCleared)
            return;
        if (root.isPathCleared)
            return;
        if (root.Type != ChamberType.Optional)
        {
            ClearedPathToBoss(root);
            return;
        }
        ClearedPathToParent(root);
    }


    private void ClearedPathToBoss(ChamberNode root)
    {
        if (root.Parent.isPathCleared)
        {
            root.ChamberControl.SetClearedPathColor(root.ParentDirection);
            root.ChamberControl.SetDefaultPathsColors();
            root.Parent.ChamberControl.SetClearedPathColor(Directions.GetOpposite(root.ParentDirection));
            root.Parent.ChamberControl.SetDefaultPathsColors();
        }

        root.isPathCleared = root.IsChamberAndParentCleared();
        if (!root.isPathCleared)
            return;

        foreach (var item in root.Children())
            if (item.Type != ChamberType.Optional)
            {
                item.ChamberControl.SetClearedPathColor(item.ParentDirection);
                if (item.isCleared)
                    item.ChamberControl.SetDefaultPathsColors();

                // Path to boss
                if (item.isCleared)
                {
                    item.Parent.ChamberControl.SetClearedPathColor(Directions.GetOpposite(item.ParentDirection));
                    item.Parent.ChamberControl.SetDefaultPathsColors();
                }

                ClearedPathSet(item);
            }
    }

    private void ClearedPathToParent(ChamberNode root)
    {
        root.isPathCleared = root.IsChildrenCleared();

        if (!root.isPathCleared)
            return;

        root.Parent.ChamberControl.SetClearedPathColor(Directions.GetOpposite(root.ParentDirection));
        if (root.Parent.isCleared)
            root.Parent.ChamberControl.SetDefaultPathsColors();

        // Path to boss
        root.ChamberControl.SetClearedPathColor(root.ParentDirection);
        if (root.isCleared)
            root.ChamberControl.SetDefaultPathsColors();

        ClearedPathSet(root.Parent);
    }

    private bool IsChildrenCleared()
    {
        foreach (var item in Children())
            if (!item.isPathCleared)
                return false;
        return true;
    }

    private bool IsChamberAndParentCleared()
    {
        foreach (var item in Children())
            if (item.Type == ChamberType.Optional)
                if (!item.isPathCleared)
                    return false;
        if (!Parent.isPathCleared)
            return false;
        return true;
    }

    //public static void ShowChambers(ChamberNode root, int depth, ChamberNode from = null)
    //{
    //    if (depth >= 0 && !root.ChamberControl.gameObject.activeInHierarchy)
    //        root.SetActive(true);
    //    else if (depth < 0 && root.ChamberControl.gameObject.activeInHierarchy)
    //        root.SetActive(false);
    //    else if (depth < 0 && !root.ChamberControl.gameObject.activeInHierarchy)
    //        return;

    //    foreach (var item in root.Children())
    //        if (item != from)
    //            ShowChambers(item, depth - 1, root);

    //    if (root.Parent != null)
    //        if (root.Parent != from)
    //            ShowChambers(root.Parent, depth - 1, root);
    //}

    //private void SetActive(bool isActive)
    //{
    //    ChamberControl.gameObject.SetActive(isActive);
    //}
}
