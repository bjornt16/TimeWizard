using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMB<InputManager> {


    public override void CopyValues(SingletonMB<InputManager> copy)
    {

    }


    private void Update()
    {

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > .1f)
        {
            Debug.Log(Input.GetAxis("Horizontal"));
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            Debug.Log(Input.GetAxis("Vertical"));
        }

    }
    
}
