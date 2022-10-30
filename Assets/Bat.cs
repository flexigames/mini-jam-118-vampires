using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private Villager following;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        following = FindRandomVillager();
    }

    void Update()
    {
        FollowVillager();
    }

    void FollowVillager()
    {
        var villagerPosition = following.transform.position;
        var direction = villagerPosition - transform.position;

        var distance = direction.magnitude;
        if (distance < 0.1f)
        {
            following.StopWalking();
            rigidBody.velocity = Vector2.zero;
            return;
        }

        rigidBody.velocity = direction.normalized * 5;
    }

    Villager FindRandomVillager()
    {
        var villagers = FindObjectsOfType<Villager>() as Villager[];
        if (villagers.Length == 0)
            return null;

        return villagers[Random.Range(0, villagers.Length)];
    }
}
