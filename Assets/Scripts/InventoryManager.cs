using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    //Set the max weight you can carry
    private const float maxWeight = 10f;

    [Header("General")]
    public float totalCoins;
    public float totalWeight;
    public Text visualTotalWeight;
    public GameObject visualItemPrefab;
    public GameObject inventoryContent;

    [Header("Items Inventory")]
    [SerializeField] private List<Consumable> inventoryConsumables = new List<Consumable>();
    [SerializeField] private List<Resource> inventoryResources = new List<Resource>();
    [SerializeField] private List<Weapon> inventoryWeapons = new List<Weapon>();
    
    //Actions
    public Action<Consumable> onAddConsumable;
    public Action<Resource> onAddResource;
    public Action<Weapon> onAddWeapon;

    private float _elapsed = 0f;
    private float _interval = 1f;
    private int ids = 0;


    //Singleton
    private static InventoryManager _instance;
    public static InventoryManager Instance { get { return _instance; } }

    private void Awake()
    {
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        totalWeight = 0;
        totalCoins = 0;
    }

    public float GetMaxWeight()
    {
        return maxWeight;
    }

    //Actions
    private void OnEnable()
    {
        onAddConsumable += AddConsumable;
        onAddResource += AddResource;
        onAddWeapon += AddWeapon;
    }

    private void OnDisable()
    {
        onAddConsumable -= AddConsumable;
        onAddResource -= AddResource;
        onAddWeapon -= AddWeapon;
    }

    //CONSUMABLES
    public void AddConsumable(Consumable item)
    {
        ids++;

        //Adding Item to Memory
        Consumable newItem = new Consumable();
        newItem.data = item.data;
        newItem.InitConsumable();
        newItem.id = ids;
        SetWeightGlobal(item.weigth);
        inventoryConsumables.Add(newItem);

        //Adding Item Visual on inventory
        var obj = Instantiate(visualItemPrefab, inventoryContent.transform);
        obj.GetComponent<VisualItem>().SetImage(item.data.portrait);
        obj.GetComponent<VisualItem>().SetImageTrash(item.data.portraitTrash);
        obj.GetComponent<VisualItem>().SetTypeId(item.data.typeId);
        obj.GetComponent<VisualItem>().SetId(ids);
        obj.GetComponent<VisualItem>().SetWeight(item.data.weight);
    }

    //Delete consumable for the inventory after use
    public void DeleteConsumable(int id)
    {
        for (int i = inventoryConsumables.Count-1; i >= 0; i--)
        {
            if (inventoryConsumables[i].id == id)
            {
                SetWeightGlobal(-1 * inventoryConsumables[i].weigth);
                inventoryConsumables.RemoveAt(i);
            }
        }
    }

    public void DeleteAllConsumable(int typeId)
    {
        for (int i = inventoryConsumables.Count - 1; i >= 0; i--)
        {
            if (inventoryConsumables[i].typeId == typeId)
            {
                SetWeightGlobal(-1 * inventoryConsumables[i].weigth);
                inventoryConsumables.RemoveAt(i);
            }
        }
    }

    public Consumable GetFirstTypeConsumable(int type)
    {
        foreach (var con in inventoryConsumables)
        {
            if (con.typeId == type)
            {
                return con;
            }
        }
        return null;
    }

    public int GetCountConsumableByType(int type)
    {
        int i = 0;
        foreach (var con in inventoryConsumables)
        {
            if (con.typeId == type)
            {
                i++;
            }
        }
        return i;
    }

    public Consumable GetConsumable(int id)
    {
        foreach (var con in inventoryConsumables)
        {
            if (con.id == id)
            {
                return con;
            }
        }
        return null;
    }


    //RESOURCES
    public void AddResource(Resource item)
    {
        ids++;

        //Adding Item to Memory
        Resource newItem = new Resource();
        newItem.data = item.data;
        newItem.InitResource();
        newItem.id = ids;
        SetWeightGlobal(item.weigth);
        inventoryResources.Add(newItem);

        //Adding Item Visual on inventory
        var obj = Instantiate(visualItemPrefab, inventoryContent.transform);
        obj.GetComponent<VisualItem>().SetImage(item.data.portrait);
        obj.GetComponent<VisualItem>().SetTypeId(item.data.typeId);
        obj.GetComponent<VisualItem>().SetPrice(item.data.price);
        obj.GetComponent<VisualItem>().SetId(ids);
        obj.GetComponent<VisualItem>().SetWeight(item.data.weight);
    }

    public int GetCountResourceByType(int type)
    {
        int i = 0;
        foreach (var con in inventoryResources)
        {
            if (con.typeId == type)
            {
                i++;
            }
        }
        return i;
    }

    public Resource GetFirstTypeResource(int type)
    {
        foreach (var con in inventoryResources)
        {
            if (con.typeId == type)
            {
                return con;
            }
        }
        return null;
    }

    //Delete resource for the inventory after use
    public void DeleteResource(int id)
    {
        for (int i = inventoryResources.Count - 1; i >= 0; i--)
        {
            if (inventoryResources[i].id == id)
            {
                SetWeightGlobal(-1 * inventoryResources[i].weigth);
                inventoryResources.RemoveAt(i);
            }
        }
    }

    //WEAPONS
    public void AddWeapon(Weapon item)
    {
        ids++;

        //Adding Item to Memory
        Weapon newItem = new Weapon();
        newItem.data = item.data;
        newItem.InitWeapon();
        newItem.id = ids;
        SetWeightGlobal(item.weigth);
        inventoryWeapons.Add(newItem);

        //Adding Item Visual on inventory
        var obj = Instantiate(visualItemPrefab, inventoryContent.transform);
        obj.GetComponent<VisualItem>().SetImage(item.data.portrait);
        obj.GetComponent<VisualItem>().SetTypeId(item.data.typeId);
        obj.GetComponent<VisualItem>().SetPrice(item.data.price);
        obj.GetComponent<VisualItem>().SetId(ids);
        obj.GetComponent<VisualItem>().SetWeight(item.data.weight);
        obj.GetComponent<VisualItem>().SetDPS(item.data.dps);
        newItem.EnableWeapon(item.typeId, true);
    }

    public int GetCountWeaponByType(int type)
    {
        int i = 0;
        foreach (var con in inventoryWeapons)
        {
            if (con.typeId == type)
            {
                i++;
            }
        }
        return i;
    }

    public Weapon GetFirstTypeWeapon(int type)
    {
        foreach (var con in inventoryWeapons)
        {
            if (con.typeId == type)
            {
                return con;
            }
        }
        return null;
    }


    //Get a visual item from inventory
    public VisualItem GetVisualItem(int id)
    {
        foreach (Transform child in inventoryContent.transform)
        {
            if (child.gameObject.GetComponent<VisualItem>().GetId() == id)
            {
                return child.gameObject.GetComponent<VisualItem>();
            }
        }
        return null;
    }

    //Destroy a visual item from inventory
    public void DestroyVisualItem(int id)
    {
        foreach (Transform child in inventoryContent.transform)
        {
            if (child.gameObject.GetComponent<VisualItem>().GetId() == id)
            {
                Destroy(child.gameObject);
            }
        }
    }

    //Destroy all visual item from inventory by type
    public void DestroyAllVisualItem(int typeId)
    {
        foreach (Transform child in inventoryContent.transform)
        {
            if (child.gameObject.GetComponent<VisualItem>().GetTypeId() == typeId)
            {
                Destroy(child.gameObject);
            }
        }
    }

    //WEIGHT

    //Get total Weight and show on inventory
    private void SetWeightGlobal(float weight)
    {
        totalWeight += weight;
        visualTotalWeight.text = totalWeight.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Deteriorate of Consumables and Resources
        _elapsed += Time.deltaTime;
        if (_elapsed >= _interval)
        {
            _elapsed = 0f;
            DeteriorateConsumable();
            DeteriorateResource();
        }   
    }

    //Deteriorate consumable by healthDamage var
    void DeteriorateConsumable()
    {
        foreach (var consumable in inventoryConsumables)
        {

            if (!consumable.isTrash)
            {
                consumable.health -= consumable.healthDamage;
                var visualItem = GetVisualItem(consumable.id);
                if (consumable.health < 0)
                {
                    consumable.health = 0;
                    consumable.isTrash = true;
                    consumable.typeId = GameManager.Instance.GetTrashType();
                    visualItem.ApplyImageTrash();
                    visualItem.SetHealth(consumable.health);
                }
                visualItem.SetHealth(consumable.health);
            }
        }
    }

    //Deteriorate resource by healthDamage var
    void DeteriorateResource()
    {
        foreach (var resource in inventoryResources)
        {
            resource.price -= resource.priceDamage;
            resource.health -= resource.healthDamage;
            if (resource.price < 1)
            {
                resource.price = 1;
            }
            var visualItem = GetVisualItem(resource.id);
            visualItem.SetPrice(resource.price);
            if (resource.health < 0)
            {
                resource.health = 0;
                visualItem.SetHealth(resource.health);
            }
            visualItem.SetHealth(resource.health);
        }
    }
}
