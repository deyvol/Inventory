using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item", order = 0)]
public class Item : ScriptableObject
{
    [Header("Description")]
    public int itemId;
    public bool isLock;
    public string itemName;
    public string textDesciption;
    public Sprite portrait;

    [Header("Properties")]
    public string typeObject;
    public enum MyEnum {ONE,TWO,three};
    public bool isSaleable; 
    public bool isUsable;
    public int weight;
    public int price;
    public int health;
    public int damagePerSecond;

}
