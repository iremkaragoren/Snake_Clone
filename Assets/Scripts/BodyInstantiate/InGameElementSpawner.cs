using System.Collections.Generic;
using UnityEngine;

public class InGameElementSpawner : MonoBehaviour
{
    private GridController m_gridController;
  
    public GameObject m_curentHeadObject { get; private set; }
    public GameObject m_curentAppleObject { get; private set; }
    public GameObject m_currentTailObject { get; private set; }

    public Vector2Int m_applePosition { get; private set; }
    public Vector2Int m_headPosition { get; private set; }
    public Vector2Int m_tailPosition { get; private set; }

    private readonly List<GameObject> m_bodyList = new();

    private int m_StartingSize = 2;

    [SerializeField] private InstantiateBody_SO m_instantiateBody;


    public void Initialize()
    {
    
   
        m_gridController = InitializationManager.instance.m_gridController;
        FillGrid();
    }


    private void FillGrid()
    {
        
        int width = m_gridController._width;
        int height = m_gridController._height;

        int m_headX = width / 2;
        int m_headY = height / 2;
        
        m_gridController.ChangeGridItemType(m_headX, m_headY, Enum.CellType.Head);
        m_headPosition = new Vector2Int(m_headX, m_headY);
        var m_headVecto3Position = new Vector3(m_headPosition.x, m_headPosition.y, 0f);
        m_curentHeadObject = Instantiate(m_instantiateBody.m_headPrefabs, m_headVecto3Position, Quaternion.identity);

        m_gridController.ChangeGridItemType(m_headX, m_headY-3, Enum.CellType.Tail);
       
        m_tailPosition = new Vector2Int(m_headX, m_headY - 3);
        var m_tailVector3Position = new Vector3(m_tailPosition.x, m_tailPosition.y, 0f);
        m_currentTailObject = Instantiate(m_instantiateBody.m_tailPrefabs, m_tailVector3Position, Quaternion.identity);


        for (var i = 0; i < m_StartingSize; i++)
        {
            int yOffset = m_headY - 1;
            
            m_gridController.ChangeGridItemType(m_headX, yOffset - i, Enum.CellType.Body);
            var m_bodyVector3Position = new Vector3(m_headX, yOffset - i, 0f);
            var m_curentBodyObject =
                Instantiate(m_instantiateBody.m_bodyPrefabs, m_bodyVector3Position, Quaternion.identity);
            m_bodyList.Add(m_curentBodyObject);
        }

        Apple();
    }


    private void Apple()
    {
        var m_width = m_gridController._width;
        var m_height = m_gridController._height;

        int m_appleX, m_appleY;

        do
        {
            m_appleX = Random.Range(0, m_width);
            m_appleY = Random.Range(0, m_height);
        } while (!m_gridController.IsCellEmpty(m_appleX,m_appleY)); 
        
        m_gridController.ChangeGridItemType(m_appleX,m_appleY,Enum.CellType.Apple);
        m_applePosition = new Vector2Int(m_appleX, m_appleY);
        var m_appleVector3Position = new Vector3(m_applePosition.x, m_applePosition.y, 0f);
        m_curentAppleObject = Instantiate(m_instantiateBody.m_applePrefab, m_appleVector3Position, Quaternion.identity);

        // LoopBodyListWithAction((current, index) =>
        // {
        //     current.transform.position +=Vector3.forward * index; 
        // });

        
        
    }

    public List<GameObject> GetBodyList()
    {
        return m_bodyList;
    }

    public void AddBodyElement(GameObject item)
    {
        m_bodyList.Add(item);
    }
    
    
    
 
// public void LoopBodyListWithAction(System.Action<GameObject, int> onItemChanged)
// {
//     for (int i = 0; i < m_bodyList.Count; i++)
//     {
//         var tmpItem = m_bodyList[i];
//         onItemChanged.Invoke(tmpItem, i);
//     }
// }
    
}