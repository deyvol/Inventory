using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Main Camera")]
    public Camera mainCamera;
    
    [Header("Game Loop")]
    public bool isGameActive;
        
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
    void Start()
    {
        //Variables
        isGameActive = true;

           
    }

    // Update is called once per frame
    void Update()
    {
               
    }
}
