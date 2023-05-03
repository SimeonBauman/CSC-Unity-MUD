using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CoolDudeBrain : MonoBehaviour
{
    public int state = 0;
    public GameObject player;
    public GameObject stick;



    public AudioClip wakeUp;
    public AudioClip ComeOver;
    public AudioClip Monolouge;

    public AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startAudio());
        //player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
        if(state == 1 && Audio.isPlaying)
        {
            player.GetComponent<PlayerMove>().speed = 0;
        }
        else if(state == 1 && !Audio.isPlaying)
        {
            player.GetComponent<PlayerMove>().resetSpeed();
            giveStick();
            
            state = 2;
        }
    }

    void lookAtPlayer()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
    }

    IEnumerator startAudio()
    {
        yield return new WaitForSeconds(3.5f);
        playAudio(wakeUp);
        yield return new WaitForSeconds(2.5f);
        playAudio(ComeOver);
        state = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            if(state == 0)
            {
                playAudio(Monolouge);
                state = 1;
            }
            else if (state == 2)
            {
                player.GetComponent<playerHands>().canSwing = false;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                player.GetComponent<playerHands>().buyMenu.SetActive(true);
            }
        }
    }

    void playAudio(AudioClip ac)
    {
        Audio.clip = ac;
        Audio.Play();
    }
    
    void giveStick()
    {
        
        player.GetComponent<playerHands>().pickUpItem(stick,0);
        
    }
}
