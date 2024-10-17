using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivatedEventArgs : EventArgs
{
    public bool isActive { get; }

    public ItemActivatedEventArgs(bool isActive)
    {
        this.isActive = isActive;
    }
}
