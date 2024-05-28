using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public static event Action OnLevelStarted;
    
    [SerializeField] private GridManager mGManager;
    [SerializeField] private GameObject m_startLevel;
    [SerializeField] private GameObject m_failLevel;
    [SerializeField] private GameObject m_inGame;
    private void OnEnable()
    {
        SnakeMovement.OnSnakeEatItself += OnLevelFailed;
    }

    private void OnLevelFailed()
    {
        m_inGame.SetActive(false);
        m_failLevel.SetActive(true);
       
        
    }

    public void LevelStart()
    {
        m_startLevel.SetActive(false);
        m_inGame.SetActive(true);
        OnLevelStarted?.Invoke();
        
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
   
    private void OnDisable()
    {
        SnakeMovement.OnSnakeEatItself -= OnLevelFailed;
    }
}
