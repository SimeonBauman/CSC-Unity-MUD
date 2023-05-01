using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public Transform player;
    public float sensitivity;
    float y = 0;
    public bool inMenu;
    float yOff = 0;
    float xOff = 0;

    // Start is called before the first frame update
    void Start()
    {
        inMenu = false;
        yOff = Input.mousePosition.y;
        xOff = Input.mousePosition.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inMenu)
        {
            
            y = (Input.mousePosition.y - yOff) * sensitivity;
            y = Mathf.Clamp(y, -90f, 90f);
            transform.localRotation = Quaternion.AngleAxis(y, Vector3.left);
            player.localRotation = Quaternion.AngleAxis((Input.mousePosition.x-xOff) * sensitivity, Vector3.up);
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            yOff = Input.mousePosition.y - yOff;
            xOff = Input.mousePosition.x - xOff;
            Cursor.lockState = CursorLockMode.None;
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
