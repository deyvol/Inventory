using System;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("General")]
    public bool isGameActive = false;
    public GameObject gameZone;

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
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(0, 0, 0);
        InitConsumables(defaultConsumablesCount);
        InitResources(defaultResourcesCount);
        InitWeapons(defaultWeaponsCount);
        isGameActive = true;
    }

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

    private Vector3 SetRandomAreaPoint(Vector3 size)
    {
        return new Vector3(UnityEngine.Random.Range(-size.x/2, size.x/2),defaultY, UnityEngine.Random.Range(-size.z / 2, size.z / 2));
    }

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
    {
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
        }
    }

    private void DeleteTrash()
    {
        if (InventoryManager.Instance.GetCountConsumableByType(trashType) > 0)
        {
            InventoryManager.Instance.DeleteAllConsumable(trashType);
            InventoryManager.Instance.DestroyAllVisualItem(trashType);
            ShowMessage(trashType);
        }
        else
        {
            Debug.Log("NO TIENES BASURA");
        }
    }

    private void UseWeapon(int type)
    {
        if (InventoryManager.Instance.GetCountWeaponByType(type) > 0)
        {
            var item = InventoryManager.Instance.GetFirstTypeWeapon(type);
            if (item.resourceItemType > 0)
            {
                if (InventoryManager.Instance.GetCountResourceByType(item.resourceItemType) > 0)
                {
                    var itemResource = InventoryManager.Instance.GetFirstTypeResource(item.resourceItemType);
                    InventoryManager.Instance.DestroyVisualItem(itemResource.id);
                    InventoryManager.Instance.DeleteResource(itemResource.id);
                    ShowMessage(type);
                } else
                {
                    Debug.Log("SENSE RECURSOS");
                }
            } else
            {
                ShowMessage(type);
            }
        }
    }

    void DeleteConsumable (int type)
    {
        if (InventoryManager.Instance.GetCountConsumableByType(type) > 0)
        {
            var item = InventoryManager.Instance.GetFirstTypeConsumable(type);
            InventoryManager.Instance.DestroyVisualItem(item.id);
            InventoryManager.Instance.DeleteConsumable(item.id);
            ShowMessage(type);
        }
        else
        {
            Debug.Log("NO TIENES");
        }
        
    }

    void ShowMessage(int type)
    {
        switch (type)
        {
            case bananaType:
                Debug.Log("HE MENJAT UNA BANANA");
                break;
            case coconutType:
                Debug.Log("HE BEGUT UN COCO");
                break;
            case shotgunType:
                Debug.Log("HE DISPARAT L'ESCOPETA");
                break;
            case swordType:
                Debug.Log("HE UTILITZAT L'ESPASA");
                break;
            case trashType:
                Debug.Log("HE BUIDAT LA BROSSA");
                break;
        }
    }

}
