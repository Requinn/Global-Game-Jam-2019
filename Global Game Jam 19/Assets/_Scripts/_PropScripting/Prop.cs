using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Props are ALL interactable scenery pieces
interface Prop
{
    //Every prop has a trigger
    void OnTriggerEnter2D();

}
