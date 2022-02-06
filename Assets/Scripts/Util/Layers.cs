using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Layers
{
    public static readonly string Wall = "Wall";
    public static readonly string Floor = "Floor";
    public static readonly string Item = "Item";
    public static readonly string Player = "Player";
    public static readonly string Enemy = "Enemy";
    public static readonly string Projectile = "Projectile";
    public static readonly string ItemSolid = "ItemSolid";
    public static readonly string Blade = "Blade";

    public static readonly LayerMask TerrainLayers = LayerMask.GetMask(Wall, Floor);

    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public static int GetLayer(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }
}
