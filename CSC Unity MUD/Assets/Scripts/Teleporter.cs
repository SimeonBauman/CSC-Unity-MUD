using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    AudioSource Ads;
    public GameObject areana;
    bool hasTeled;

    GameObject coolDude;
    // Start is called before the first frame update
    void Start()
    {
        Ads= GetComponent<AudioSource>();
        hasTeled = false;
    }

  

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in");
        if (other.transform.gameObject.tag.Equals("player") &&!hasTeled)
        {
            hasTeled=true;
            coolDude = other.GetComponent<PlayerMove>().coolDude;
            coolDude.GetComponent<CoolDudeBrain>().state = 3;
            other.GetComponent<CharacterController>().enabled= false;
            other.GetComponent<playerHands>().inFinal = true;
            other.GetComponent<PlayerMove>().canCall= false;
            Ads.Play();
            Instantiate(areana, new Vector3(2000,0,2000), Quaternion.identity);
            coolDude.transform.position = new Vector3(2000, 2, 2000);
            
            transform.position = new Vector3(1975, 0, 2000);
            other.transform.position = new Vector3(1975, 2, 2000);
            other.GetComponent<CharacterController>().enabled = true;
            StartCoroutine(coolDude.GetComponent<CoolDudeBrain>().startFight());
        }
    }
}
