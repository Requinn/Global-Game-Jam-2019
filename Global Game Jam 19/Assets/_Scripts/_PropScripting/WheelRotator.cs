using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    //public Vector2 Velocity = new Vector2(1, 0);
    [SerializeField]
    private float rotspd = 5f;
    [SerializeField]
    private float radius = .1f;
    private Vector2 _centre;
    private float _angle;
    [SerializeField]
    private float customAngle = 90;

    void Start()
    {
        _centre = transform.position;
    }
    void Update()
    {
        _angle += rotspd * Time.deltaTime;
        Vector2 offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * radius;
        transform.position = _centre + offset;
    }
}
