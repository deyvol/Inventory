using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character", order = 0)]
public class Character : ScriptableObject
{
    [Header("Base info")]
    public int characterId;
    public string charName;
    public Sprite portrait;

    [Header("State")]
    public bool isDead;
    public bool isPlayable;         //Can be used by player
    public bool isCurrentActive;    //Is current active

    [Header("ScriptableObjects Items")]
    public Item item1;
    public Item item2;
    public Item item3;
    
}
