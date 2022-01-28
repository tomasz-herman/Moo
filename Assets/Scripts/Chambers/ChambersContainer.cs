using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChambersContainer", menuName = "ScriptableObjects/ChambersContainer")]
public class ChambersContainer : ScriptableObject
{
    public List<ChamberInfo> Chambers;
}

[System.Serializable]
public struct ChamberInfo
{
    public GameObject chamber;
    public ChamberType type;
}