using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreTextAssigner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;
    private int m_score;
    public int Score => m_score;


    void Start()
    {
        m_score = 0;

    }

    private void OnEnable()
    {
        SnakeEatingController.OnSnakeGrowth += ScoreIncrease;
    }

    private void ScoreIncrease()
    {
        m_score++;
        m_scoreText.text = m_score.ToString();
    }

    private void OnDisable()
    {
        SnakeEatingController.OnSnakeGrowth -= ScoreIncrease;
    }

}
