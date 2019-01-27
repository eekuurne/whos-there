using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    public float sceneDisplayTime = 20f;
    public float audioDelay = 0.5f;
    public new AudioSource audio;

    void Start()
    {
        StartCoroutine(SwitchScene());
        PlayAudio();
    }

    void Update() {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)) {
            Initiate.Fade("HomeScene", Color.white, 2.0f);
        }    
    }


    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(sceneDisplayTime);
        Initiate.Fade("HomeScene", Color.white, 2.0f);
    }

    void PlayAudio()
    {
        audio.PlayDelayed(audioDelay);
    }
}
