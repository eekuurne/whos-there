using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionState : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject WinOverlay;
    public GameObject LoseOverlay;
    public GameObject HUD;
    public GameObject Timer;

    public enum State
    {
        CharacterSelected, AttackConfirmation, GuardConfirmation,
        TakeCoverConfirmation, MissionComplete, MissionLost, OverlayMenu
    };

    private void Start()
    {
        PauseMenu.SetActive(false);
        WinOverlay.SetActive(false);
        LoseOverlay.SetActive(false);
        HUD.SetActive(true);
        Timer.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
        }
    }
}
