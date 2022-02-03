using UnityEngine;

public abstract class UpgradeView
{
    //i think there should be field  upgrade type
    private string name;
    private string description;
    private Sprite sprite;

    protected UpgradeView(string name, string description, Sprite sprite)
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
