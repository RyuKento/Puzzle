using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }
    [SerializeField] GameObject[] m_effectPrefabs;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayEffect(EventType type, Vector2 pos)
    {
        Instantiate(m_effectPrefabs[(int)type]).transform.position = pos;
    } 

}
