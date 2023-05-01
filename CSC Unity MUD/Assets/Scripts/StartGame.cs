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
        //transform.position = new Vector3(player.transform.position.x, 1, player.transform.position.z);
        ani.SetBool("start", true);
        transform.position = new Vector3(player.transform.position.x, 1, player.transform.position.z);
        yield return new WaitForSeconds(5f);
        ani.SetBool("start", false);
        player.SetActive(true);
        
        Destroy(this.gameObject);
    }

   
}
