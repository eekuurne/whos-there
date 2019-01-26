using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public Enemy[] Monsters;
    public Transform[] SpawnPoints;
    public GameObject Player;

    [SerializeField] float timeBetweenMonsters = 1f;

    IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        coroutine = MonsterLoop();
    }

    public void StartGeneratingMonsters()
    {
        Debug.Log("StartGeneratingMonsters at: " + System.DateTime.Now.ToString());
        StartCoroutine(coroutine);
    }

    public void StopGeneratingMonsters()
    {
        Debug.Log("StopGeneratingMonsters at: " + System.DateTime.Now.ToString());
        StopCoroutine(coroutine);
        Clear();
    }

    public void SetTimeBetweenMonsters(float nTimeBetweenMonsters)
    {
        timeBetweenMonsters = nTimeBetweenMonsters;
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void InstantiateMonster(Vector3 obstaclePos)
    {
        var monsterIndex = UnityEngine.Random.Range(0, Monsters.Length);
        Enemy newMonster = Instantiate(Monsters[monsterIndex], obstaclePos, Quaternion.identity) as Enemy;
        newMonster.moveTarget = Player;
        newMonster.transform.parent = this.transform;
    }

    IEnumerator MonsterLoop()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(timeBetweenMonsters);
            Vector3 obstaclePosition = GetMonsterPosition();
            InstantiateMonster(obstaclePosition);
        }
    }

    private Vector3 GetMonsterPosition()
    {
        // TODO: tarkista ettei oo pelaajan vieressä
        var spawnIndex = UnityEngine.Random.Range(0, SpawnPoints.Length);
        return SpawnPoints[spawnIndex].position;
    }
}
