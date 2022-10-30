using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bat : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private Villager following;

    public TextMeshPro bloodText;

    private float blood = 0;

    bool isSucking = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        following = FindClosestVillager();
    }

    void Update()
    {
        if (isSucking)
        {
            SuckVillager();
        }
        else
        {
            FollowVillager();
        }
    }

    void SuckVillager()
    {
        blood += Time.deltaTime * 10;
        var rounded = Mathf.RoundToInt(blood);
        bloodText.text = rounded + "/100";
        if (blood >= 100)
        {
            Destroy(gameObject);
            Destroy(following.gameObject);
        }
    }

    void StartSucking()
    {
        isSucking = true;
        following.StopWalking();
        rigidBody.velocity = Vector2.zero;
    }

    void FollowVillager()
    {
        var villagerPosition = following.transform.position;
        var direction = villagerPosition - transform.position;

        var distance = direction.magnitude;
        if (distance < 0.1f)
        {
            StartSucking();
            return;
        }

        rigidBody.velocity = direction.normalized * 5;
    }

    Villager FindClosestVillager()
    {
        var villagers = FindObjectsOfType<Villager>();
        Villager closest = null;
        var closestDistance = float.MaxValue;
        foreach (var villager in villagers)
        {
            var distance = (villager.transform.position - transform.position).magnitude;
            if (distance < closestDistance)
            {
                closest = villager;
                closestDistance = distance;
            }
        }
        return closest;
    }

    Villager FindRandomVillager()
    {
        var villagers = FindObjectsOfType<Villager>() as Villager[];
        if (villagers.Length == 0)
            return null;

        return villagers[Random.Range(0, villagers.Length)];
    }
}
