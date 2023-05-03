using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyMenu : MonoBehaviour
{
    public GameObject player;
    PlayerMove pm;
    playerHands ph;
    public GameObject Hammer;
    public GameObject Sword;
    public GameObject Rusty;

    Button leave;
    Button take;
    // Start is called before the first frame update
    void Start()
    {
        pm = player.GetComponent<PlayerMove>();
        ph = player.GetComponent<playerHands>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Return))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            this.gameObject.SetActive(false); 
            
        }
    }

    public void buyHammer()
    {
        if(Hammer.GetComponent<WeaponStats>().cost <= pm.souls)
        {
            pm.souls -= Hammer.GetComponent<WeaponStats>().cost;
            takeWeapon(Hammer);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void buySword()
    {
        if(Sword.GetComponent<WeaponStats>().cost <= pm.souls)
        {
            pm.souls -= Sword.GetComponent<WeaponStats>().cost;
            takeWeapon(Sword);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void buyRusty()
    {
        if(Rusty.GetComponent<WeaponStats>().cost <= pm.souls)
        {
            pm.souls -= Rusty.GetComponent<WeaponStats>().cost;
            takeWeapon(Rusty);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void UpgradeHealth()
    {
        if(pm.souls >= 2)
        {
            pm.maxHealth += 2;
            pm.health = pm.maxHealth;
            pm.souls -= 2;
            pm.healthBar.transform.localScale = new Vector2(((float)pm.health / (float)pm.maxHealth) * 10, 1);
        }
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        ph.canSwing = true;
    }

    public void UpgradeWeapons()
    {
        if (pm.souls >= 20)
        {
            pm.souls -= 20;
            for (int i = 0; i < ph.invetory.Length; i++)
            {
                if (ph.invetory[i].GetComponent<WeaponStats>().Damage > 0)
                {
                    ph.invetory[i].GetComponent<WeaponStats>().Damage += 5;
                }
            }
        }
        Cursor.lockState= CursorLockMode.Locked;
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        ph.canSwing = true;
    }

    void takeWeapon(GameObject loot)
    {
        pm.itemInfo.SetActive(false);
        ph.menu.SetActive(true);
        ph.showOptions(loot);
        this.gameObject.SetActive(false);
    }

    
}
