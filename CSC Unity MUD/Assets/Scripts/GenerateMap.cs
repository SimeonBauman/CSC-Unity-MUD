using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    int size = 20;
    public GameObject[] room;
    public GameObject preRoom;
    public GameObject wall;
    public GameObject wallWithExit;
    // Start is called before the first frame update
    void Start()
    {
        room = new GameObject[size * size];
        int xOff = Random.Range(0, 10000);
        int yOff = Random.Range(0, 10000);
        generateRooms(xOff,yOff);
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
        generateWalls();
        
    }

    void generateWalls()
    {
        for(int i = 0; i < size * size; i++)
        {
            if(room[i] != null)
            {
                var roomScript = room[i].GetComponent<Room>();
                
                if(i >= size && room[i - size] != null)
                {
                    roomScript.walls[0] = Instantiate(wallWithExit, roomScript.wallPoints[0].transform.position, roomScript.wallPoints[0].transform.rotation);
                }
                if(i <= room.Length - size - 1 && room[i+size] != null)
                {
                    roomScript.walls[1] = Instantiate(wallWithExit, roomScript.wallPoints[1].transform.position, roomScript.wallPoints[1].transform.rotation);
                }
                if(i % size != 0 && room[i-1] != null)
                {
                    roomScript.walls[2] = Instantiate(wallWithExit, roomScript.wallPoints[2].transform.position, roomScript.wallPoints[2].transform.rotation);
                }
                if(i % size != size - 1 && room[i+1] != null)
                {
                    roomScript.walls[3] = Instantiate(wallWithExit, roomScript.wallPoints[3].transform.position, roomScript.wallPoints[3].transform.rotation);
                }

                for(int j = 0; j < 4; j++)
                {
                    if(roomScript.walls[j] == null)
                    {
                        roomScript.walls[j] = Instantiate(wall, roomScript.wallPoints[j].transform.position, roomScript.wallPoints[j].transform.rotation);
                    }
                }
            }
        }
    }

}
