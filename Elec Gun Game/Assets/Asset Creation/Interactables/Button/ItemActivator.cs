using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivator : MonoBehaviour
{
    public IControllable controllableItem;

    public void ActivateItem(bool isActive)
    {
        // Raise the event
        controllableItem.OnActivation(this, new ItemActivatedEventArgs(isActive));
    }
}