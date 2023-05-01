using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public Transform player;
    public float sensitivity;
   
    public bool inMenu;
  

    
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        inMenu = true;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if (!inMenu)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity *Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity *Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.transform.Rotate(Vector3.up * mouseX);

           
        }
        else
        {
           
        }
        
       
    }

    void shootRay()
    {
        int layer_mask = LayerMask.GetMask("player visuals", "wall");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layer_mask))
        {
            
            
        }
    }
}
