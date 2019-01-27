using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneSubtitles : MonoBehaviour
{
    public float letterPause = 0.1f;
    public AudioClip typeSound1;
    public AudioClip typeSound2;

    string message;
    Text textComp;

    // Use this for initialization
    void Start()
    {
        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            yield return new WaitForSecondsRealtime(letterPause);
        }
    }
}
