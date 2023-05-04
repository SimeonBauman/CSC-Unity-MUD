using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerMove : MonoBehaviour
{
    public int speed = 100;
    int actualSpeed;
    CharacterController controller;
    public bool canMove;

    public GameObject itemInfo;
    public int health = 10;
    public int maxHealth;
    public GameObject cam;

    public float shakeDuration = 0f;

    public float shakeAmount = 0.03f;
    public float decreaseFactor = 1.0f;
    Vector3 originalPos;

    public Image healthBar;

    public int souls = 0;

    public TMP_Text soulValue;

    public Image deathBack;
    public TMP_Text deathText;

    public bool canCall;

    public GameObject coolDude;
    public AudioSource Ads;
    
    // Start is called before the first frame update
    void Start()
    {
        itemInfo.SetActive(false);
        canMove = true;
        controller = GetComponent<CharacterController>(); 
        originalPos = cam.transform.localPosition;
        maxHealth = health;
        actualSpeed = speed;
        canCall= false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        call();
        soulValue.text = souls.ToString();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (canMove)
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        else
        {
            GetComponentInChildren<Look>().inMenu = true;
        }

        if (shakeDuration > 0)
        {
            cam.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            cam.transform.localPosition = originalPos;
        }

    }

    public void call()
    {
        if(canCall && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = transform.position;
            coolDude.transform.position = new Vector3(Random.Range(-5f,5f) + pos.x,2,Random.Range(-5f,5f) + pos.z);
        }
    }
    public void resetSpeed()
    {
        speed = actualSpeed;
    }
    public void TakeDamage(int damage)
    {
        
        health-= damage;
        float d = (float)damage;
        if(damage> 0)
        {
            shakeDuration = d / 50;
            Ads.Play();
        }
        
        healthBar.transform.localScale = new Vector2(((float)health / (float)maxHealth) * 10, 1);
        checkHealth();
    }
    
    void checkHealth()
    {
        if(health <= 0)
        {
            
            StartCoroutine(deathScreen());
            Debug.Log("dead");
        }
    }


    IEnumerator deathScreen()
    {
        float i = 0;
        yield return new WaitForSecondsRealtime(.25f);
        Time.timeScale = 0;
        while (i <= 255){
            deathBack.color = new Color32(0, 0, 0, (byte)i);
            deathText.color = new Color32(180, 11, 11, (byte)i);
            yield return new WaitForSecondsRealtime(.01f);
            i += 5;
        }

        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene(0);
    }
}
