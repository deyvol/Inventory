using UnityEngine;

public interface IWeapon
{
    float price { get; set; }
    int dps { get; set; }
    int resourceItemType { get; set; }
    bool HasWeapon(int typeId);
}


public class Weapon : MonoBehaviour, IWeapon, IItem
{
    public int id { get; set; }
    public int typeId { get; set; }
    public string itemName { get; set; }
    public float weigth { get; set; }
    public float price { get; set; }
    public int dps { get; set; }
    public int resourceItemType { get; set; }

    public SOWeapon data;

    private bool isProcessing = false;

    public void InitWeapon()
    {
        typeId = data.typeId;
        itemName = data.itemName;
        weigth = data.weight;
        price = data.price;
        dps = data.dps;
        resourceItemType = data.resourceItemType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isProcessing)
        {
            isProcessing = true;
            if (InventoryManager.Instance.GetMaxWeight() >= InventoryManager.Instance.totalWeight + weigth)
            {
                InventoryManager.Instance.onAddWeapon?.Invoke(this);
                Destroy(this.gameObject);
            }
        }
    }

    public bool HasWeapon(int typeId)
    {
        if (typeId == 4)
        {
            return GameManager.Instance.shotGun.activeInHierarchy;
        }
        if (typeId == 5)
        {
            return GameManager.Instance.sword.activeInHierarchy;
        }
        return false;
    }

    public void EnableWeapon(int typeId, bool state)
    {
        if (typeId == 4)
        {
            GameManager.Instance.shotGun.SetActive(state);
        }
        if (typeId == 5)
        {
            GameManager.Instance.sword.SetActive(state);
        }
    }
}
