using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeView
{
    private string name, description;
    private Sprite sprite;
    public UpgradeView(string name, string description, Sprite sprite)
    {
        this.name = name;
        this.description = description;
        this.sprite = sprite;
    }

    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public Sprite GetSprite() { return sprite; }

    public abstract UpgradeType CommitUpdate();
}
