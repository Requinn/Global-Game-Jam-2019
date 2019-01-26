using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement for the character
/// </summary>
public class CharacterMotor : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce, _movementSpeed;
    [SerializeField]
    private Transform _feetPosition;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    [SerializeField]
    private float _moveDamping = 0.001f;
    private Vector3 _velocity = Vector3.zero;
    private bool _canMove = true;

    [SerializeField]
    private LayerMask _groundLayer;
    private float _centerHeightAdjust = 0f;
    [SerializeField]
    private bool _isGrounded = true;
    public bool IsGrounded { get { return _isGrounded; } }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _centerHeightAdjust = _collider.bounds.extents.y + 0.075f;
    }

    void FixedUpdate() {
        CheckGrounded();
    }

    /// <summary>
    /// Sets the movement state
    /// </summary>
    public void SetMovement(bool canMove) {
        _canMove = canMove;
    }

    /// <summary>
    /// Apply a force to the player in the direction
    /// </summary>
    /// <param name="force"></param>
    public void ApplyForce(Vector2 direction, float force) {
        Vector2 appliedForce = direction * force;
        if (force > 0) {
            _rigidbody.AddForce(appliedForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Do a Jump
    /// </summary>
    public void Jump() {
        if (!_isGrounded || !_canMove) { return; }
        ApplyForce(transform.up, _jumpForce);
    }

    /// <summary>
    /// Move horizontally
    /// </summary>
    /// <param name="force"></param>
    public void Move(float force) {
        Vector3 _targetVelocity = new Vector2(force * _movementSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, _targetVelocity, ref _velocity, _moveDamping);
    }

    RaycastHit2D _hit;
    /// <summary>
    /// Check for ground detection
    /// </summary>
    private void CheckGrounded() {
        bool wasGrounded = _isGrounded;
        _isGrounded = false;
        _hit = Physics2D.Raycast(transform.position, Vector2.down, _centerHeightAdjust, _groundLayer);
        Debug.DrawLine(transform.position, transform.position + new Vector3(Vector2.down.x * _centerHeightAdjust, Vector2.down.y * _centerHeightAdjust), Color.red);
        if(_hit) {
            _isGrounded = true;
        }
    }

}
