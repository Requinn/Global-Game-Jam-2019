using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement for the character
/// </summary>
public class CharacterMotor : MonoBehaviour {
    [SerializeField]
    private float _jumpForce, _movementSpeed;
    [SerializeField]
    private Transform _feetPosition;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    [SerializeField]
    private float _moveDamping = 0.001f, _movementForce = 0f;
    private Vector3 _velocity = Vector3.zero;
    private bool _canMove = true, _canJump = true;

    [SerializeField]
    private LayerMask _groundLayer;
    private float _centerHeightAdjust = 0f;
    [SerializeField]
    private bool _isGrounded = true;
    public bool IsGrounded { get { return _isGrounded; } }

    public AudioSource audioSource;
    public AudioClip jumpSound;

    // Start is called before the first frame update
    void Start() {
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

    public void SetJump(bool canJump) {
        _canJump = canJump;
    }

    public void StopAllMovement() {
        _rigidbody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Apply a force to the player in the direction
    /// </summary>
    /// <param name="force"></param>
    public void ApplyForce(Vector2 direction, float force) {
        Vector2 appliedForce = direction.normalized * force;
        if (force > 0) {
            _rigidbody.AddForce(appliedForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Do a Jump
    /// </summary>
    public bool DoJump() {
        if (!_isGrounded || !_canJump) { return false; }
        ApplyForce(transform.up, _jumpForce);
        if(audioSource)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        return true;
    }

    /// <summary>
    /// Move horizontally
    /// </summary>
    /// <param name="force"></param>
    public bool DoMove(float force) {
        bool success = false;
        if (!_canMove) { return false; }
        RaycastHit2D midAirWallCheck;
        //mid air wall check
        midAirWallCheck = Physics2D.BoxCast(transform.position, _collider.bounds.size, 0, (transform.right * force).normalized, _collider.bounds.extents.x + _rigidbody.velocity.x * Time.fixedDeltaTime, 1 << 8);
        Vector2 wallNormal = midAirWallCheck.normal;
        if (wallNormal == Vector2.right || wallNormal == Vector2.left) {
            //we hit a 90 degree wall surface
            success = false;
            force = 0;
        }
        if (!_isGrounded && midAirWallCheck) {
            //return false;
        }
        Vector3 _targetVelocity = new Vector2(force * _movementSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, _targetVelocity, ref _velocity, _moveDamping);
        success = true;
        return success;
    }

    RaycastHit2D _hit;
    /// <summary>
    /// Check for ground detection
    /// </summary>
    private void CheckGrounded() {
        bool wasGrounded = _isGrounded;
        _isGrounded = false;
        Vector3 originOffset = transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0);
        _hit = Physics2D.Raycast(originOffset, Vector2.down, _centerHeightAdjust, _groundLayer);
        Debug.DrawLine(originOffset, transform.position + new Vector3(Vector2.down.x * _centerHeightAdjust, Vector2.down.y * _centerHeightAdjust), Color.red);
        if(_hit) {
            _isGrounded = true;
        }
    }

}
