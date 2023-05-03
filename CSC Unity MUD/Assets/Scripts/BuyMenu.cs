using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyMenu : MonoBehaviour
{
    public GameObject player;
    PlayerMove pm;

    public GameObject Hammer;
    public GameObject Sword;
    public GameObject Rusty;

    Button leave;
    Button take;
    // Start is called before the first frame update
    void Start()
    {
        pm = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyHammer()
    {
        if(Hammer.GetComponent<WeaponStats>().cost >= pm.souls)
        {
            pm.souls -= Hammer.GetComponent<WeaponStats>().cost;
            takeWeapon(Hammer);
        }
    }

    public void buySword()
    {
        if(Sword.GetComponent<WeaponStats>().cost >= pm.souls)
        {
            pm.souls -= Sword.GetComponent<WeaponStats>().cost;
            takeWeapon(Sword);
        }
    }

    public void buyRusty()
    {
        if(Rusty.GetComponent<WeaponStats>().cost >= pm.souls)
        {
            pm.souls -= Rusty.GetComponent<WeaponStats>().cost;
            takeWeapon(Rusty);
        }
    }

    void takeWeapon(GameObject loot)
    {
        player.GetComponent<PlayerMove>().itemInfo.SetActive(false);
        player.GetComponent<playerHands>().menu.SetActive(true);
        player.GetComponent<playerHands>().showOptions(loot);
        this.gameObject.SetActive(false);
    }

    void something(GameObject loot)
    {
        GameObject g = player.GetComponent<PlayerMove>().itemInfo;
        g.SetActive(true);
        var gScript = g.GetComponent<PickUpMenu>();
        var lScript = loot.GetComponent<WeaponStats>();
        gScript.discription.text = lScript.discription;
        gScript.damageText.text = lScript.Damage.ToString();
        gScript.itemName.text = lScript.itemName;
        gScript.profile.sprite = lScript.profile;
        leave = gScript.leave;
        leave.onClick.AddListener(LeaveItem);
        take = gScript.Take;
        take.onClick.AddListener(takeWeapon);
    }
}
