using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
 
    public Enum.CellType celltype = Enum.CellType.Empty;

    [SerializeField] private SpriteRenderer m_gridSprite;
    public SpriteRenderer GridSprite => m_gridSprite;
    
}