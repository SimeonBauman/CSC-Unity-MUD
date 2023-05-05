using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(run());
    }

   IEnumerator run()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);
    }
}
