using UnityEngine;

public interface IConsumable
{
    float health { get; set; }
    bool isTrash { get; set; }
    float healthDamage { get; set; }
}


public class Consumable : MonoBehaviour, IConsumable, IItem
{
    public int id { get; set; }
    public int typeId { get; set; }
    public string itemName { get; set; }
    public float weigth { get; set; }
    public float health { get; set; }
    public bool isTrash { get; set; }
    public float healthDamage { get; set; }

    public SOConsumable data;

    public void InitConsumable()
    {
        typeId = data.typeId;
        itemName = data.itemName;
        weigth = data.weight;
        health = data.initialHealth;
        isTrash = false;
        healthDamage = data.healthDamage;
    }
        
    //Get a consumable and puts on inventory
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            if (InventoryManager.Instance.GetMaxWeight() >= InventoryManager.Instance.totalWeight + weigth)
            {
                InventoryManager.Instance.onAddConsumable?.Invoke(this);
                Destroy(this.gameObject);
            }
        }
    }
}