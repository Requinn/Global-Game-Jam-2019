using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base character class
/// </summary>
public class Character : MonoBehaviour
{
    private CharacterMotor _motor;

    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //handle inputs here
        _motor.Move(Input.GetAxis("Horizontal"));
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            _motor.Jump();
        }
    }
}
