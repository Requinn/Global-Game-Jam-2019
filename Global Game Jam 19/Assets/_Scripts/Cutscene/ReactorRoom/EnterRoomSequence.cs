using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using MichaelWolfGames.DamageSystem;

/// <summary>
/// This sequence step is to move the camera into the proper position and revoke the input of the player.
/// </summary>
public class EnterRoomSequence : SequenceObject
{
    [SerializeField]
    private CinemachineVirtualCamera _reactorRoomCamera;
    public Transform _inspectReactorNode;

    public UnityEvent OnStartSequence;

    public override IEnumerator DoSequenceAction() {
        _affectingPlayer.GetComponent<HealthManager>().SetHealth(3);
        OnStartSequence.Invoke();
        //zoom camera in
        _reactorRoomCamera.Priority = 50;
        yield return new WaitForSeconds(0.25f);
        //move player over with external input
        float time = 0;

        while(time < 1.3f) {
            time += Time.deltaTime;
            _affectingPlayer.DoMovements(0.5f, false);
            yield return null;
        }
        yield return null;
    }
}
