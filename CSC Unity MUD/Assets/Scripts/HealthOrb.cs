using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag.Equals("player"))
        {
            other.GetComponent<PlayerMove>().health = other.GetComponent<PlayerMove>().maxHealth;
            Destroy(this.gameObject);
        }
    }
}
