using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class Explosion : MonoBehaviour
{
    public Light l;
    public AudioSource Am;
    // Start is called before the first frame update
    void Start()
    {
        l = this.gameObject.GetComponent<Light>();
        StartCoroutine(explode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator explode()
    {
        long pw = 0;
        while (pw< 10000000000)
        {
            if(Am.volume > 0)
            {
                Am.volume -= .01f;
            }
            
            
            l.intensity = pw;
            pw += 1000;
            yield return null;
        }
    }
}
