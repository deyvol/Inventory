using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const float defaultY = 0.20f;
    private const int defaultConsumablesCount = 5;
    private const int defaultResourcesCount = 10;
    private const int defaultWeaponsCount = 2;

    //Items Codes
    private const int trashType = 0;
    private const int bananaType = 1;
    private const int coconutType = 2;
    private const int bulletType = 3;
    private const int shotgunType = 4;
    private const int swordType = 5;

    [Header ("Items")]
    public List<GameObject> Consumables;
    public List<GameObject> Resources;
    public List<GameObject> Weapons;

    [Header("Container")]
    public GameObject consumablesContainer;
    public GameObject resourcesContainer;
    public GameObject resourcesWeapons;
    public GameObject messagesContainer;
    public Text messages;

    [Header("General")]
    public bool isGameActive = false;
    public GameObject gameZone;
    public GameObject inventory;
    private bool activeInventory = false;

    [Header("Player")]
    public GameObject shotGun;
    public GameObject sword;

    private bool isProcessing = false;


    //Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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

    // Start is called before the first frame update
    private void Start()
    {
        //Set the player in the center of the plane
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(0, 0, 0);
        //Init of the items
        InitConsumables(defaultConsumablesCount);
        InitResources(defaultResourcesCount);
        InitWeapons(defaultWeaponsCount);

        isGameActive = true;
    }

    //Init of the consumables and Instantiation (5 items of each)
    private void InitConsumables(int count)
    {
        var areaZone = GetPlaneSize(gameZone);
        foreach (var consumable in Consumables)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = SetRandomAreaPoint(areaZone);
                var obj = Instantiate(consumable, pos, Quaternion.identity, consumablesContainer.transform);
                obj.GetComponent<Consumable>().InitConsumable();
            }
        }
    }

    //Init of the recources and Instantiation (10 items)
    private void InitResources(int count)
    {
        var areaZone = GetPlaneSize(gameZone);
        foreach (var resources in Resources)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = SetRandomAreaPoint(areaZone);
                var obj = Instantiate(resources, pos, Quaternion.identity, resourcesContainer.transform);
                obj.GetComponent<Resource>().InitResource();
            }
        }
    }

    //Init of the weapons and Instantiation (2 items of each)
    private void InitWeapons(int count)
    {
        var areaZone = GetPlaneSize(gameZone);
        foreach (var weapons in Weapons)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = SetRandomAreaPoint(areaZone);
                var obj = Instantiate(weapons, pos, Quaternion.identity, resourcesWeapons.transform);
                obj.GetComponent<Weapon>().InitWeapon();
            }
        }
    }
    //Assign a Vector3 for the item inside the plane, receiving the size of the plane
    private Vector3 SetRandomAreaPoint(Vector3 size)
    {
        return new Vector3(UnityEngine.Random.Range(-size.x/2, size.x/2),defaultY, UnityEngine.Random.Range(-size.z / 2, size.z / 2));
    }
    //Get the size of the plane in vector3
    Vector3 GetPlaneSize(GameObject _plane)
    {
        Mesh planeMesh = _plane.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;
        // size in pixels
        float boundsX = _plane.transform.localScale.x * bounds.size.x;
        float boundsY = _plane.transform.localScale.y * bounds.size.y;
        float boundsZ = _plane.transform.localScale.z * bounds.size.z;
        return new Vector3(boundsX, boundsY, boundsZ);
    }

    public int GetTrashType()
    {
        return trashType;
    }

    void LateUpdate()
    {   //Keys active in the game
        if (isGameActive)
        {
            //Eat banana
            if (Input.GetKeyDown(KeyCode.B) && !isProcessing)
            {
                isProcessing = true;
                DeleteConsumable(bananaType);
                isProcessing = false;
            }
            //Drink Coconut
            if (Input.GetKeyDown(KeyCode.C))
            {
                isProcessing = true;
                DeleteConsumable(coconutType);
                isProcessing = false;
            }
            //Shot the gun
            if (Input.GetKeyDown(KeyCode.K))
            {
                isProcessing = true;
                UseWeapon(shotgunType);
                isProcessing = false;
            }
            //Use the sword
            if (Input.GetKeyDown(KeyCode.L))
            {
                isProcessing = true;
                UseWeapon(swordType);
                isProcessing = false;
            }
            //Delete trash from inventory
            if (Input.GetKeyDown(KeyCode.J))
            {
                isProcessing = true;
                DeleteTrash();
                isProcessing = false;
            }
            //Show inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                isProcessing = true;
                activeInventory = !activeInventory;
                inventory.SetActive(activeInventory);
                isProcessing = false;
            }
        }
    }

    //Function to delete the objects type Trash of inventory
    private void DeleteTrash()
    {
        if (InventoryManager.Instance.GetCountConsumableByType(trashType) == 0)
        {
            messagesContainer.SetActive(true);
            messages.text = "YOU DON'T HAVE TRASH TO DESTROY";
            StartCoroutine(SendMessage());
        }
        if (InventoryManager.Instance.GetCountConsumableByType(trashType) > 0)
        {
            InventoryManager.Instance.DeleteAllConsumable(trashType);
            InventoryManager.Instance.DestroyAllVisualItem(trashType);
            ShowMessage(trashType);
        }
    }

    //Function to use a weapon. If the weapon is type ShotGun, consumes a resource
    private void UseWeapon(int type)
    {   //Search for any weapon on inventory
        if (InventoryManager.Instance.GetCountWeaponByType(type) > 0)
        {
            //get a first weapon on inventory depends of type (Sword or ShotGun)
            var item = InventoryManager.Instance.GetFirstTypeWeapon(type);
            //With this function, we know is a ShotGun or not
            if (item.resourceItemType > 0)
            {
                //The item is a ShotGun, check for resources type Ammo 
                if (InventoryManager.Instance.GetCountResourceByType(item.resourceItemType) == 0)
                {
                    messagesContainer.SetActive(true);
                    messages.text = "YOU DON'T HAVE AMMO";
                    StartCoroutine(SendMessage());
                }
                if (InventoryManager.Instance.GetCountResourceByType(item.resourceItemType) > 0)
                {
                    //You have AMMO, Shot and delete the resource on inventory
                    var itemResource = InventoryManager.Instance.GetFirstTypeResource(item.resourceItemType);
                    InventoryManager.Instance.DestroyVisualItem(itemResource.id);
                    InventoryManager.Instance.DeleteResource(itemResource.id);
                    ShowMessage(type);
                } 
                
            } else
            {
                //Here, the item is a Sword, message for the use of Sword
                ShowMessage(type);
            }
        }
    }

    //Function to delete (use) the objects type consumable of inventory
    void DeleteConsumable (int type)
    {
        //Firts, check if you have consumables of any type
        if (InventoryManager.Instance.GetCountConsumableByType(type) == 0)
        {
            messagesContainer.SetActive(true);
            messages.text = "YOU DON'T HAVE CONSUMABLES";
            StartCoroutine(SendMessage());
        }
        if (InventoryManager.Instance.GetCountConsumableByType(type) > 0)
        {
            //You have consumable, use and delete the consumable on inventory
            var item = InventoryManager.Instance.GetFirstTypeConsumable(type);
            InventoryManager.Instance.DestroyVisualItem(item.id);
            InventoryManager.Instance.DeleteConsumable(item.id);
            ShowMessage(type);
        }   
    }
    //Send Messages on screen
    void ShowMessage(int type)
    {
        switch (type)
        {
            case bananaType:
                messagesContainer.SetActive(true);
                messages.text = "YOU EAT A BANANA";
                StartCoroutine(SendMessage());
                break;
            case coconutType:
                messagesContainer.SetActive(true);
                messages.text = "YOU DRINK A COCO";
                StartCoroutine(SendMessage());
                break;
            case shotgunType:
                messagesContainer.SetActive(true);
                messages.text = "YOU SHOT A GUN";
                StartCoroutine(SendMessage());
                break;
            case swordType:
                messagesContainer.SetActive(true);
                messages.text = "YOU USE THE SWORD";
                StartCoroutine(SendMessage());
                break;
            case trashType:
                messagesContainer.SetActive(true);
                messages.text = "EMPTY TRASH";
                StartCoroutine(SendMessage());
                break;
        }
    }
    //Wait 2 seconds to delete the displayed message
    IEnumerator SendMessage()
    {
        yield return new WaitForSeconds(2f);
        messagesContainer.SetActive(false);
        messages.text = "";
    }

}
