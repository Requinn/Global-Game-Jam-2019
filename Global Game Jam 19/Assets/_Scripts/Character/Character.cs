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
    private bool _shouldReset = false;
    public bool ShouldReset => _shouldReset;

    [SerializeField]
    private bool _isTestingMode = false;

    private bool _canDoInputs = true;
    public bool CanDoInputs { get { return _canDoInputs; } set { _canDoInputs = value; } }

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
        _healthManager.OnUpdateHealth += CheckPositiveHealth;

        //_healthManager.SetHealth(JUMP_COUNT_MAX);

    }

    bool didMove = false, doJump = false;
    // Update is called once per frame
    void Update()
    {
        doJump = false;
        didMove = false;
        //handle inputs here
        float movementForce = _canDoInputs ? Input.GetAxis("Horizontal") : 0;

        if(_canDoInputs && (Input.GetButtonDown("Jump") || (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))) {
            doJump = true;
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }

        DoMovements(movementForce, doJump);
    }

    /// <summary>
    /// Perform the movements on the player
    /// </summary>
    public void DoMovements(float movement, bool doJump) {
        //horizontal movement
        if (movement != 0) {
            didMove = _motor.DoMove(movement);
        }

        //did we jump
        if (doJump) {
            bool didJump = _motor.DoJump();
            if (!_isTestingMode && didJump) {
                ApplyDamage(this, 1f, transform.position);
            }
        }
        //animator stuff
        FlipPlayer(movement);
        _animator.SetBool("Ground", _motor.IsGrounded);
        if (didMove) _animator.SetFloat("Speed", Mathf.Abs(movement));
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

    public void SetResetState(bool reset) {
        _shouldReset = reset;
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
        bool isHealthy = _healthManager.CurrentHealth > 0;
        SetResetState(!isHealthy);
    }

    /// <summary>
    /// Stop all movement on the player and go back to the last checkpoint stepped on
    /// </summary>
    public void Reset()
    {
        if (CheckpointHandler.Instance.CurrentIndex < 0)
        {
            return;
        }
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
