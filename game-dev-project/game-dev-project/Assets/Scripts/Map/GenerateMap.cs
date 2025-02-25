using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private Canvas MapManager;

    private static GenerateMap instance;

    void Awake()
    {
        //Making sure that no more than one map exists at any given time.
        //Map duplication could occur when switching between scenes.
        //By assigning a static instance to the object, we know if the map has been instantiated in the current run.
        if (instance == null) 
        {
            instance = this;
            Canvas map = Instantiate(MapManager, Vector3.zero, Quaternion.identity);
            map.name = MapManager.name;
        } 
        else 
        {
            Destroy(gameObject);
        }
    }
}
