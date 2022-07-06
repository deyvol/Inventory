using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Resource", menuName = "Items/New Resource", order = 0)]
public class SOResource : ScriptableObject
{
    [Header("Description")]
    public int typeId;
    public string itemName;
    public string textDescription;
    public Sprite portrait;

    [Header("Properties")]
    public int weight;
    public int price;
    public float priceDamage;
}