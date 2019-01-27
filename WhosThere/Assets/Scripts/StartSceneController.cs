using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public float scenePause = 10f;
    public AudioSource audio;

    void Start()
    {
        StartCoroutine(SwitchScene());
        PlayAudio();
    }


    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(scenePause);
        SceneManager.LoadScene("HomeScene");
    }

    void PlayAudio()
    {
        audio.PlayDelayed(0.5f);
    }
}
