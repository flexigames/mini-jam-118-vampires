using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BatState
{
    FollowVillager,
    SuckBlood,
    ReturnToChurch,
    DropBlood,
}

public class Bat : MonoBehaviour, MouseInteractable
{
    public float suckSpeed = 20f;
    private Rigidbody2D rigidBody;

    private Villager following;

    public TextMeshPro bloodText;

    private float blood = 0;

    private float maxBlood = 10;

    public BatState state = BatState.FollowVillager;

    private Animator bubbleAnimator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bubbleAnimator = GetComponentInChildren<Animator>();
        var target = FindVillagerTarget();
        FollowVillager(target);
    }

    void FollowVillager(Villager villager)
    {
        following = villager;
        villager.isTargeted = true;
        state = BatState.FollowVillager;
    }

    void Update()
    {
        if (state == BatState.FollowVillager)
        {
            FollowVillager();
        }
        if (state == BatState.SuckBlood)
        {
            SuckVillager();
        }
        if (state == BatState.ReturnToChurch)
        {
            ReturnToChurch();
        }
        if (state == BatState.DropBlood)
        {
            DropBlood();
        }
    }

    void DropBlood()
    {
        if (blood <= 0)
        {
            blood = 0;
            FollowVillager(FindVillagerTarget());
            return;
        }

        var bloodAmount = Time.deltaTime * suckSpeed;
        blood -= bloodAmount;
        BloodScore.Add(bloodAmount);

        UpdateBloodText();
        UpdateBubble();
    }

    void ReturnToChurch()
    {
        var church = GameObject.Find("Church");
        var churchPosition = church.transform.position;
        var direction = churchPosition - transform.position;
        rigidBody.velocity = direction.normalized * 5f;

        if (Vector2.Distance(transform.position, churchPosition) < 0.5f)
        {
            state = BatState.DropBlood;
            rigidBody.velocity = Vector2.zero;
        }
    }

    public void OnMouseClick()
    {
        if (state != BatState.SuckBlood)
            return;

        state = BatState.ReturnToChurch;
        Destroy(following.gameObject);
        following = FindVillagerTarget();
    }

    void SuckVillager()
    {
        blood += Time.deltaTime * suckSpeed;
        UpdateBloodText();
        UpdateBubble();

        if (blood >= maxBlood)
        {
            blood = maxBlood;
            Destroy(gameObject);
            Destroy(following.gameObject);
        }
    }

    void UpdateBubble()
    {
        bubbleAnimator.Play("Bubble", 0, blood / maxBlood);
    }

    void UpdateBloodText()
    {
        var rounded = Mathf.RoundToInt(blood);
        bloodText.text = rounded + "/" + maxBlood;
    }

    void StartSucking()
    {
        state = BatState.SuckBlood;
        following.StopWalking();
        following.isSucked = true;
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

    Villager FindVillagerTarget()
    {
        var villagers = FindObjectsOfType<Villager>();
        var possibleTargets = new List<Villager>();
        foreach (var villager in villagers)
        {
            if (!villager.isTargeted)
            {
                possibleTargets.Add(villager);
            }
        }

        if (possibleTargets.Count == 0)
        {
            return null;
        }

        var randomIndex = Random.Range(0, possibleTargets.Count);
        return possibleTargets[randomIndex];
    }
}
