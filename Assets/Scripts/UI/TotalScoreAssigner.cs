using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalScoreAssigner : MonoBehaviour
{
  [SerializeField] private ScoreTextAssigner m_scoreText;
  [SerializeField] private TextMeshProUGUI m_totalScoreText;

  private void OnEnable()
  {
    int score = m_scoreText.Score;
    m_totalScoreText.text = score.ToString();
  }
}
