using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class playerHands : MonoBehaviour
{
    public GameObject hand;
    public GameObject[] invetory = new GameObject[2];
    int active = 0;

    public Image profile1;
    public Image profile2;
    public TMP_Text Damage1;
    public TMP_Text Damage2;
    public Button b1;
    public Button b2;
    public GameObject menu;

    GameObject Replacement;
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
        if (!invetory[pos].tag.Equals("empty"))
        {
            Destroy(invetory[pos]);
        }
        
        invetory[pos] = Instantiate(g,hand.transform.position,Quaternion.identity);
        invetory[pos].transform.parent = hand.transform;
        invetory[pos].transform.localRotation = Quaternion.identity;
        if(pos != active)
        {
            invetory[pos].SetActive(false);
        }
        //invetory[pos].transform.position =;
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void showOptions(GameObject Replacement)
    {
        this.Replacement = Replacement;
        profile1.sprite = invetory[0].GetComponent<WeaponStats>().profile;
        Damage1.text = invetory[0].GetComponent<WeaponStats>().Damage.ToString();
        b1.onClick.AddListener(replace1);
        b2.onClick.AddListener(replace2);
        profile2.sprite = invetory[1].GetComponent<WeaponStats>().profile;
        Damage2.text = invetory[1].GetComponent<WeaponStats>().Damage.ToString();
    }

    void replace1()
    {
        pickUpItem(Replacement, 0);
    }

    void replace2()
    {
        pickUpItem(Replacement, 1);
    }
}
