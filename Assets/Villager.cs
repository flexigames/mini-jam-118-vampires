using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    private Vector2 direction;

    private float timeLeft = 0f;

    private Rigidbody2D rigidBody;

    private bool isWalking = true;

    public bool isSucked = false;

    public bool isTargeted = false;

    private Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Offset", Random.Range(0f, 1f));
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
