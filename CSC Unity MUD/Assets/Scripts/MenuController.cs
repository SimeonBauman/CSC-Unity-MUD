using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public GameObject sizeInput;
    public GameObject viewInput;
    public TMP_Text mathText;
    public TMP_Text viewText;

    int size;
    float renderDist;

    private void Start()
    {
        this.size = Data.mapSize;
        this.renderDist = Data.renderDist;
        Data.mapReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        string sizeText = sizeInput.GetComponent<TMP_InputField>().text;
        string rendText = viewInput.GetComponent<TMP_InputField>().text;

        if (sizeText.Length > 0)
        {
            int.TryParse(sizeText, out size);
            mathText.text = size.ToString() + " * " + size.ToString() + " = " + (size * size).ToString() + " Rooms";
        }

        if (rendText.Length > 0)
        {
            float.TryParse(rendText, out renderDist);
            viewText.text = "Render Distance: " + rendText + " Rooms";
        }
    }

    public void onCreate()
    {
        Data.mapSize = size;
        Data.renderDist = renderDist;
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }
}
