using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject Monsters;
    [SerializeField] GameObject UI;
    [SerializeField] Player kid;

    [SerializeField] float TimeBeforeFirstMonster = 1f;
    [SerializeField] float TimeBetweenMonstersBeginning = 10f;
    [SerializeField] float TimeBetweenMonstersEnd = 5f;
    MonsterGenerator monsterGenerator;

    public float GameSessionTime = 15f;

    IEnumerator startGeneratingMonsters;
    IEnumerator sessionTimer;

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        monsterGenerator = Monsters.GetComponent<MonsterGenerator>();
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

    void PauseGame()
    {

        Time.timeScale = 0;
        // Show Pause Screen
        Debug.Log("PAUSE");
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        Debug.Log("RESUME");
    }

    IEnumerator SessionTimer( )
    {
        float elapsedTime = 0;
        while(elapsedTime < GameSessionTime)
        {
            elapsedTime += 1;
            yield return new WaitForSecondsRealtime(1.0f);

            var timeDivider = elapsedTime / GameSessionTime;
            var timeBeforeNextMonster = Mathf.Lerp(TimeBetweenMonstersBeginning, TimeBetweenMonstersEnd, timeDivider);
            monsterGenerator.SetTimeBetweenMonsters(timeBeforeNextMonster);

            if (kid.GetHealth() == 0)
            {
                // Show Lose screen
                StopGame();
                Debug.Log("YOU LOSE");
            }
        }
        StopGame();
        Debug.Log("TIME IS UP");
        Debug.Log("YOU WON");
        Debug.Log("RESTART THE GAME BY PRESSING R");
        // Show Win Screen

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
}
