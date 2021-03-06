﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject Monsters;
    [SerializeField] GameObject UI;
    [SerializeField] Player kid;
    [SerializeField] MeshRenderer[] stairCollidersMeshRenderers;
    [SerializeField] Hand hand;

    [SerializeField] float TimeBeforeFirstMonster = 1f;
    [SerializeField] float TimeBetweenMonstersBeginning = 10f;
    [SerializeField] float TimeBetweenMonstersEnd = 5f;
    MonsterGenerator monsterGenerator;

    public float GameSessionTime = 15f;
    public GameObject PauseMenu;
    public GameObject WinOverlay;
    public GameObject LoseOverlay;
    public GameObject HUD;
    public GameObject Timer;
    public bool isPaused = false;

    IEnumerator startGeneratingMonsters;
    IEnumerator sessionTimer;

    public AudioClip ThemeMusic;
    public AudioClip Monster_Death;
    public AudioClip Monster_Spawn;
    public AudioClip WinSound;
    AudioSource sound;
    AudioSource effect;

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.AddComponent<AudioSource>();
        effect = gameObject.AddComponent<AudioSource>();
        sound.clip = ThemeMusic;
        sound.volume = 0.5f;
        sound.loop = true;
        sound.playOnAwake = true;
        sound.Play();
        GameSessionTime =  GameSessionTime * 1.01f;
        monsterGenerator = Monsters.GetComponent<MonsterGenerator>();
        startGeneratingMonsters = StartGeneratingMonsters();
        sessionTimer = SessionTimer();
        StartGame();
    }

    void HideStairColliders() {
        for (int i = 0; i < stairCollidersMeshRenderers.Length; i++) {
            stairCollidersMeshRenderers[i].enabled = false;
        }
    }

    void StartGame()
    {
        StartCoroutine(sessionTimer);
        StartCoroutine(startGeneratingMonsters);
    }

    void StopGame()
    {
        StopCoroutine(sessionTimer);
        StopCoroutine(startGeneratingMonsters);
        monsterGenerator.StopGeneratingMonsters();
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        // Show Pause Screen
        PauseMenu.SetActive(true); 
        Debug.Log("PAUSE");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        Debug.Log("RESUME");
    }

    IEnumerator SessionTimer( )
    {
        float elapsedTime = 0;
        while(elapsedTime < GameSessionTime)
        {

                if (!isPaused) { elapsedTime += 1; }
                yield return new WaitForSecondsRealtime(1.0f);

                var timeDivider = elapsedTime / GameSessionTime;
                var timeBeforeNextMonster = Mathf.Lerp(TimeBetweenMonstersBeginning, TimeBetweenMonstersEnd, timeDivider);
                monsterGenerator.SetTimeBetweenMonsters(timeBeforeNextMonster);

                if (kid.GetHealth() == 0)
                {
                    // Show Lose screen
                    LoseOverlay.SetActive(true);
                    StopGame();
                    Debug.Log("YOU LOSE");
                } 
        }
        StopGame();
        effect.clip = WinSound;
        effect.Play();
        Debug.Log("TIME IS UP");
        Debug.Log("YOU WON");
        Debug.Log("RESTART THE GAME BY PRESSING R");
        // Show Win Screen

        WinOverlay.SetActive(true);
        HUD.SetActive(false);
        Timer.SetActive(false);

    }

    IEnumerator StartGeneratingMonsters()
    {
        yield return new WaitForSecondsRealtime(TimeBeforeFirstMonster);
        PlayEnemySpawnSound();
        hand.PullFingerGun();
        monsterGenerator.StartGeneratingMonsters();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R PRESSED");
            SceneManager.LoadScene("Mikko");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC PRESSED");
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P PRESSED");
            ResumeGame();
        }
    }

    public void PlayEnemySpawnSound() {
        effect.clip = Monster_Spawn;
        effect.Play();
    }

    public void EnemyDies(Transform victim, Transform attacker, RagdollCorpse ragdollPrefab) {
        effect.clip = Monster_Death;
        effect.Play();
        victim.gameObject.SetActive(false);
        /*
        RagdollCorpse ragdoll = Instantiate(ragdollPrefab, victim.transform.position + Vector3.up * 0.1f, victim.transform.rotation) as RagdollCorpse;
        Rigidbody rbHead = ragdoll.head.GetComponent<Rigidbody>();
        Rigidbody rbSpine = ragdoll.spine.GetComponent<Rigidbody>();
        Vector3 forceDirection = (victim.transform.position - attacker.position + Vector3.up).normalized;
        rbHead.AddForce(forceDirection * 100);

        StartCoroutine(ApplyForce(0.25f, rbSpine, 150, forceDirection, victim));*/
    }

    IEnumerator ApplyForce(float duration, Rigidbody target, float forceAmount, Vector3 forceDirection, Transform victim) {
        float startTime = Time.time;
        while (Time.time < startTime + duration) {
            target.AddForce(forceDirection * forceAmount * Time.deltaTime * 35);
            yield return null;
        }
    }
}
