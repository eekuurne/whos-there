using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject Monsters;
    [SerializeField] float TimeBeforeFirstMonster;
    MonsterGenerator monsterGenerator;
    IEnumerator startGeneratingMonsters;

    // Start is called before the first frame update
    void Start()
    {
        monsterGenerator = Monsters.GetComponent<MonsterGenerator>();
        startGeneratingMonsters = StartGeneratingMonsters();
        StartGame();
    }

    void StartGame()
    {
        StartCoroutine(startGeneratingMonsters);
    }

    void StopGame()
    {
        monsterGenerator.StopGeneratingMonsters();
    }

    IEnumerator StartGeneratingMonsters()
    {
        Debug.Log("Just about to call WaitForSecondsRealtime(TimeBeforeFirstMonster)");
        yield return new WaitForSecondsRealtime(TimeBeforeFirstMonster);
        Debug.Log("Just about to call monsterGenerator.StartGeneratingMonsters()");
        monsterGenerator.StartGeneratingMonsters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
