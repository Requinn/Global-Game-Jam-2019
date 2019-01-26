using MichaelWolfGames.DamageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Base character class
/// </summary>
public class Character : MonoBehaviour, IDamageable
{
    private const int JUMP_COUNT_MAX = 3;
    private CharacterMotor _motor;
    public CharacterMotor Motor { get { return _motor; } }
    private HealthManager _healthManager;
    private Animator _animator;
    private GameObject _spriteObject;

    public event Damage.DamageEventMutator MutateDamage;
    public event Damage.DamageEventHandler OnTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<CharacterMotor>();
        _healthManager = GetComponent<HealthManager>();
        _spriteObject = transform.GetChild(0).gameObject;
        _animator = _spriteObject.GetComponent<Animator>();

        _healthManager.OnDeath += HandleDeath;
        _healthManager.SetHealth(JUMP_COUNT_MAX);

    }

    // Update is called once per frame
    void Update()
    {
        //handle inputs here
        float movementForce = Input.GetAxis("Horizontal");
        if (movementForce != 0) {
            _motor.Move(movementForce);
        }
        if(Input.GetButtonDown("Jump") || (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
            _motor.Jump();
            ApplyDamage(this, 1f, transform.position);
        }
        FlipPlayer(movementForce);
        _animator.SetBool("Ground", _motor.IsGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(movementForce));
    }

    /// <summary>
    /// Shouldn't be used? If used reroutes call to ApplyDamage(obj, float, vec3)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void ApplyDamage(object sender, ref Damage.DamageEventArgs args) {
        ApplyDamage(sender, args.DamageValue, transform.position);
    }

    /// <summary>
    /// Takes damage to the player
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="damage"></param>
    /// <param name="hitPoint"></param>
    public void ApplyDamage(object sender, float damage, Vector3 hitPoint) {
        _healthManager.ApplyDamage(damage);
    }

    public Damage.Faction GetFaction() {
        return _healthManager.Faction;
    }

    /// <summary>
    /// Do stuff on death
    /// </summary>
    private void HandleDeath() {
        _motor.SetMovement(false);
        Debug.Log("We Died");
    }

    /// <summary>
    /// Stop all movement on the player and go back to the last checkpoint stepped on
    /// </summary>
    public void Reset() {
        _healthManager.AddHealth(JUMP_COUNT_MAX);
        _motor.StopAllMovement();
        transform.position = CheckpointHandler.Instance.GetCurrentCheckpointPosition();
    }

    private Quaternion _rightRotation = Quaternion.Euler(Vector3.zero), _leftRotation = Quaternion.Euler(new Vector3(0, 180f, 0));
    /// <summary>
    /// flips player sprite based on direction
    /// </summary>
    /// <param name="direction"></param>
    private void FlipPlayer(float direction) {
        if(direction > 0) {
            _spriteObject.transform.rotation = _rightRotation;
        }
        else if(direction < 0) {
            _spriteObject.transform.rotation = _leftRotation;
        }
    }
}
