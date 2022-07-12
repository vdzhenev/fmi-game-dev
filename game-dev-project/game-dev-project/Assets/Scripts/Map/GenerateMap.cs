using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private Canvas MapManager;

    private static GenerateMap instance;

    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            Canvas map = Instantiate(MapManager, Vector3.zero, Quaternion.identity);
            map.name = MapManager.name;
        } 
        else 
        {
            DestroyObject(gameObject);
        }
    }
}
