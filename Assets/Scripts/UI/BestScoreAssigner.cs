using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


public class BestScoreAssigner : MonoBehaviour
{
    [SerializeField] private ScoreTextAssigner m_scoreTextAssigner;

    [SerializeField] private TextMeshProUGUI m_bestScoreText;

    private int m_bestScore;

    private const string HIGH_SCORE_PREFS = "HighScore";

    private void OnEnable()
    {
        OnBestScoressigned();
    }

    private void OnBestScoressigned()
    {
        m_bestScore = PlayerPrefs.GetInt(HIGH_SCORE_PREFS, 0);
        int score = m_scoreTextAssigner.Score;

        if (score >= m_bestScore)
        {
            m_bestScore = score;
            PlayerPrefs.SetInt(HIGH_SCORE_PREFS, score);
            m_bestScoreText.text = m_bestScore.ToString();
        }
        else
        {
            m_bestScoreText.text = m_bestScore.ToString();
        }
    }
}