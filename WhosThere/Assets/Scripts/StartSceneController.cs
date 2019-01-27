using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public float scenePause = 10f;

    void Start()
    {
        StartCoroutine(SwitchScene());
    }


    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(scenePause);
        SceneManager.LoadScene("HomeScene");
    }
}
