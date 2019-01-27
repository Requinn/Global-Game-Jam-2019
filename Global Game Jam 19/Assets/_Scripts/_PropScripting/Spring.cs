using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float launch;
    [SerializeField] private float controlSupressTime = 0.5f;

    [SerializeField] private List<AudioClip> springSounds;
    private Coroutine springJuice, disablePlayer;
    private Animator springAnimator;
    private int springHash;
    private AudioSource springSource;
    
    private void Start()
    {
        springAnimator = GetComponentInChildren<Animator>();
        springHash = Animator.StringToHash("Active");
        springSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.attachedRigidbody)
            Launch(other.attachedRigidbody);
    }

    private void Launch(Rigidbody2D arb2d)
    {
        arb2d.velocity = new Vector2(0, 0);
        if (arb2d.gameObject.CompareTag("Player"))
        {
            CharacterMotor playerMotor = arb2d.gameObject.GetComponent<CharacterMotor>();
            disablePlayer = StartCoroutine(DisablePlayer(playerMotor));
            playerMotor.ApplyForce(transform.up, launch*2);
            
        }
        else
        {
            arb2d.AddForce(transform.up * launch, ForceMode2D.Impulse);
        }

        arb2d.AddForce(gameObject.transform.up * launch , ForceMode2D.Force);
        springJuice = StartCoroutine(SpringJuice());
        
    }

    IEnumerator SpringJuice()
    {
        //Debug.Log("Started.");
        springAnimator.SetTrigger(springHash);
        springSource.clip = Randomizer.GetRandom(springSounds);
        springSource.Play();
        yield return null;
        springJuice = null;
    }

    IEnumerator DisablePlayer(CharacterMotor pm)
    {
        pm.SetMovement(false);
        pm.StopAllMovement();
        yield return new WaitForSeconds(controlSupressTime);
        pm.SetMovement(true);
    }
    void FixedUpdate()
    {
        if(springJuice == null)
            springAnimator.ResetTrigger(springHash);
    }
    

}
