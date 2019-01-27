using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// shows UI text to tell the player to reset
/// </summary>
public class ShowResetUIText : MonoBehaviour
{
    [SerializeField]
    private Character _character;
    [SerializeField]
    private GameObject _textUI;

    void Start() {
        _textUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (_character.ShouldReset) {
            _textUI.SetActive(true);
        }
        else {
            _textUI.SetActive(false);
        }
    }
}
