using UnityEngine;

public enum ChamberType { Normal, Boss, Optional, Start }
public enum Direction { Up, Down, Left, Right }

public static class Directions
{
    public static Vector2Int Vector2IntFromDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2Int.down;
            case Direction.Down:
                return Vector2Int.up;
            case Direction.Left:
                return Vector2Int.right;
            case Direction.Right:
                return Vector2Int.left;
            default:
                return Vector2Int.down;
        }
    }

    public static Direction DirectionFromVector2Int(Vector2Int vector)
    {
        if (vector==Vector2Int.down)
            return Direction.Up;
        else if (vector == Vector2Int.up)
            return Direction.Down;
        else if (vector == Vector2Int.right)
            return Direction.Left;
        return Direction.Right;
    }
}
