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
    private int isShotGun = 4;
    private int isSword = 5;


    public void InitWeapon()
    {
        typeId = data.typeId;
        itemName = data.itemName;
        weigth = data.weight;
        price = data.price;
        dps = data.dps;
        resourceItemType = data.resourceItemType;
    }

    //Get a gun and put on inventory
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

    //Returns false or true if you have a Weapon
    public bool HasWeapon(int typeId)
    {
        if (typeId == isShotGun)
        {
            return GameManager.Instance.shotGun.activeInHierarchy;
        }
        if (typeId == isSword)
        {
            return GameManager.Instance.sword.activeInHierarchy;
        }
        return false;
    }

    //Show the gun on the player visual
    public void EnableWeapon(int typeId, bool state)
    {
        if (typeId == isShotGun)
        {
            GameManager.Instance.shotGun.SetActive(state);
        }
        if (typeId == isSword)
        {
            GameManager.Instance.sword.SetActive(state);
        }
    }
}
