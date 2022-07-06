using UnityEngine;

public interface IResource
{
    float price { get; set; }
    float priceDamage { get; set; }
}


public class Resource : MonoBehaviour, IResource, IItem
{
    public int id { get; set; }
    public int typeId { get; set; }
    public string itemName { get; set; }
    public float weigth { get; set; }
    public float price { get; set; }
    public float priceDamage { get; set; }

    public SOResource data;

    public void InitResource()
    {
        typeId = data.typeId;
        itemName = data.itemName;
        weigth = data.weight;
        price = data.price;
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
