using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    public static event Action OnSnakeEatApple;
   
    
    [SerializeField] private SnakeMovement m_snakeMovement;
    private GridController m_gridController;

   

    private void Start()
    {
        m_gridController = GetComponent<GridController>();
    }

    public void UpdateGrid()
    {
        int headX = Mathf.RoundToInt(m_snakeMovement.headPosition.x);
        int headY = Mathf.RoundToInt(m_snakeMovement.headPosition.y);
        m_gridController.ChangeGridItemType(headX, headY, Enum.CellType.Head);
        

        foreach (var bodyPosition in m_snakeMovement.bodyList)
        {
            int bodyX = Mathf.RoundToInt(bodyPosition.transform.position.x);
            int bodyY = Mathf.RoundToInt(bodyPosition.transform.position.y);
            m_gridController.ChangeGridItemType(bodyX, bodyY, Enum.CellType.Body);
        }

        int appleX = Mathf.RoundToInt(m_snakeMovement.applePosition.x);
        int appleY = Mathf.RoundToInt(m_snakeMovement.applePosition.y);
        m_gridController.ChangeGridItemType(appleX, appleY, Enum.CellType.Apple);

        int tailX = Mathf.RoundToInt(m_snakeMovement.tailPosition.x);
        int tailY = Mathf.RoundToInt(m_snakeMovement.tailPosition.y);
        m_gridController.ChangeGridItemType(tailX, tailY, Enum.CellType.Tail);
    }
}