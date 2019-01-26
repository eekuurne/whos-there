using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayMenuUI : MonoBehaviour
{  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
             //disable overlay menu
        }
    }
     
    public void GoBack() {

    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
