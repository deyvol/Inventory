using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/New Consumable", order = 0)]
public class SOConsumable : ScriptableObject
{
    [Header("Description")]
    public int typeId;
    public string itemName;
    public string textDescription;
    public Sprite portrait;
    public Sprite portraitTrash;

    [Header("Properties")]
    public int weight;
    public float initialHealth;
    public float healthDamage;
}