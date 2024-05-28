using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InstantiateBody_SO", menuName = "ThisGame/Sprites/InstantiateBody_SO", order = 1)]
public class InstantiateBody_SO : ScriptableObject
{
    public GameObject m_applePrefab;
    public GameObject m_bodyPrefabs;
    public GameObject m_headPrefabs;
    public GameObject m_tailPrefabs;
    public GameObject m_eatenApplePrefabs;
}
