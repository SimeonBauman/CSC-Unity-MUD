using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class EnemyBrain : MonoBehaviour
{

    public int health = 0;
    public float speed = 5f;
    public Transform target;
    public GameObject player;
    GameObject parentRoom;
    public Rigidbody rb;
    public GameObject eyes;
    Animator ani;
    public GameObject soul;
    bool canAttack;
    public int attackDistance;
    public int damage = 5;
    int souls;
    // Start is called before the first frame update
    void Start()
    {
        souls = Random.Range(1, 4);
        target = player.transform;
        parentRoom = this.GetComponentInParent<Transform>().gameObject;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        canAttack = true;
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
        transform.position = new Vector3(transform.position.x,3f,transform.position.z);
        if (target != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 3 &&  canAttack)
            {
                rb.velocity = transform.forward * speed;
                ani.SetBool("isMoving", true);
            }
            else
            {
                rb.velocity = Vector3.zero;
                ani.SetBool("isMoving", false);
                if (canAttack) StartCoroutine(attack());
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
        int layer_mask = LayerMask.GetMask("player visuals","wall");
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,layer_mask))
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
            Vector3 pos = transform.position;
            for(int i = 0; i < souls;i++)
            {
                Instantiate(soul, new Vector3(Random.Range(-3f, 3f) + pos.x, 1, Random.Range(-3f, 3f) + pos.z), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }



    IEnumerator attack()
    {
        canAttack = false;
        ani.SetBool("isAttacking", true);
        yield return new WaitForSeconds(.5f);
        if(Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            player.GetComponent<PlayerMove>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(.5f);
        ani.SetBool("isAttacking", false);
        yield return new WaitForSeconds(.75f);
        canAttack= true;
    }




}
