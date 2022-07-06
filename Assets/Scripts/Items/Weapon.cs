using UnityEngine;

public interface IWeapon
{
    public float price { get; set; }
    public float damagePerSecond { get; set; }
}


public class Weapon : MonoBehaviour, IWeapon, IItem
{
    public int typeId { get; set; }
    public string itemName { get; set; }
    public float weigth { get; set; }
    public float price { get; set; }
    public float damagePerSecond { get; set; }

    public SOWeapon data;

}
