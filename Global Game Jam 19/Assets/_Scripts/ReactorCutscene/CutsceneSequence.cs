using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSequence : MonoBehaviour
{
    [SerializeField]
    private SequenceObject[] _sequence;

    private int _currentStep = 0;

    public void StartSequence() {
        StartCoroutine(DoSequence());
    }

    /// <summary>
    /// Perform every sequence action provided
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoSequence() {
        foreach(SequenceObject step in _sequence) {
            yield return new WaitForSeconds(step.StartTime);
            StartCoroutine(step.DoSequenceAction());
            yield return new WaitForSeconds(step.Duration + step.EndTime);
            yield return null;
        }
        yield return null;
    }
}