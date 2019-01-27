using System.Collections;
using Cinemachine;
using UnityEngine;


/// <summary>
/// This sequence step is to move the camera into the proper position and revoke the input of the player.
/// </summary>
public class EnterRoomSequence : SequenceObject
{
    [SerializeField]
    private CinemachineVirtualCamera _reactorRoomCamera;
    public Transform _inspectReactorNode;

    public override IEnumerator DoSequenceAction() {
        //stop the player
        _affectingPlayer.CanDoInputs = false;
        //zoom camera in
        _reactorRoomCamera.Priority = 50;
        yield return new WaitForSeconds(1f);
        //move player over with external input
        while(_affectingPlayer.transform.position != _inspectReactorNode.position) {
            _affectingPlayer.DoMovements(1, false);
            yield return null;
        }
        yield return null;
    }
}
