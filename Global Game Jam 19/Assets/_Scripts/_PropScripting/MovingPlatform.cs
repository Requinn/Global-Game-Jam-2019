using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject nodeCollection;
    
    [SerializeField] private float travelSpeed = 1f;
    [SerializeField] private float slerpTolerance = .001f;
    private float startTime;
    
    private List<Transform> nodes;
    private int nextNode = 1;
    private bool reverse;
    
    void Start()
    {
        Init();
    }
    
    void Init()
    {
        IEnumerable<Transform> children = nodeCollection.transform.Cast<Transform>();
        nodes = children.ToList();
        Debug.Log(nodes.Count);
        gameObject.transform.position = nodes[0].transform.position;
        Debug.Log("Position set to " + nodes[0].transform.localPosition);
    }
    
    void Update()
    {
        if (nextNode + 1 == nodes.Count)
            reverse = true;
        else if (nextNode - 1 < 0 && reverse)
            reverse = false;
        //approx. comparison of vector3 positions
        if (V3Equal(transform.position,nodes[nextNode].position))
        {
            Debug.Log("PROXIMITY REACHED");
            if (reverse)
                nextNode--;
            else
                nextNode++;
        }

        Debug.Log(nextNode);
        transform.position  = Vector3.MoveTowards(transform.position, nodes[nextNode].position, 
            Time.smoothDeltaTime * travelSpeed);
        
        //Debug.Log(transform.localPosition +" : "+ nodes[nextNode].localPosition);
    }

    /*void TraverseNodes()
    {
        if (nextNode + 1 == nodes.Count)
            forward = false;
        else if (nextNode - 1 < 0 && !forward)
            forward = true;

        //approx. comparison of vector3 positions
        if (transform.position == nodes[nextNode].position)
        {
            nextNode = (forward) ? nextNode++ : nextNode--;
        }
        
        Debug.Log(transform.localPosition +" : "+ nodes[nextNode].localPosition);
        Vector3.MoveTowards(transform.position, nodes[nextNode].position, 
            Time.smoothDeltaTime * travelSpeed);


    }*/
    
    //thanks joel
    private bool V3Equal(Vector3 a, Vector3 b) {
        return Vector3.SqrMagnitude(a - b) < slerpTolerance;
    }
}
