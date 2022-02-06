using UnityEngine;

public abstract class UpgradeView
{
    private readonly string _name;
    private readonly string _description;
    private readonly Sprite _sprite;
    public UpgradeType upgradeType { get; protected set; }

    protected UpgradeView(string name, string description, Sprite sprite, UpgradeType upgradeType)
    {
        this._name = name;
        this._description = description;
        this._sprite = sprite;
        this.upgradeType = upgradeType;
    }

    public string GetName() { return _name; }
    public string GetDescription() { return _description; }
    public Sprite GetSprite() { return _sprite; }

    public abstract UpgradeType CommitUpdate();
}
