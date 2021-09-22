using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour
{
    public event EventHandler OnSpacePressed;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Space Pressed!
            OnSpacePressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
