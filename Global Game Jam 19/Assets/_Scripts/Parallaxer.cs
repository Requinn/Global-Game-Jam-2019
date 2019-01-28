using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Revised by Peter Doria to add an offset to the parallax to put the backgrounds in the right place in play.

public class Parallaxer : MonoBehaviour
{
    [Header("Parallax will happen automatically when the object is placed between z=0 and z=10")]
    public Transform ReferencePoint;
    [SerializeField] private bool _useHorizontal = true;
    [SerializeField] private bool _useVertical = false;

    private float _maxDepth = 10f;
    private Vector3 _startPos;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    

	void Start ()
	{
        _startPos = this.transform.position; 
        if (!ReferencePoint) ReferencePoint = Camera.main.transform; 
	}

    void LateUpdate ()
	{

	    var deltaX = ReferencePoint.position.x - (_startPos.x + _xOffset);
	    var deltaY = ReferencePoint.position.y - (_startPos.y + _yOffset);
        var depth = this.transform.position.z;
	    if (depth < 0) depth = 0f;
	    var dLevel = Mathf.Clamp(depth / _maxDepth, 0f, 1f);

	    this.transform.position = _startPos + new Vector3((_useHorizontal) ? deltaX * dLevel : 0f, (_useVertical) ? deltaY * dLevel : 0f, 0f);
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var projPos = Vector3.ProjectOnPlane(transform.position, Vector3.forward);
        Gizmos.DrawLine(projPos, projPos + Vector3.forward*_maxDepth);
        Gizmos.DrawWireCube(projPos + Vector3.forward * _maxDepth/2f, new Vector3(_maxDepth, _maxDepth,_maxDepth));
    }
}
