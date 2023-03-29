using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    int size = 30;
    public GameObject[] room;
    public GameObject preRoom;
    // Start is called before the first frame update
    void Start()
    {
        room = new GameObject[size * size];
        int xOff = Random.Range(0, 10000);
        int yOff = Random.Range(0, 10000);
        generateRooms(xOff,yOff);
    }

    // Update is called once per frame
    
    void Update()
    {
       
    }
    void generateRooms(int xOff, int yOff)
    {
        int prog = 0;
        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                float scaler = .2f;
                float xCord = (i * scaler) + xOff;
                float yCord = (j * scaler) +yOff;
                Debug.Log((Mathf.PerlinNoise(xCord, yCord)));
                if (Mathf.PerlinNoise(xCord, yCord) <= .5)
                {
                    room[prog] = Instantiate(preRoom, new Vector3(i * 20, 0, j * 20), new Quaternion(0, 0, 0, 0));
                    
                }
                else
                {
                    room[prog] = null;
                }
                prog++;
            }
        }
        
    }
}
