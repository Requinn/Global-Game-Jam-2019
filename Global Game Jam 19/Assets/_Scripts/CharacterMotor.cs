using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
/// <summary>
/// Movement for the character
/// </summary>
public class CharacterMotor : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce, _movementSpeed;
    private CharacterController2D _controller;
    private Collider2D _collider;
    private float _gravityForce = -9.81f;
    public Vector3 _movement;

    private float _centerHeightAdjust = 0f;
    private bool _isGrounded = false;
    public bool IsGrounded { get { return _isGrounded; } }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _collider = GetComponent<Collider2D>();
        _centerHeightAdjust = _collider.bounds.extents.y + 0.075f;
    }

    void Update() {
        CheckGrounded();
    }

    void FixedUpate() {
        //gravity in air
        if (!_isGrounded) {
            _movement.y += _gravityForce * Time.deltaTime;
        }
        //small downforce while on the ground
        else {
            _movement.y = -.05f / Time.deltaTime;
        }
        //apply the movement
        _controller.move(_movement * Time.deltaTime);
    }

    /// <summary>
    /// Make the player jump up with force
    /// </summary>
    /// <param name="force"></param>
    public void Jump(float force = 0) {
        _movement.y = 0;
        if (force == 0) {
            _movement.y += _jumpForce;
        }
        else {
            _movement.y += force;
        }
    }

    /// <summary>
    /// Move horizontally
    /// </summary>
    /// <param name="force"></param>
    public void Move(float force) {
        _movement.x += force * _movementSpeed;
    }

    RaycastHit2D _hit;
    private void CheckGrounded() {
        _hit = Physics2D.Raycast(transform.position, Vector2.down, _centerHeightAdjust, LayerMask.NameToLayer("Ground"));
    }
}
