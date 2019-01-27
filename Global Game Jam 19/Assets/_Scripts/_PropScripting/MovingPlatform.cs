using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool activateOnPlayerTouch;
    private bool active;
    
    [SerializeField] private GameObject nodeCollection;
    [SerializeField] private float travelSpeed = 1f;
    [SerializeField] private float slerpTolerance = .001f;
    [SerializeField] private int startingNode = 0;
    [SerializeField] private float platformDelay = 0f;
    private float startTime;
    //public bool returnToStartOption; SHELVED
    
    private List<Transform> nodes;
    private int nextNode;
    public bool reverse;
    
    void Start()
    {
        Init();
    }
    
    void Init()
    {
        IEnumerable<Transform> children = nodeCollection.transform.Cast<Transform>();
        nodes = children.ToList();

        gameObject.transform.position = nodes[startingNode].transform.position;
        if (startingNode == nodes.Count - 1)
        {
            nextNode = startingNode - 1;
            reverse = true;
        }
        else
        {
            nextNode = startingNode + 1;
        }
    }
    
    void Update()
    {
        if (!active)
            return;
        //if (!returnToStartOption) ShElVeD
        NormalMovement();

    }

    void NormalMovement()
    {
        if (nextNode + 1 == nodes.Count)
            reverse = true;
        else if (nextNode - 1 < 0 && reverse)
            reverse = false;

        if (V3Equal(transform.position, nodes[nextNode].position))
        {
            if(nextNode == 0 || nextNode == nodes.Count - 1)
                StartCoroutine(Delay(platformDelay));
            if (reverse)
                nextNode--;
            else
                nextNode++;
        }

        transform.position = Vector3.MoveTowards(transform.position, nodes[nextNode].position,
            Time.smoothDeltaTime * travelSpeed);
    }

    /*void ReturnToStartMovement() s h e l v e d
    {
        if (nextNode + 1 == nodes.Count)
            active = false;
    }*/

    //thanks joel
    private bool V3Equal(Vector3 a, Vector3 b) {
        return Vector3.SqrMagnitude(a - b) < slerpTolerance;
    }

    public void Toggle()
    {
        active = !active;
    }

    IEnumerator Delay(float delay)
    {
        active = false;
        yield return new WaitForSeconds(delay);
        active = true;
    }
}
