using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSymbolControler : MonoBehaviour
{
    private Dictionary<Direction, Renderer> elements = new Dictionary<Direction, Renderer>();
    public Dictionary<Direction, Material> materials = new Dictionary<Direction, Material>();

    private void Awake()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform t = gameObject.transform.GetChild(i);
            if (t.localPosition.x == 0.0f && t.localPosition.z > 0.0f)
                elements.Add(Direction.Down, t.gameObject.GetComponent<Renderer>());
            if (t.localPosition.x == 0.0f && t.localPosition.z < 0.0f)
                elements.Add(Direction.Up, t.gameObject.GetComponent<Renderer>());
            if (t.localPosition.x > 0.0f && t.localPosition.z == 0.0f)
                elements.Add(Direction.Left, t.gameObject.GetComponent<Renderer>());
            if (t.localPosition.x < 0.0f && t.localPosition.z == 0.0f)
                elements.Add(Direction.Right, t.gameObject.GetComponent<Renderer>());
        }
    }

    public void SetDefaultMaterials()
    {
        foreach (var item in materials)
        {
            elements[item.Key].material = item.Value;
        }
    }

    public void SetMaterials(Material material)
    {
        foreach (var item in elements)
        {
            item.Value.material = material;
        }
    }
}
