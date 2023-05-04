using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class Explosion : MonoBehaviour
{
    
    public AudioSource Am;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(explode());
    }

   

    IEnumerator explode()
    {
      float pw = 0;
        while (pw <= 100)
        {
            if(Am.volume > 0)
            {
                Am.volume -= .1f;
            }
            
            
            this.transform.localScale = new Vector3(pw, pw, pw);
            yield return null;
            pw += Time.deltaTime * 20 ;
        }

        SceneManager.LoadScene(2);

    }
}
