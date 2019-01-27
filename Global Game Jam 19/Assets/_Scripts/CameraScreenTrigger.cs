﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helps control camera transition
/// </summary>
public class CameraScreenTrigger : MonoBehaviour
{
    private int _assignedIndex = 0;
    [SerializeField]
    private GameObject _blockingWallObject;

    public delegate void CameraEventTrigger(int index);
    public CameraEventTrigger OnCameraTrigger;

    void Start() {
        _blockingWallObject.SetActive(false);
    }
    
    public void SetIndex(int newIndex){
        _assignedIndex = newIndex;
    }

    void OnTriggerExit2D(Collider2D c) {
        _blockingWallObject.SetActive(true);
        OnCameraTrigger(_assignedIndex);
    }
}