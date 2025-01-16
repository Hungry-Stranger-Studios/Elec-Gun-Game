using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SpriteController : MonoBehaviour
{
    private SpikeTrapController trapController;
    private GameObject spriteMiddle;
    // Start is called before the first frame update
    void Awake()
    {
        trapController = transform.parent.GetComponent<SpikeTrapController>();
        spriteMiddle = transform.GetChild(1).gameObject;
        int maxLen = (int) trapController.getMaxLength();
        //create maxLen - 2 (because we already have the tip and one copy) copies of middle
        for(int i = 1; i < maxLen; i++)
        {
            GameObject newMidSprite = Instantiate(spriteMiddle, transform); //create a copy of the sprite
            newMidSprite.transform.localPosition -= new Vector3(0, i, 0); //shove the new sprite down in the scene
        }
    }
}
