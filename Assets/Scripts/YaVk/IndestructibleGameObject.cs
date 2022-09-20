using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndestructibleGameObject : MonoBehaviour
{
    private static IndestructibleGameObject _instance;

    private void Awake()
    {
        if (_instance == null) 
        {
            _instance = this;
            DontDestroyOnLoad(this);
        } 
        else 
        {
            DestroyImmediate(gameObject);
        } 
    }
}
