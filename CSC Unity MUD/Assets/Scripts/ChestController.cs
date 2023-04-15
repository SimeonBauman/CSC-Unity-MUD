using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ChestController : MonoBehaviour
{
    public GameObject loot;
    GameObject visuals;

    GameObject player;

    bool hasBeenSearched;

    Button leave;
    Button take;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in");
        
        if (other.gameObject.tag.Equals("player") && !hasBeenSearched)
        {
            Debug.Log("in 2");
            player = other.gameObject;
            Time.timeScale = 0;
            GameObject g = other.GetComponent<PlayerMove>().itemInfo;
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
            hasBeenSearched= true;
        }
    }

    void takeWeapon()
    {
        player.GetComponent<playerHands>().pickUpItem(loot, 0);
        Time.timeScale = 1;
        player.GetComponent<PlayerMove>().itemInfo.SetActive(false);
        StartCoroutine(destroyChest());
    }

    public void LeaveItem()
    {

        Time.timeScale = 1;
        player.GetComponent<PlayerMove>().itemInfo.SetActive(false);
        StartCoroutine(destroyChest());

    }

    IEnumerator destroyChest()
    {
        if (transform.localScale.x > .1) {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime, transform.localScale.y - Time.deltaTime, transform.localScale.z - Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        else
        {
            Destroy(gameObject);
        }
        yield return null;
        StartCoroutine(destroyChest());
    }
}
