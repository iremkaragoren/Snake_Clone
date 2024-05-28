using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public static event Action OnHeadStepToApplePosition;
    public static event Action OnSnakeEatItself;
    
    public static event Action<Vector2Int, SpriteRenderer> OnTailDirectionChanged;
    public static event Action<Vector2Int, SpriteRenderer> OnHeadDirectionChanged;
    public static event Action<GameObject, Vector2Int, Vector2Int> OnSnakeDirectionChanged;

   [SerializeField] private InGameElementSpawner m_instantiateController;
   private GridController m_gridController;
   private GridItem[,] m_grid;
    [SerializeField]private GridManager m_gridManager;

    public Vector2Int headPosition { get; private set; }
    public Vector2Int tailPosition { get; private set; }
    public Vector2Int applePosition { get; private set; }
    public GameObject currentHeadObject { get; private set; }
    public GameObject currentAppleObject { get; private set; }
    public GameObject currentTailObject { get; private set; }
    public List<GameObject> bodyList { get; private set; }


    private int width;
    private int height;
    private float m_timer;
    [SerializeField] private float m_moveTimeInterval = 0.2f;

    private Vector2Int _direction = Vector2Int.up;
    private Vector2Int _previousDirection;
    private Vector2Int m_previousHeadPosition;
    private Vector2Int m_newHeadPosition;

    private bool m_isGameActive;

   

    private void Start()
    {
        
        m_gridController = InitializationManager.instance.m_gridController;
        Initialize();
    }


    private void Initialize()
    {
        
        m_isGameActive = true;
        InstantiateValue();
    }


    private void InstantiateValue()
    {
        headPosition = m_instantiateController.m_headPosition;
        tailPosition = m_instantiateController.m_tailPosition;
        applePosition = m_instantiateController.m_applePosition;
        currentHeadObject = m_instantiateController.m_curentHeadObject;
        currentAppleObject = m_instantiateController.m_curentAppleObject;
        currentTailObject = m_instantiateController.m_currentTailObject;
        bodyList = m_instantiateController.GetBodyList();
        width = m_gridController._width;
        height = m_gridController._height;
    }

    private void Update()
    {
        if (!m_isGameActive)
            return;
        
        if (m_timer >= m_moveTimeInterval)
        {
            SnakeMoveInput();
            
            if (!CanMove(_direction))
            {
                m_isGameActive = false;
                OnSnakeEatItself?.Invoke();
                return;
            }
          
            SnakeMovementWithDirection();
            m_gridManager.UpdateGrid();
            m_timer = 0;
        }

        m_timer += Time.deltaTime;
    }

    public void SnakeMoveInput()
    {
        _previousDirection = _direction;

        if (Input.GetKey(KeyCode.LeftArrow))
            _direction = Vector2Int.left;

        if (Input.GetKey(KeyCode.RightArrow))
            _direction = Vector2Int.right;

        if (Input.GetKey(KeyCode.UpArrow))
            _direction = Vector2Int.up;

        if (Input.GetKey(KeyCode.DownArrow))
            _direction = Vector2Int.down;
    }

    private void SnakeMovementWithDirection()
    {
        if (IsReversedDirection()) _direction = _direction * -1;

        MoveSnake(_direction);
    }

    private bool IsReversedDirection()
    {
        return _previousDirection * -1 == _direction;
    }

    private bool CanMove(Vector2Int direction)
    {
        if (IsReversedDirection())
            return true;
        

        var x = (headPosition.x + direction.x) % width;
        var y = (headPosition.y + direction.y) % height;

        if (x < 0)
            x = width - 1;

        if (y < 0)
            y = height - 1;

        // return m_grid[x, y].celltype != Enum.CellType.Body;
        return (m_gridController.IsCellEmpty(x,y));
    }


    private void MoveSnake(Vector2Int direction)
    {
        m_previousHeadPosition = headPosition;
        m_newHeadPosition = headPosition + direction;

        if (new Vector3(m_newHeadPosition.x, m_newHeadPosition.y, 0) == currentAppleObject.transform.position)
            OnHeadStepToApplePosition?.Invoke();

        CheckScreenWrap();

        m_gridController.ChangeGridItemType(headPosition.x, headPosition.y, Enum.CellType.Body);
        
        Vector2 _lastBodyListObject = bodyList[bodyList.Count - 1].transform.position;

        int _lastPosObjX = Mathf.RoundToInt(_lastBodyListObject.x);
        int _lastPosObjY = Mathf.RoundToInt(_lastBodyListObject.y);

        GameObject _lastBody = bodyList[bodyList.Count - 1];
        _lastBody.transform.position =
            new Vector2(m_previousHeadPosition.x, m_previousHeadPosition.y);

        bodyList.RemoveAt(bodyList.Count - 1);
        bodyList.Insert(0, _lastBody);

        currentTailObject.transform.position = new Vector2(_lastPosObjX, _lastPosObjY);

        headPosition = m_newHeadPosition;

        m_gridController.ChangeGridItemType(m_newHeadPosition.x, m_newHeadPosition.y, Enum.CellType.Head);

        currentHeadObject.transform.position = new Vector2(m_newHeadPosition.x, m_newHeadPosition.y);

        m_gridController.ChangeGridItemType(_lastPosObjX, _lastPosObjY, Enum.CellType.Tail);

        Vector2 tailDirection =
            currentTailObject.transform.position - bodyList[bodyList.Count - 1].transform.position;


        Vector2Int _lastBodyPosition = new Vector2Int(
            Mathf.RoundToInt(bodyList[bodyList.Count - 1].transform.position.x)
            , Mathf.RoundToInt(bodyList[bodyList.Count - 1].transform.position.y));

        Vector2Int _secondBodyPosition = new Vector2Int(
            Mathf.RoundToInt(bodyList[bodyList.Count - 2].transform.position.x)
            , Mathf.RoundToInt(bodyList[bodyList.Count - 2].transform.position.y));

        Vector2Int _lastBodyDirection = _secondBodyPosition - _lastBodyPosition;

        if (_lastBodyDirection != tailDirection)
            _lastBodyDirection = -new Vector2Int(Mathf.RoundToInt(tailDirection.x), Mathf.RoundToInt(tailDirection.y));

        OnSnakeDirectionChanged?.Invoke(_lastBody, _previousDirection, _direction);
        OnHeadDirectionChanged?.Invoke(direction, currentHeadObject.GetComponent<SpriteRenderer>());
        OnTailDirectionChanged?.Invoke(-_lastBodyDirection, currentTailObject.GetComponent<SpriteRenderer>());
    }

    private void CheckScreenWrap()
    {
        if (m_newHeadPosition.x < 0)
            m_newHeadPosition.x = width - 1;
        else if (m_newHeadPosition.x >= width) m_newHeadPosition.x = 0;

        if (m_newHeadPosition.y < 0)
            m_newHeadPosition.y = height - 1;
        else if (m_newHeadPosition.y >= height) m_newHeadPosition.y = 0;
    }
}