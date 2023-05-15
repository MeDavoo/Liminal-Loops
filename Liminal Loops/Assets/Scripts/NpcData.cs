using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Liminal Loops/NPCData")]
public class NpcData : ScriptableObject
{
    public string npcName;
    public GameObject npcObject;
    public bool isDemon;
    public bool isDead;
    public List<CustomData> NpcRoutine = new List<CustomData>();
}

[System.Serializable]
public class CustomData
{
    public string scheduleName;
    public int Hour;
    public int Minuit;
    public int Day;
    public Location coordinates;
    public FacingDirection facingDirection;
    public float speed;
}

[System.Serializable]
public enum FacingDirection
{
    Up,
    Down,
    Left,
    Right
}

[System.Serializable]
public class Location
{
    public float x;
    public float y;
    public float z;
}








