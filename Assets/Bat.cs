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

    public BatState state = BatState.FollowVillager;

    private Animator bubbleAnimator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bubbleAnimator = GetComponentInChildren<Animator>();
        following = FindClosestVillager();
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
            state = BatState.FollowVillager;
            following = FindClosestVillager();
            return;
        }

        blood -= Time.deltaTime * suckSpeed;
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
        state = BatState.ReturnToChurch;
        Destroy(following.gameObject);
        following = FindClosestVillager();
    }

    void SuckVillager()
    {
        blood += Time.deltaTime * suckSpeed;
        UpdateBloodText();
        UpdateBubble();

        if (blood >= 100)
        {
            Destroy(gameObject);
            Destroy(following.gameObject);
        }
    }

    void UpdateBubble()
    {
        bubbleAnimator.Play("Bubble", 0, blood / 100f);
    }

    void UpdateBloodText()
    {
        var rounded = Mathf.RoundToInt(blood);
        bloodText.text = rounded + "/100";
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

    Villager FindClosestVillager()
    {
        var villagers = FindObjectsOfType<Villager>();
        Villager closest = null;
        var closestDistance = float.MaxValue;
        foreach (var villager in villagers)
        {
            if (villager.isSucked)
                continue;
            var distance = (villager.transform.position - transform.position).magnitude;
            if (distance < closestDistance)
            {
                closest = villager;
                closestDistance = distance;
            }
        }
        return closest;
    }
}
