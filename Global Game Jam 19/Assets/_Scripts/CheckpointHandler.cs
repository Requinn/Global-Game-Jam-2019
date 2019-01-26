using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    public static CheckpointHandler Instance;

    [SerializeField]
    private CheckPoint[] _checkpoints;
    private int _currentCheckPoint = 0;

    void Start() {
        Instance = this;
        int i = 0;
        foreach(CheckPoint p in _checkpoints) {
            p.SetIndex(i);
            p.OnChecked += UpdateCurrentCheckPoint;
            i++;
        }
    }

    /// <summary>
    /// Update the current checkpoint if the new index is past the current highest
    /// </summary>
    /// <param name="index"></param>
    void UpdateCurrentCheckPoint(int index) {
        if(index > _currentCheckPoint) {
            _currentCheckPoint = index;
        }
    }

    /// <summary>
    /// get the position of the highest chckpoint 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCurrentCheckpointPosition() {
        return _checkpoints[_currentCheckPoint].position;
    }
}
