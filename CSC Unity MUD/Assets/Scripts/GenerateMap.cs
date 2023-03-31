using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GenerateMap : MonoBehaviour
{
    int size = 20;
    public GameObject[] room;
    public GameObject preRoom;
    public GameObject wall;
    public GameObject wallWithExit;
    public GameObject player;
    public float renderdist = 5;
    bool going = false;

    public GameObject createScreen;
    public GameObject sizeInput;
    public GameObject viewInput;
    public TMP_Text mathText;
    public TMP_Text viewText;

    public GameObject loadingText;
    
    // Start is called before the first frame update
   

    private void FixedUpdate()
    {
        if (going)
        {
            
            checkRenderDistance();
        }
        else
        {
            string sizeText = sizeInput.GetComponent<TMP_InputField>().text;
            string rendText = viewInput.GetComponent<TMP_InputField>().text;

            if (sizeText.Length > 0)
            {
                int.TryParse(sizeText,out size);
                mathText.text = size.ToString() + " * " + size.ToString() + " = " + (size * size).ToString() + " Rooms";
            }

            if(rendText.Length > 0)
            {
                float.TryParse(rendText,out renderdist);
                viewText.text = "Render Distance: " + rendText + " Rooms";
            }
        }
    }
    public void onCreate()
    {
        createScreen.SetActive(false);
        loadingText.SetActive(true);
        StartCoroutine(startCreation());
    }
    IEnumerator startCreation()
    {
        yield return new WaitForEndOfFrame();
        room = new GameObject[size * size];
        int xOff = Random.Range(0, 10000);
        int yOff = Random.Range(0, 10000);
        StartCoroutine(generateRooms(xOff, yOff));
    }
    IEnumerator generateRooms(int xOff, int yOff)
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
                    room[prog] = Instantiate(preRoom, new Vector3(i * 40, 0, j * 40), new Quaternion(0, 0, 0, 0));
                    
                }
                else
                {
                    room[prog] = null;
                }
                prog++;
                yield return new WaitForEndOfFrame();
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
                
                if(i >= size && room[i - size] != null)// check north
                {
                    if (room[i - size].GetComponent<Room>().walls[1] != null)
                    {
                        roomScript.walls[0] = room[i - size].GetComponent<Room>().walls[1];
                    }
                    else
                    {
                        roomScript.walls[0] = Instantiate(wallWithExit, roomScript.wallPoints[0].transform.position, roomScript.wallPoints[0].transform.rotation);
                    }
                }
                if(i <= room.Length - size - 1 && room[i+size] != null)//check south
                {
                    if (room[i + size].GetComponent<Room>().walls[0] != null)
                    {
                        roomScript.walls[1] = room[i + size];
                    }
                    else
                    {
                        roomScript.walls[1] = Instantiate(wallWithExit, roomScript.wallPoints[1].transform.position, roomScript.wallPoints[1].transform.rotation);
                    }
                }
                if(i % size != 0 && room[i-1] != null)// check east
                {
                    if (room[i - 1].GetComponent<Room>().walls[3] != null)
                    {
                        roomScript.walls[2] = room[i - 1];
                    }
                    else
                    {
                        roomScript.walls[2] = Instantiate(wallWithExit, roomScript.wallPoints[2].transform.position, roomScript.wallPoints[2].transform.rotation);
                    }
                    
                }
                if(i % size != size - 1 && room[i+1] != null)//check west
                {
                    if (room[i + 1].GetComponent<Room>().walls[2] != null)
                    {
                        roomScript.walls[3] = room[i+1]; 
                    }
                    else
                    {
                        roomScript.walls[3] = Instantiate(wallWithExit, roomScript.wallPoints[3].transform.position, roomScript.wallPoints[3].transform.rotation);
                    }
                    
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
        placePlayer();
        going = true;
    }

    void placePlayer()
    {
        int i = Random.Range(0, size*size);
        Debug.Log(i);
        if (room[i] != null)
        {
            
            Debug.Log(room[i].transform.position);

            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(room[i].transform.position.x, 1f, room[i].transform.position.z);
            player.GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            placePlayer();
        }
        
    }
    void checkRenderDistance()
    {
        

        for(int i = 0; i < room.Length; i++)
        {
            if (room[i] != null)
            {
                float dist = Vector3.Distance(player.transform.position, room[i].transform.position);
                if (dist > renderdist * 40)
                {
                    room[i].GetComponent<Room>().disableRoom();
                }
                else
                {
                    room[i].SetActive(true);
                    room[i].GetComponent<Room>().enableRoom();
                }
            }
        }
    }

    
}
