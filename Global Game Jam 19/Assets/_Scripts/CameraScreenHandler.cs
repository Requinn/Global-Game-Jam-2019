using Cinemachine;
using UnityEngine;

/// <summary>
/// controls camera and screen movements
/// </summary>
public class CameraScreenHandler : MonoBehaviour
{
    public CinemachineVirtualCamera[] _screenCameras;
    public CameraScreenTrigger[] _screenTriggers;

    private int _currentCameraIndex = 0;

    public void Start() {
        int index = 0;
        _screenCameras[0].Priority = 50;

        foreach(CameraScreenTrigger s in _screenTriggers) {
            s.SetIndex(index);
            s.OnCameraTrigger += SetNewCamera;
            index++;
        }
    }

    /// <summary>
    /// Set new camera to have higher priority than the old camera.
    /// </summary>
    /// <param name="index"></param>
    public void SetNewCamera(int nextCameraIndex) {
        nextCameraIndex += 1;
        if (nextCameraIndex < _screenCameras.Length) {
            _screenTriggers[_currentCameraIndex].enabled = false;
            _screenCameras[_currentCameraIndex].Priority = 0;
            _screenCameras[nextCameraIndex].Priority = 50;
            _currentCameraIndex = nextCameraIndex;
        }
    }
}
