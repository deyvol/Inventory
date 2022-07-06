using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{    
    [Header("Room Zone")]
    public float width, lenght;
    public float camOffetX;
    public float camOffetZ;
    public float smoothTime;

    [Header("Player Zone")]
    public GameObject followObject;    
    public float playerOffetMaxX;
    public float playerOffetMinX;
    public float playerOffetMaxZ;
    public float playerOffetMinZ;

    [Header("Camera Zone")]
    [SerializeField]
    private float posX;
    [SerializeField]
    private float posZ;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followObject != null)
        {            
            posX = lenght + camOffetX;
            posZ = width + camOffetZ;
            if ((followObject.transform.position.x > playerOffetMaxX))
            {
                posX += followObject.transform.position.x - playerOffetMaxX;
            }
            if ((followObject.transform.position.x < playerOffetMinX))
            {
                posX -= playerOffetMinX - followObject.transform.position.x;
            }
            if ((followObject.transform.position.z > playerOffetMaxZ))
            {
                posZ += followObject.transform.position.z - playerOffetMaxZ;
            }
            if ((followObject.transform.position.z < playerOffetMinZ))
            {
                posZ -= playerOffetMinZ - followObject.transform.position.z;
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, transform.position.y, posZ), smoothTime);
        }
    }
}
