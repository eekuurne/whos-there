using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLost : MonoBehaviour
{

    HomeManager hm;

    void Start()
    {
        hm = GameObject.Find("HomeManager").GetComponent<HomeManager>();

    }

    void Update()
    {

        hm.PauseMenu.SetActive(false);
        hm.HUD.SetActive(false);
        if (Input.GetKeyUp(KeyCode.Escape)) { 
            SceneManager.LoadScene("Menu");
        }
    }
}
