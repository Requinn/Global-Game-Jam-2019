using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private float launch;
    private Coroutine springJuice;
    private Animator springAnimator;
    private int springHash;
    
    private void Start()
    {
        springAnimator = GetComponentInChildren<Animator>();
        springHash = Animator.StringToHash("Active");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.attachedRigidbody)
            Launch(other.attachedRigidbody);
    }

    private void Launch(Rigidbody2D arb2d)
    {
        arb2d.velocity = new Vector2(arb2d.velocity.x, 0);
        Debug.Log(gameObject.transform.up * launch);
        arb2d.AddForce(gameObject.transform.up * launch , ForceMode2D.Force);
        //Debug.Log("Launched.");
        springJuice = StartCoroutine(ISpringJuice());
        
    }

    IEnumerator ISpringJuice()
    {
        //Debug.Log("Started.");
        springAnimator.SetTrigger(springHash);
        yield return null;
        springJuice = null;
    }

    void FixedUpdate()
    {
        if(springJuice == null)
            springAnimator.ResetTrigger(springHash);
    }
    

}
