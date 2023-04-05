using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class EnemyBrain : MonoBehaviour
{

    public int health = 0;
    float speed = 5f;
    public Transform target;
    public GameObject player;
    GameObject parentRoom;
    public Rigidbody rb;
    public GameObject eyes;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        target = player.transform;
        parentRoom = this.GetComponentInParent<Transform>().gameObject;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        if(eyes != null)
        {
            eyes = this.gameObject;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        checkHealth();
        movement();
        
    }
    

    void movement()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 3)
            {
                rb.velocity = transform.forward * speed;
                ani.SetBool("isMoving", true);
            }
            else
            {
                rb.velocity = Vector3.zero;
                ani.SetBool("isMoving", false);
            }

            lookAtTarget();
            if (!canSeePlayer())
            {
                target = null;
                rb.velocity = Vector3.zero;
                ani.SetBool("isMoving", false);
            }
        }
        else if (canSeePlayer() || Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            target = player.transform;
            lookAtTarget();

        }
        else
        {
            target = null;
            rb.velocity = Vector3.zero;
        }
    }

    void lookAtTarget()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
        
    }
    bool canSeePlayer()
    {
       
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject == player)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        return false;
    }

    void checkHealth()
    {
        if(health <= 0)
        {
            transform.GetComponentInParent<Room>().liveInhabs -= 1;
            Destroy(gameObject);
        }
    }








}
