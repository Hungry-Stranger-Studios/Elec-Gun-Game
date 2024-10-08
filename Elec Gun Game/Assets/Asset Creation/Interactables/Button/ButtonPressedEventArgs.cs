using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedEventArgs : EventArgs
{
    public bool IsPressed { get; }

    public ButtonPressedEventArgs(bool isPressed)
    {
        IsPressed = isPressed;
    }
}
