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
    

    public Action<Consumable> onAddConsumable;

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
    }

    private void OnDisable()
    {
        onAddConsumable -= AddConsumable;
    }

    public void AddConsumable(Consumable item)
    {
        Consumable newItem = new Consumable();
        newItem.type = item.type;
        newItem.InitConsumable();
        totalWeight += item.weigth;
        inventoryConsumables.Add(newItem);
    }


    public Consumable GetFirstTypeConsumable(int type)
    {
        return inventoryConsumables.First(s => s.typeId == type);
    }

    // Update is called once per frame
    void Update()
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
                        consumable.itemName = consumable.itemName + "(trash)";
                        consumable.name = consumable.itemName;
                        
                    }
                }
            }
        }
    }
}
