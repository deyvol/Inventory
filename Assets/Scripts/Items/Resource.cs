using UnityEngine;

public interface IResource
{
    public float price { get; set; }
    public float health { get; set; }
    public bool isTrash { get; set; }
    public float healthDamage { get; set; }
}


public class Recource : MonoBehaviour, IResource, IItem
{
    public int typeId { get; set; }
    public string itemName { get; set; }
    public float weigth { get; set; }
    public float price { get; set; }
    public float health { get; set; }
    public bool isTrash { get; set; }
    public float healthDamage { get; set; }

    public SOResource data;

}
