using UnityEngine;

public interface IConsumable
{
    public float health { get; set; }
    public bool isTrash { get; set; }
    public float healthDamage { get; set; }
    public void Use();
}


public class Consumable : MonoBehaviour, IConsumable, IItem
{
    public int typeId { get; set; }
    public string itemName { get; set; }
    public float weigth { get; set; }
    public float health { get; set; }
    public bool isTrash { get; set; }
    public float healthDamage { get; set; }

    public SOConsumable type;

    public void InitConsumable()
    {
        typeId = type.typeId;
        itemName = type.itemName;
        weigth = type.weight;
        health = type.initialHealth;
        isTrash = false;
        healthDamage = type.healthDamage;
    }

    public void Use()
    {
        if (!isTrash)
        {

        }
    }

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