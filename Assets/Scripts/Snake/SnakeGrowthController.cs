using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGrowthController : MonoBehaviour
{
   private SnakeMovement _mSnakeMovement;
    public GameObject currentTailObject { get; private set; }
    public List<GameObject> bodyList { get; private set; }
    
    [SerializeField] private InstantiateBody_SO m_instantiateBody;

    private void Awake()
    {
        _mSnakeMovement = GetComponent<SnakeMovement>();
    }

    private void OnEnable()
    {
        SnakeEatingController.OnSnakeGrowth += OnBodyGrowth;
    }

    private void OnBodyGrowth()
    {
        currentTailObject = _mSnakeMovement.currentTailObject;
        bodyList = _mSnakeMovement.bodyList;
        
        
        var _newBodyGameObject =
            Instantiate(m_instantiateBody.m_bodyPrefabs, currentTailObject.transform.position, Quaternion.identity);
        bodyList.Add(_newBodyGameObject);
    }

    private void OnDisable()
    {
        SnakeEatingController.OnSnakeGrowth -= OnBodyGrowth;
    }
}
