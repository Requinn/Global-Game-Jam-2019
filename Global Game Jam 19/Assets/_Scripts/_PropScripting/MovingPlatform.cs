using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject nodeCollection;
    private List<Transform> nodes;
    
    void Start()
    {
        Init();
    }
    
    void Init()
    {
        nodes = new List<Transform>();
        foreach(Transform child in transform)
            nodes.Add(child);
        gameObject.transform.position = nodes[0].transform.position;
    }
    void FixedUpdate()
    {
        
    }
}
