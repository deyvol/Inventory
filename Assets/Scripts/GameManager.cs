using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float defaultY = 0.20f;
    private const int defaultConsumablesCount = 5;
    private const int defaultResourcesCount = 10;


    [Header ("Items")]
    public List<GameObject> Consumables;
    public List<GameObject> Resources;

    [Header("Container")]
    public GameObject consumablesContainer;
    public GameObject resourcesContainer;

    [Header("General")]
    public GameObject gameZone;
    public bool isGameActive = false;


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

    private Vector3 SetRandomAreaPoint(Vector3 size)
    {
        return new Vector3(Random.Range(-size.x/2, size.x/2),defaultY, Random.Range(-size.z / 2, size.z / 2));
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

    // Update is called once per frame
    void Update()
    {
               
    }

    
}
