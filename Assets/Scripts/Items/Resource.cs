using UnityEngine;

public interface IResource
{
    public float price { get; set; }
    public float health { get; set; }
    public float healthDamage { get; set; }
}


public class Resource : MonoBehaviour, IResource, IItem
{
    public int typeId { get; set; }
    public string itemName { get; set; }
    public float weigth { get; set; }
    public float price { get; set; }
    public float health { get; set; }
    public float healthDamage { get; set; }
    public float priceDamage { get; set; }

    public SOResource data;

    public void InitResource()
    {
        typeId = data.typeId;
        itemName = data.itemName;
        weigth = data.weight;
        health = data.initialHealth;
        healthDamage = data.healthDamage;
        priceDamage = data.priceDamage;
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
