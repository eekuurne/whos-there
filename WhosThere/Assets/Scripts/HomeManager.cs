using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject Monsters;
    [SerializeField] GameObject UI;
    [SerializeField] float TimeBeforeFirstMonster;
    MonsterGenerator monsterGenerator;

    public float GameSessionTime = 15f;

    IEnumerator startGeneratingMonsters;
    IEnumerator sessionTimer;

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        monsterGenerator = Monsters.GetComponent<MonsterGenerator>();
        canvas = UI.GetComponent<Canvas>();
        startGeneratingMonsters = StartGeneratingMonsters();
        sessionTimer = SessionTimer();
        StartGame();
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

    IEnumerator SessionTimer()
    {
        yield return new WaitForSecondsRealtime(GameSessionTime);
        Debug.Log("TIME IS UP");
        Debug.Log("RESTART THE GAME BY PRESSING R");
        StopGame();
    }

    IEnumerator StartGeneratingMonsters()
    {
        yield return new WaitForSecondsRealtime(TimeBeforeFirstMonster);
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
    }
}
