using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    private const float maxWeight = 10f;

    [Header("General")]
    public float totalWeight;

    [Header("Items Inventory")]
    [SerializeField] private List<Consumable> inventoryConsumables = new List<Consumable>();
    [SerializeField] private List<Resource> inventoryResources = new List<Resource>();


    public Action<Consumable> onAddConsumable;
    public Action<Resource> onAddResource;

    private float _elapsed = 0f;
    private float _interval = 1f;


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
    }

    public float GetMaxWeight()
    {
        return maxWeight;
    }

    private void OnEnable()
    {
        onAddConsumable += AddConsumable;
        onAddResource += AddResource;
    }

    private void OnDisable()
    {
        onAddConsumable -= AddConsumable;
    }

    public void AddConsumable(Consumable item)
    {
        Consumable newItem = new Consumable();
        newItem.data = item.data;
        newItem.InitConsumable();
        totalWeight += item.weigth;
        inventoryConsumables.Add(newItem);
    }

    public void AddResource(Resource item)
    {
        Resource newItem = new Resource();
        newItem.data = item.data;
        newItem.InitResource();
        totalWeight += item.weigth;
        inventoryResources.Add(newItem);    
    }


    public Consumable GetFirstTypeConsumable(int type)
    {
        return inventoryConsumables.First(s => s.typeId == type);
    }

    // Update is called once per frame
    void Update()
    {
        DeteriorateConsumable();
        DeteriorateResource();
    }

    void DeteriorateConsumable()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= _interval)
        {
            _elapsed = 0f;
            foreach (var consumable in inventoryConsumables)
            {

                if (!consumable.isTrash)
                {
                    consumable.health -= consumable.healthDamage;
                    if (consumable.health < 0)
                    {
                        consumable.health = 0;
                        consumable.isTrash = true;
                        Debug.Log(consumable.itemName + " is Trash");
                        //consumable.itemName = consumable.itemName + "(trash)";
                        //consumable.name = consumable.itemName.ToString();

                    }
                }
            }
        }
    }

    void DeteriorateResource()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= _interval)
        {
            _elapsed = 0f;
            foreach (var resource in inventoryResources)
            {

                if (resource.health > 1)
                {
                    resource.health -= resource.healthDamage;
                    resource.price -= resource.priceDamage;
                } 
                if (resource.health == 1)
                {
                    resource.health = 0;
                    resource.price = 1;
                    Debug.Log(resource.itemName + " is devaluated. The new price is " + resource.price);
                    //resource.itemName = resource.itemName + "(trash)";
                    //resource.name = resource.itemName.ToString();
                }
            }
        }
    }
}
