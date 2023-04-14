using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject loot;
    GameObject visuals;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in");
        int layer_mask = LayerMask.GetMask("player visuals");
        if (other.gameObject.layer.Equals(layer_mask))
        {
            Debug.Log("in 2");
            other.GetComponent<PlayerMove>().canMove = false;
            GameObject g = other.GetComponent<PlayerMove>().itemInfo;
            g.SetActive(true);
            var gScript = g.GetComponent<PickUpMenu>();
            var lScript = loot.GetComponent<WeaponStats>();
            gScript.discription.text = lScript.discription;
            gScript.damageText.text = lScript.Damage.ToString();
            gScript.itemName.text = lScript.itemName;
            gScript.profile.sprite = lScript.profile;

        }
    }
}
