using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadingText : MonoBehaviour
{
    string text = "Waiting for Mr. Gonzales to wake up ";
    public TMP_Text lText;
    public bool typing = false;
    string currentString = "";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadingText());
        StartCoroutine(cursor());
    }

    IEnumerator loadingText()
    {
        for(int i = 0; i < text.Length; i++)
        {
            lText.text = lText.text.Replace("|","");
            
            if (text[i].Equals( ' '))
            {
                typing = false;
                currentString = lText.text.ToString();
                yield return new WaitForSeconds(2.25f);
                typing = true;
                lText.text = currentString + " |";
            }
            else
            {
                lText.text += text[i].ToString() + '|';
                yield return new WaitForSeconds(.2f);
            }
        }
        typing = false;
       
    }
    IEnumerator cursor()
    {
        if (!typing)
        {

            lText.text = currentString + "|";

            yield return new WaitForSeconds(.4f);

            lText.text = currentString + " ";

            yield return new WaitForSeconds(.4f);
        }
        yield return null;
        StartCoroutine(cursor());
    }
}
