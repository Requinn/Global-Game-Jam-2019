using System;
using System.Collections;
using UnityEngine;


/// <summary>
/// Base class for sequences that should play during a cutscene
/// </summary>
[Serializable]
public class SequenceObject : MonoBehaviour
{
    [Header("Cutscene Properties")]
    [SerializeField]
    protected float _startDelay = 0f, _endDelay = 0f, _sequenceDuration;
    public float Duration => _sequenceDuration;
    public float StartTime => _startDelay;
    public float EndTime => _endDelay;
    [Header("Step specific properties")]
    protected Character _affectingPlayer; 

    public virtual IEnumerator DoSequenceAction() {
        Debug.Log("this is empty");
        yield return null;
    }
}
