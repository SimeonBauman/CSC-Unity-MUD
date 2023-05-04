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

    public GameObject coolDude;
    CoolDudeBrain cB;
    // Start is called before the first frame update
    void Start()
    {
        pm = player.GetComponent<PlayerMove>();
        ph = player.GetComponent<playerHands>();
        cB = coolDude.GetComponent<CoolDudeBrain>();
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
            cB.souls += Hammer.GetComponent<WeaponStats>().cost;
        }
        else
        {
            noBuy();
        }
    }

    public void buySword()
    {
        if(Sword.GetComponent<WeaponStats>().cost <= pm.souls)
        {
            pm.souls -= Sword.GetComponent<WeaponStats>().cost;
            takeWeapon(Sword);
            cB.souls += Sword.GetComponent<WeaponStats>().cost;
        }
        else
        {
            noBuy();
        }
    }

    public void buyRusty()
    {
        if(Rusty.GetComponent<WeaponStats>().cost <= pm.souls)
        {
            pm.souls -= Rusty.GetComponent<WeaponStats>().cost;
            takeWeapon(Rusty);
            cB.souls += Rusty.GetComponent<WeaponStats>().cost;
        }
        else
        {
            noBuy();
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
            CoolDudeBrain Cb = coolDude.GetComponent<CoolDudeBrain>();
            cB.souls += 2;
            Cb.playAudio(Cb.HealAudios[Random.Range(0, Cb.HealAudios.Length)]);
            Cursor.lockState = CursorLockMode.Locked;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
            ph.canSwing = true;
        }
        else
        {
            noBuy();
        }
    }

    public void UpgradeWeapons()
    {
        if (pm.souls >= 20)
        {
            CoolDudeBrain Cb = coolDude.GetComponent<CoolDudeBrain>();
            pm.souls -= 20;
            for (int i = 0; i < ph.invetory.Length; i++)
            {
                if (ph.invetory[i].GetComponent<WeaponStats>().Damage > 0)
                {
                    ph.invetory[i].GetComponent<WeaponStats>().Damage += 5;
                }
            }
            cB.souls += 20;
            ph.setStats();
            Cb.playAudio(Cb.WeaponUpgrade[Random.Range(0,Cb.WeaponUpgrade.Length)]);
            Cursor.lockState = CursorLockMode.Locked;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
            ph.canSwing = true;
        }
        else
        {
            noBuy();
        }
        
    }

    public void callAbility() 
    {
        if (pm.souls >= 8)
        {


            CoolDudeBrain cB = coolDude.GetComponent<CoolDudeBrain>();
            if (pm.canCall)
            {

                cB.playAudio(cB.AlreadyOwned);

            }
            else
            {
                cB.playAudio(cB.Call);
                pm.souls -= 8;
                pm.canCall = true;
                cB.souls += 8;
            }
            Cursor.lockState = CursorLockMode.Locked;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
            ph.canSwing = true;
        }
        else
        {
            noBuy();
        }
    }
    void takeWeapon(GameObject loot)
    {
        CoolDudeBrain Cb = coolDude.GetComponent<CoolDudeBrain>();
        pm.itemInfo.SetActive(false);
        ph.menu.SetActive(true);
        ph.showOptions(loot);
        Cb.playAudio(Cb.BuyAudios[Random.Range(0, Cb.BuyAudios.Length)]);
        this.gameObject.SetActive(false);
        player.GetComponent<playerHands>().canSwing = true;
    }

    void noBuy()
    {
        CoolDudeBrain Cb = coolDude.GetComponent<CoolDudeBrain>();
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cb.playAudio(Cb.noBuy);
        player.GetComponent<playerHands>().canSwing = true;
    }
    
}
