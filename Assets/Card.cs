using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public enum CardType
{
    BuyBat,
    WinGame,
    SpawnVillagers,
    IncreaseBatSpeed,
}

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int cost;
    public CardType type;

    public TextMeshProUGUI costText;

    private Image image;

    void Start()
    {
        costText.text = cost + "";
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (BloodScore.score < cost)
            return;

        BloodScore.Sub(cost);

        switch (type)
        {
            case CardType.BuyBat:
                BuyBat();
                break;
            case CardType.WinGame:
                WinGame();
                break;
            case CardType.SpawnVillagers:
                SpawnVillagers();
                break;
            case CardType.IncreaseBatSpeed:
                IncreaseBatSpeed();
                break;
        }
    }

    void Update()
    {
        if (BloodScore.score < cost)
        {
            image.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        }
        else
        {
            image.color = Color.white;
        }
    }

    void IncreaseBatSpeed()
    {
        Bat.flySpeed = Bat.flySpeed * 1.5f;
        Bat[] bats = FindObjectsOfType<Bat>();
        foreach (Bat bat in bats)
        {
            bat.speed = Bat.flySpeed;
        }
    }

    void SpawnVillagers()
    {
        var world = FindObjectOfType<World>();
        world.ReduceTimeBetweenSpawn();
    }

    void BuyBat()
    {
        var bat = Instantiate(Resources.Load("Bat")) as GameObject;
        var church = GameObject.Find("Church");
        bat.transform.position = church.transform.position;
    }

    void WinGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y + 200f,
            transform.localPosition.z
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y - 200f,
            transform.localPosition.z
        );
    }
}
