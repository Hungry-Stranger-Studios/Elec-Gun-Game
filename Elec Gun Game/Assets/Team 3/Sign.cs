using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text signText;
    public string[] text;
    private int index;

    public float textSpeed;
    public bool isClose;
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                noText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());

            }
        }
    }

    public void noText()
    {
        signText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in text[index].ToCharArray())
        {
            signText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClose = false;
            noText();
        }
    }
}
