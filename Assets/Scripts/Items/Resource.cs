using UnityEngine;

public interface IResource
{
    float health { get; set; }
    float price { get; set; }
    float priceDamage { get; set; }
    float healthDamage { get; set; }
}


public class Resource : MonoBehaviour, IResource, IItem
{
    public int id { get; set; }
    public int typeId { get; set; }
    public string itemName { get; set; }
    public float weigth { get; set; }
    public float price { get; set; }
    public float health { get; set; }
    public float priceDamage { get; set; }
    public float healthDamage { get; set; }

    public SOResource data;

    public void InitResource()
    {
        typeId = data.typeId;
        itemName = data.itemName;
        weigth = data.weight;
        price = data.price;
        health = data.health;
        priceDamage = data.priceDamage;
        healthDamage = data.healthDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (InventoryManager.Instance.GetMaxWeight() >= InventoryManager.Instance.totalWeight + weigth)
            {
                InventoryManager.Instance.onAddResource?.Invoke(this);
                Destroy(this.gameObject);
            }
        }
    }
}
