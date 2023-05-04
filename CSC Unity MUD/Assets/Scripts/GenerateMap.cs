using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GenerateMap : MonoBehaviour
{
    int size = 20;
    public GameObject[] room;
    public GameObject preRoom;
    public GameObject wall;
    public GameObject wallWithExit;
    public GameObject player;
    public GameObject coolDude;
    public float renderdist = 5;
    bool going = false;

    public GameObject monster;
    public GameObject startScene;
    public GameObject loadingText;
    public GameObject HealthBar;

    public GameObject Teleporter;
    // Start is called before the first frame update
    private void Start()
    {
        //Random.InitState(Data.RSeed);
        this.renderdist = Data.renderDist;
        this.size = Data.mapSize;
        HealthBar.SetActive(false);
        startCreation();

    }

    private void FixedUpdate()
    {
        if (going)
        {
            
           checkRenderDistance();
        }
        
    }
    
    void startCreation()
    {
        
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
                        roomScript.walls[1] = room[i + size].GetComponent<Room>().walls[0];
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
                        roomScript.walls[2] = room[i - 1].GetComponent<Room>().walls[3];
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
                        roomScript.walls[3] = room[i+1].GetComponent<Room>().walls[2]; 
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
        generateEnemies();
        placePlayer();
        placeTeleporter();
        Data.mapReady = true;
        //StartCoroutine(checkRenderDistance());
        going = true;
    }

    void placePlayer()
    {
        int i = Random.Range(0, size*size);

        loadingText.SetActive(false);
        Debug.Log(i);
        if (room[i] != null)
        {
            
            Debug.Log(room[i].transform.position);
            HealthBar.SetActive(true);
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(room[i].transform.position.x, 2f, room[i].transform.position.z);
            player.GetComponent<CharacterController>().enabled = true;
            var ene = room[i].GetComponent<Room>().enemies;
            for(int j = 0; j < ene.Length; j++)
            {
                if(ene[j] != null)
                {
                    
                        ene[j].GetComponent<EnemyBrain>().souls = -1;
                    
                    
                    ene[j].GetComponent<EnemyBrain>().health = 0;
                }
            }
            Vector3 pos = new Vector3(player.transform.position.x,2,player.transform.position.z);
            GameObject g =Instantiate(startScene,pos, player.transform.rotation);
            var s = g.GetComponent<StartGame>();
            s.player = player;
            coolDude = Instantiate(coolDude, new Vector3(Random.Range(5,9) + pos.x,2, Random.Range(5, 9) + pos.z), Quaternion.identity);
            coolDude.GetComponent<CoolDudeBrain>().player = player;
            StartCoroutine(s.startCut());
        }
        else
        {
            placePlayer();
        }
        
    }
    
    void generateEnemies()
    {
        for(int i = 0; i < size * size; i++)
        {
            if(room[i] != null)
            {
                GameObject g = room[i];
                int r = Random.Range(1, 5);
                for(int j = 0; j < r; j++)
                {
                    
                   
                        GameObject m = Instantiate(monster, g.transform);
                        m.transform.localPosition = new Vector3(Random.Range(-9.5f, 9.5f), 2, Random.Range(-9.5f, 9.5f));
                        m.transform.localScale = new Vector3(1, 2, 1);
                        m.GetComponent<EnemyBrain>().player = this.player;
                        g.GetComponent<Room>().enemies[j] = m;
                        g.GetComponent<Room>().liveInhabs += 1;
                    
                }

            }
        }
    }

    void placeTeleporter()
    {
        int i = Random.Range(0, size * size);

        if (room[i] != null)
        {
            Instantiate(Teleporter, room[i].transform.position,Quaternion.identity);
        }
        else
        {
            placeTeleporter();
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
            
            /*if(i % 10 == 0)
            {
                yield return null;
            }*/
        }
        //StartCoroutine(checkRenderDistance());
    }

    
}
