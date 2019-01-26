using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject[] Monsters;
    public Transform[] SpawnPoints;
    public Camera PlayerCamera;

    float timeBetweenMonsters = 5f;

    IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        coroutine = InstantiateMonster();
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

    void InstantieteMonster(Vector3 obstaclePos)
    {
        var obstacleIndex = UnityEngine.Random.Range(0, Monsters.Length);
        var newMonster = Instantiate(Monsters[obstacleIndex], obstaclePos, Quaternion.identity);
        newMonster.transform.parent = this.transform;
    }

    IEnumerator InstantiateMonster()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(timeBetweenMonsters);
            Vector3 obstaclePosition = GetMonsterPosition();
            InstantieteMonster(obstaclePosition);
        }
    }

    private Vector3 GetMonsterPosition()
    {
        // TODO: tarkista ettei oo pelaajan vieressä
        var spawnIndex = UnityEngine.Random.Range(0, SpawnPoints.Length);
        return SpawnPoints[spawnIndex].position;
    }
}
