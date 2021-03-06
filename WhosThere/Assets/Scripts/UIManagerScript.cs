﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{

    private void Start()
    {
        Cursor.visible = true;
    }


    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    
    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }


    public void Credits() {
        SceneManager.LoadScene("Credits");
    }
}