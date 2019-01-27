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
    [SerializeField] private GameObject _spriteObject;


    [SerializeField]
    private bool _isTestingMode = false;

    public event Damage.DamageEventMutator MutateDamage;
    public event Damage.DamageEventHandler OnTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<CharacterMotor>();
        _healthManager = GetComponent<HealthManager>();
        //_spriteObject = transform.GetChild(0).gameObject;
        _animator = _spriteObject.GetComponent<Animator>();

        _healthManager.OnDeath += HandleDeath;
        _healthManager.OnHealHealth += HandleHeal;

        _healthManager.SetHealth(JUMP_COUNT_MAX);

    }

    bool didMove = false;
    // Update is called once per frame
    void Update()
    {
        //handle inputs here
        float movementForce = Input.GetAxis("Horizontal");
        if (movementForce != 0) {
            didMove = _motor.DoMove(movementForce);
        }
        if(Input.GetButtonDown("Jump") || (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
            bool didJump = _motor.DoJump();
            if (!_isTestingMode && didJump) {
                ApplyDamage(this, 1f, transform.position);
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }
        FlipPlayer(movementForce);
        _animator.SetBool("Ground", _motor.IsGrounded);
        if(didMove) _animator.SetFloat("Speed", Mathf.Abs(movementForce));
        else _animator.SetFloat("Speed", Mathf.Abs(0));
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
        _motor.SetJump(false);
    }

    /// <summary>
    /// Do stuff when we revive
    /// </summary>
    private void HandleHeal(float h) {
        _motor.SetJump(true);
    }
    /// <summary>
    /// Check when we pick up health to re-enable the jump
    /// </summary>
    /// <param name="obj"></param>
    private void CheckPositiveHealth(float obj) {
        if (_healthManager.CurrentHealth > 0) {
            //_healthManager.Revive();
            _motor.SetJump(true);
        }
    }
    /// <summary>
    /// Stop all movement on the player and go back to the last checkpoint stepped on
    /// </summary>
    public void Reset() {
        _motor.StopAllMovement();
        _motor.SetMovement(false);
        _motor.SetJump(false);
        SceneFader.Instance.FadeIn(SceneFadeOutAction);
    }

    private void SceneFadeOutAction() {
        transform.position = CheckpointHandler.Instance.GetCurrentCheckpointPosition();
        SceneFader.Instance.FadeOut(DoResetSequence);
    }

    private void DoResetSequence() {
        _healthManager.Revive();
        _motor.SetJump(true);
        _motor.SetMovement(true);
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
