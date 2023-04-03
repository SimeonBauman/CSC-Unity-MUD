using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] walls = new GameObject[4];
    public GameObject[] wallPoints = new GameObject[4];
    public GameObject bulb;
    public GameObject[] enemies = new GameObject[4];
    private void Start()
    {
        
        getLightColor();
    }
    public void enableRoom()
    {

        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i] != null)
            {
                walls[i].SetActive(true);
            }
        }
    }
    
    public void disableRoom()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if(walls[i] != null) {
                walls[i].SetActive(false);
            }
            
        }
        transform.gameObject.SetActive(false);
    }

    void getLightColor()
    {
        Light lig = GetComponentInChildren<Light>();
        
        Color color = new Color32(System.Convert.ToByte(Random.Range(0, 255)), System.Convert.ToByte(Random.Range(0, 255)), System.Convert.ToByte(Random.Range(0, 255)), 255);
        lig.color = color;
        bulb.GetComponent<Renderer>().material.color = new Color32((byte)color.r,(byte)color.g,(byte)color.b, 166);
        
    }

    
}
