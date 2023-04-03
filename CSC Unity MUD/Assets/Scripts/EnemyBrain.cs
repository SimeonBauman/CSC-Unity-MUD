using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    public int health = 0;
    public int speed = 10;
    public Transform target;
    public GameObject player;
    GameObject parentRoom;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        target = player.transform;
        parentRoom = this.GetComponentInParent<Transform>().gameObject;
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (target != null)
        {
            lookAtTarget();
            checkViewTarget();
            Vector3 move = transform.forward;
            if((Vector3.Distance(new Vector2(transform.position.x,transform.position.z), new Vector2(target.position.x,target.position.z)) > 1)){
                controller.Move(move * .01f);
            }
        }
        else if(Vector3.Distance(transform.position, player.transform.position) < 8)
        {
            target = player.transform;
        }
    }
    

    void lookAtTarget()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
        
    }

    void checkViewTarget()
    {
        LayerMask l = 1 << 7;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject == player)
            {
                Debug.Log("can see " + target.gameObject.name);
            }
            else
            {
                target = null;
            }

        }

        

        
    }

    
}
