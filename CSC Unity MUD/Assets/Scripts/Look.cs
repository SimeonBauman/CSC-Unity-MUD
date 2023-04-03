using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public Transform player;
    public float sensitivity;
    float y = 0;

    int layerMask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        y = Input.mousePosition.y * sensitivity;
        y = Mathf.Clamp(y, -90f, 90f);
        transform.localRotation = Quaternion.AngleAxis(y, Vector3.left);
        player.localRotation = Quaternion.AngleAxis(Input.mousePosition.x * sensitivity, Vector3.up);
        shootRay();
    }

    void shootRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            hit.transform.gameObject.SetActive(false);
        }
    }
}
