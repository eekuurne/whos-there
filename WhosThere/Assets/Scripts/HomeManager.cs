using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject Monsters;
    [SerializeField] GameObject UI;
    [SerializeField] Player kid;
    [SerializeField] MeshRenderer[] stairCollidersMeshRenderers;

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

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
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

    public void EnemyDies(Transform victim, Transform attacker, RagdollCorpse ragdollPrefab) {
        victim.gameObject.SetActive(false);
        RagdollCorpse ragdoll = Instantiate(ragdollPrefab, victim.transform.position + Vector3.up * 0.1f, victim.transform.rotation) as RagdollCorpse;
        Rigidbody rbHead = ragdoll.head.GetComponent<Rigidbody>();
        Rigidbody rbSpine = ragdoll.spine.GetComponent<Rigidbody>();
        Vector3 forceDirection = (victim.transform.position - attacker.position + Vector3.up).normalized;
        rbHead.AddForce(forceDirection * 600);

        StartCoroutine(ApplyForce(0.25f, rbSpine, 250, forceDirection, victim));
    }

    IEnumerator ApplyForce(float duration, Rigidbody target, float forceAmount, Vector3 forceDirection, Transform victim) {
        float startTime = Time.time;
        while (Time.time < startTime + duration) {
            target.AddForce(forceDirection * forceAmount * Time.deltaTime * 35);
            yield return null;
        }
        StartCoroutine(FadeoutEnemy(2f, victim));
    }

    IEnumerator FadeoutEnemy(float duration, Transform victim) {
        RagdollCorpse victimRagdoll = victim.GetComponent<RagdollCorpse>();
        float startTime = Time.time;
        while (Time.time < startTime + duration) {
            victimRagdoll.ChangeToFadeMaterial();
            Color color = victimRagdoll.fadeMaterial.color;
            color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * 5);
            victimRagdoll.fadeMaterial.color = color;
            yield return null;
        }
        Destroy(victim.gameObject);
    }
}
