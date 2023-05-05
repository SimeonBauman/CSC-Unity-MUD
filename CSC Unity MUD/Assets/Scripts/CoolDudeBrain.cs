using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CoolDudeBrain : MonoBehaviour
{
    public int state = 0;
    public GameObject player;
    public GameObject stick;
    public GameObject finalEni;


    public AudioClip wakeUp;
    public AudioClip ComeOver;
    public AudioClip Monolouge;

    public AudioClip[] HealAudios;

    public AudioClip[] BuyAudios;

    public AudioClip[] WeaponUpgrade;

    public AudioClip[] WelcomeAudios;

    public AudioClip noBuy;

    public AudioClip AlreadyOwned;

    public AudioClip Call;

    public AudioClip fightIntro;

    public AudioClip[] taunts;

    public AudioClip FinalLine;

    public AudioSource Audio;

    public GameObject Explosion;

    public int souls = 1;

    public int liveMonsters = 0;

    int ind = 0;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<playerHands>().buyMenu.GetComponent<BuyMenu>().coolDude = this.gameObject;
        player.GetComponent<PlayerMove>().coolDude = this.gameObject;
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
        if(ind == souls && liveMonsters == 0)
        {
            
            playAudio(FinalLine);
            
            ind = 0;
            souls = 0;
            liveMonsters = -1;
        }
        if(liveMonsters == -1 && !Audio.isPlaying)
        {
            
            Debug.Log("fight Over");
            Instantiate(Explosion, transform.position, Quaternion.identity);
            liveMonsters = -2;
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
                playAudio(WelcomeAudios[Random.Range(0, WelcomeAudios.Length)]);
                player.GetComponent<playerHands>().canSwing = false;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                player.GetComponent<playerHands>().buyMenu.SetActive(true);
            }
        }
    }

    public void playAudio(AudioClip ac)
    {
        Audio.clip = ac;
        Audio.Play();
    }
    
    void giveStick()
    {
        
        player.GetComponent<playerHands>().pickUpItem(stick,0);
        
    }

    public IEnumerator startFight()
    {
        playAudio(fightIntro);
        while(transform.position.y < 25)
        {
            yield return null;
            transform.position = new Vector3(transform.position.x, transform.position.y + (10*Time.deltaTime), transform.position.z);
        }
        StartCoroutine(spawnEn());
        StartCoroutine(sayLines());
    }

    IEnumerator spawnEn()
    {
        Vector3 pos = transform.position;
        ind = 0;
        while (ind < souls)
        {
            ind++;
            GameObject g = Instantiate(finalEni, new Vector3(pos.x, 2.5f, pos.z), Quaternion.identity);
            g.GetComponent<FinalEnemy>().health = ind + 5;
            g.GetComponent<FinalEnemy>().player = player;
            g.GetComponent<FinalEnemy>().coolDude = this.gameObject;
            liveMonsters++;
            
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator sayLines()
    {

        yield return new WaitForSeconds(45);
        int i = Random.Range(0, taunts.Length);
        playAudio(taunts[i]);
        StartCoroutine(sayLines());
    }


}
