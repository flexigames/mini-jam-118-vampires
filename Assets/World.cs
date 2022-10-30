using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public List<GameObject> villagerSpawnPoints;

    public float timeBetweenSpawns = 2f;

    void Start()
    {
        StartCoroutine(SpawnVillagers());
    }

    IEnumerator SpawnVillagers()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            var spawnPoint = villagerSpawnPoints[Random.Range(0, villagerSpawnPoints.Count)];
            var villager =
                Instantiate(
                    Resources.Load("Villager"),
                    spawnPoint.transform.position,
                    Quaternion.identity
                ) as GameObject;
        }
    }
}
