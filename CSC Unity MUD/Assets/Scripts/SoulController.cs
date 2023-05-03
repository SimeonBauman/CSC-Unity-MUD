using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    Vector3 startPosition;
    float offset;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(JumpCoroutine());
        startPosition = new Vector3(transform.position.x, Random.Range(1f,2f), transform.position.z);
        offset = Random.Range(1f, 10f);
    }

   
    private IEnumerator JumpCoroutine()
    {
        float duration = 60f; // 1 minute
        float speed = 2f;
        float startTime = Time.time;
         

        while (Time.time - startTime < duration)
        {
            float newY = startPosition.y + Mathf.Sin((Time.time - startTime + offset) * speed) * 0.5f;
            this.transform.position = new Vector3(this.transform.position.x, newY, this.transform.position.z);

            yield return null;
        }
        StartCoroutine(JumpCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("player"))
        {
            PlayerMove pm = other.gameObject.GetComponent<PlayerMove>();
            pm.souls++;
            Destroy(this.gameObject);
        }
    }
}
