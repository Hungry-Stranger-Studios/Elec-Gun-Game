using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D topCollider;
    [SerializeField] private BoxCollider2D triggerCollider;

    public float climbPercent;
    float ladderTopPos;
    float ladderBottomPos;

    private Transform playerTr;
    private Rigidbody2D playerRb;
    private PlayerMovement pm;

    private bool playerLockedOn;
    private bool playerLockingOn;

    private bool gettingOffAtTop;
    private bool gettingOffAtBottom;

    private void Start()
    {
        playerTr = null;
        playerRb = null;
        pm = null;

        playerLockedOn = false;
        gettingOffAtTop = false;
        gettingOffAtBottom = false;

        ladderTopPos = transform.position.y + triggerCollider.size.y / 2f;
        ladderBottomPos = transform.position.y - triggerCollider.size.y / 2f;
    }
    private void Update()
    {
        if (pm != null && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            //Stop player from moving, turn off their gravity, and set veloity to 0
            pm.DeactivePlayer();
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0;
            playerRb.gravityScale = 0;
            topCollider.enabled = false;
            playerLockingOn = true;
        }

        if (playerLockingOn && !playerLockedOn)
        {
            if (Mathf.Abs(playerTr.position.x - transform.position.x) < 0.01f)
            {
                playerLockedOn = true;
                playerLockingOn = false;
            }
            else
            {
                playerTr.position = new Vector3(
                    Mathf.Lerp(playerTr.position.x, transform.position.x, 0.05f),
                    Mathf.Lerp(playerTr.position.y, playerTr.position.y + 0.1f, 0.05f), 
                    playerTr.position.z);
            }
        }

        if (playerLockedOn) 
        {
            climbPercent = ((playerTr.position.y - 1f) - ladderBottomPos) / (ladderTopPos - ladderBottomPos);
            if (Input.GetKey(KeyCode.UpArrow)) 
            { 
                if (climbPercent > 0.90f) { gettingOffAtTop = true; playerLockedOn = false; }
                else { playerTr.Translate(new Vector3(0, 0.002f, 0)); }
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (climbPercent < 0.05f) { gettingOffAtBottom = true; playerLockedOn = false; }
                else { playerTr.Translate(new Vector3(0, -0.002f, 0)); }
            }
        }

        if (gettingOffAtTop) { GetOffAtTop(); }

        if (gettingOffAtBottom) { GetOffAtBottom(); }
    }

    private void GetOffAtTop()
    {
        if (Mathf.Abs(playerTr.position.y - 1f - ladderTopPos) < 0.01f)
        {
            Debug.Log("player is off the ladder at the top");
        }
        else
        {
            playerTr.position = new Vector3(
                playerTr.position.x,
                Mathf.Lerp(playerTr.position.y - 1f, ladderTopPos, 0.05f),
                playerTr.position.z);
        }
    }

    private void GetOffAtBottom()
    {
        if (Mathf.Abs(playerTr.position.y - 1f - ladderBottomPos) < 0.01f)
        {
            Debug.Log("player is off the ladder at the bottom");
        }
        else
        {
            playerTr.position = new Vector3(
                playerTr.position.x,
                Mathf.Lerp(playerTr.position.y - 1f, ladderBottomPos, 0.05f),
                playerTr.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerTr = collision.transform;
        playerRb = collision.GetComponent<Rigidbody2D>();
        pm = playerTr.GetComponent<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerTr = null;
        playerRb = null;
        pm = null;
    }
}
