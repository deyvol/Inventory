using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item", order = 0)]
public class Item : ScriptableObject
{
    public int itemId;
    public bool isLock;
    public string itemName;
    public string textDesciption;
    public Sprite portrait;

    [Header("Situation")]
    public int characterId;
    public int currentRoomId;
    public Vector3 currentPosition;
        
}
