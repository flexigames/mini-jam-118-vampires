using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    private Vector2 direction;

    private float timeLeft = 0f;

    private Rigidbody2D rigidBody;

    private bool isWalking = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isWalking)
            Walk();
    }

    void Walk()
    {
        if (timeLeft <= 0f)
        {
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft = Random.Range(3f, 5f);
        }

        timeLeft -= Time.deltaTime;
        rigidBody.velocity = direction;
    }

    public void StopWalking()
    {
        isWalking = false;
        rigidBody.velocity = Vector2.zero;
    }

    void StartWalking()
    {
        isWalking = true;
    }
}
