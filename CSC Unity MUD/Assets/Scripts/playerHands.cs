using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHands : MonoBehaviour
{
    public GameObject hand;
    public GameObject[] invetory = new GameObject[2];
    int active = 0;
    // Start is called before the first frame update
    void Start()
    {
        //pickUpItem(invetory[0], 0);
        if (invetory[1] != null)
        {
            invetory[1].SetActive(false);
        }
        if (invetory[0] != null)
        {
            invetory[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switchWeapon();
    }

    void switchWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            

            if (invetory[(active + 1) % 2] != null)
            {
                invetory[active].SetActive(false);
                invetory[(active + 1) % 2].SetActive(true);
                active = (active + 1) % 2;
                //invetory[active].transform.position = hand.transform.localPosition;
            }
        }
    }

    public void pickUpItem(GameObject g, int pos)
    {
        invetory[pos] = Instantiate(g,hand.transform.position,Quaternion.identity);
        invetory[pos].transform.parent = hand.transform;
        invetory[pos].transform.localRotation = Quaternion.identity;
        //invetory[pos].transform.position =;
    }
}
