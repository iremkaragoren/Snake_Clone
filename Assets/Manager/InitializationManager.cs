using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InitializationManager : MonoBehaviour
{
    public static InitializationManager instance;
    [SerializeField] public GridController m_gridController;
    [SerializeField] public InGameElementSpawner m_inGameElementSpawner;

    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        m_gridController.Initialize();
        m_inGameElementSpawner.Initialize();
        
    }
}
