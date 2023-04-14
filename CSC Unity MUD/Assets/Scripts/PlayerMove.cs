using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int speed = 100;
    CharacterController controller;
    public bool canMove;

    public GameObject itemInfo;
    // Start is called before the first frame update
    void Start()
    {
        itemInfo.SetActive(false);
        canMove = true;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (canMove)
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        else
        {
            GetComponentInChildren<Look>().inMenu = true;
        }
       
    }

    

}
