using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CoolDudeBrain : MonoBehaviour
{
    public int state = 0;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
    }

    void lookAtPlayer()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.GetComponent<playerHands>().canSwing = false;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            player.GetComponent<playerHands>().buyMenu.SetActive(true);
        }
    }
}
