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
        StartCoroutine(CheckNumberOfBats());
    }

    IEnumerator CheckNumberOfBats()
    {
        while (true)
        {
            var bat = FindObjectOfType<Bat>();
            if (bat == null)
            {
                yield return new WaitForSeconds(1f);
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ReduceTimeBetweenSpawn()
    {
        timeBetweenSpawns = 0.8f * timeBetweenSpawns;
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
