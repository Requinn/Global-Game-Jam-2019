using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Inidividual checkpoint object
/// </summary>
public class CheckPoint : MonoBehaviour
{
    public Vector3 position;
    private int _index = 0;
    public delegate void CheckPointAcquiredEvent(int index);
    public CheckPointAcquiredEvent OnChecked;

    void Start() {
        position = transform.position;
    }

    public void SetIndex(int newIndex) {
        _index = newIndex;
    }

    void OnTriggerEnter2D(Collider2D c) {
        OnChecked(_index);
    }
}
