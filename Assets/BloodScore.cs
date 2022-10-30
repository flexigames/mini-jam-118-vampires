using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BloodScore : MonoBehaviour
{
    public static float score = 0;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    static public void Add(float amount)
    {
        score += amount;
    }

    static public void Sub(float amount)
    {
        score -= amount;
    }

    void Update()
    {
        var rounded = Mathf.Floor(score);
        scoreText.text = "blood : " + rounded + "l";
    }
}
