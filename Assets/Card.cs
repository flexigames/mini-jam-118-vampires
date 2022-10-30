using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CardType
{
    BuyBat,
}

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int cost;
    public CardType type;

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
        }
    }

    void BuyBat()
    {
        var bat = Instantiate(Resources.Load("Bat")) as GameObject;
        var church = GameObject.Find("Church");
        bat.transform.position = church.transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 200f,
            transform.position.z
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - 200f,
            transform.position.z
        );
    }
}
