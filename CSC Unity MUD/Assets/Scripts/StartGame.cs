using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class StartGame : MonoBehaviour
{
    public GameObject player;
    public Animator ani;
    
    public IEnumerator startCut()
    {
        player.SetActive(false);
        ani.SetBool("start", true);
        yield return new WaitForSeconds(5f);
        ani.SetBool("start", false);
        Destroy(this.gameObject);
    }

   
}
