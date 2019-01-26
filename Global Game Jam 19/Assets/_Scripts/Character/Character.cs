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

    public event Damage.DamageEventMutator MutateDamage;
    public event Damage.DamageEventHandler OnTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<CharacterMotor>();
        _healthManager = GetComponent<HealthManager>();
        _animator = GetComponent<Animator>();

        _healthManager.OnDeath += HandleDeath;
        _healthManager.SetHealth(JUMP_COUNT_MAX);

    }

    // Update is called once per frame
    void Update()
    {
        //handle inputs here
        _motor.Move(Input.GetAxis("Horizontal"));
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
            _motor.Jump();
            ApplyDamage(this, 1f, transform.position);
        }
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

}
