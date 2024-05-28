using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SnakeEatingController : MonoBehaviour
{
    public static event Action OnSnakeGrowth;
    
    [SerializeField] private GridController m_gridController;
   
    [SerializeField] private InstantiateBody_SO m_instantiateBody;
    [SerializeField] private float m_moveTimeInterval = 0.2f;
    [SerializeField] private Ease m_EatEase;
    
    private SnakeMovement _mSnakeMovement;
    
    public GameObject m_eatenAppleObject;
    public Vector2Int headPosition { get; private set; }
    public Vector2Int applePosition { get; private set; }

    public GameObject currentAppleObject { get; private set; }
    public List<GameObject> bodyList { get; private set; }
    
    
   
    private void Start() {
        
        _mSnakeMovement = GetComponent<SnakeMovement>();
        
    }
    
    private void OnEnable()
    {
        SnakeMovement.OnHeadStepToApplePosition += OnSnakeEating;
    }
   

    private void OnSnakeEating()
    {
        currentAppleObject = _mSnakeMovement.currentAppleObject;
        applePosition = _mSnakeMovement.applePosition;
        
        m_gridController.ChangeGridItemType(applePosition.x, applePosition.y, Enum.CellType.Empty);
       
        var _newApplePos = new Vector2Int(Random.Range(0,m_gridController. _width), Random.Range(0, m_gridController._height));
        Vector2 _turnedApplePosition = _newApplePos;
        currentAppleObject.transform.position = _turnedApplePosition;

        EatenAppleObject();
        OnSnakeGrowth?.Invoke();
       
    }

   

    private void EatenAppleObject()
    {
        bodyList = _mSnakeMovement.bodyList;
        headPosition = _mSnakeMovement.headPosition;
        
        var _eatenApplePosition = new Vector2Int(Mathf.RoundToInt(bodyList[0].transform.position.x),
            Mathf.RoundToInt(bodyList[0].transform.position.y));
        Vector2 _ınTurnedApplePosition = _eatenApplePosition;
        m_eatenAppleObject = Instantiate(m_instantiateBody.m_eatenApplePrefabs,
            new Vector3(headPosition.x, headPosition.y, 0f),
            Quaternion.identity);
    
        var _moveDuration = m_moveTimeInterval * bodyList.Count * 1.25f;
    
        m_eatenAppleObject.transform.DOScale(Vector3.zero, _moveDuration).SetEase(m_EatEase).OnComplete(() =>
        {
            Destroy(m_eatenAppleObject.gameObject, 0.1f);
        });
    }
    
    private void OnDisable()
    {
        SnakeMovement.OnHeadStepToApplePosition -= OnSnakeEating;
    }
}
