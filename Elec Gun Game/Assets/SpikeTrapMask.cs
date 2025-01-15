using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpikeTrapMask : MonoBehaviour
{
    private SpikeTrapController parent;

    public void Awake()
    {
        parent = transform.GetComponentInParent<SpikeTrapController>();
    }

    public void Update()
    {
        float maxLength = parent.getMaxLength();
        transform.localPosition = new Vector3(0, maxLength / 2, 0);
        transform.localScale = new Vector2(1, maxLength);
    }
}
