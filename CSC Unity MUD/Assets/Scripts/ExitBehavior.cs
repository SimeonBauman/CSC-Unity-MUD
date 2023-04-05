using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehavior : MonoBehaviour
{
    public bool opened = false;
    bool enab = false;
    float startY;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;  
    }

    // Update is called once per frame
    void Update()
    {
        if (enab && transform.position.y <= startY + 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 3 * Time.deltaTime, transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("player") && opened)
        {
            enab = true;
        }
    }
}
