using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public int speed = 100;
    CharacterController controller;
    public bool canMove;

    public GameObject itemInfo;
    public int health = 10;
    int maxHealth;
    public GameObject cam;

    public float shakeDuration = 0f;

    public float shakeAmount = 0.03f;
    public float decreaseFactor = 1.0f;
    Vector3 originalPos;

    public Image healthBar;

    public int souls = 0;

    // Start is called before the first frame update
    void Start()
    {
        itemInfo.SetActive(false);
        canMove = true;
        controller = GetComponent<CharacterController>(); 
        originalPos = cam.transform.localPosition;
        maxHealth = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
    public void TakeDamage(int damage)
    {
        
        health-= damage;
        float d = (float)damage;
        shakeDuration = d/50;
        
        healthBar.transform.localScale = new Vector2(((float)health / (float)maxHealth) * 10, 1);
        checkHealth();
    }
    
    void checkHealth()
    {
        if(health <= 0)
        {
            Debug.Log("dead");
        }
    }

}
