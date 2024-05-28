using System;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int _width { get; } = 25;

    public int _height { get; } = 20;

    private GridItem[,] m_grid;
    [SerializeField] private GridItem m_GridBackground;
    [SerializeField] private Color m_LightGridColor;
    [SerializeField] private Color m_DarkGridColor;


    public void Initialize()
    {
        InitializeGridBackground();
    }

    private void InitializeGridBackground()
    {
        m_grid = new GridItem[_width, _height];
        
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var tmpGridItem = Instantiate(m_GridBackground, new Vector2(x, y), Quaternion.identity);
                tmpGridItem.transform.SetParent(transform);
                var gridColor = (y + x) % 2 == 0 ? m_LightGridColor : m_DarkGridColor;

                m_grid[x, y] = tmpGridItem;
                tmpGridItem.celltype = Enum.CellType.Empty;
                tmpGridItem.GridSprite.color = gridColor;
            }
        }
      
    }

    public GridItem[,] GetGrid()
    {
        return m_grid;
    }

    public Enum.CellType GetGridItemType(int x, int y)
    {
        return m_grid[x, y].celltype;
    }

    public void ChangeGridItemType(int x, int y, Enum.CellType type)
    {
        m_grid[x, y].celltype = type;
    }

    public bool IsCellEmpty(int x, int y) => m_grid[x, y].celltype != Enum.CellType.Body;


}