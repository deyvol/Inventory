using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/New Weapon", order = 0)]
public class SOWeapon : ScriptableObject
{
    [Header("Description")]
    public int typeId;
    public string itemName;
    public string textDescription;
    public Sprite portrait;

    [Header("Properties")]
    public int resourceItemType;
    public int weight;
    public int price;
    public int dps;
}
