using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Collider2D zoneCollider;
    private Animator animator;

    private void Awake()
    {
        zoneCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().KillPlayer();
        }
    }
     public void Death()
    {
        // Trigger the "Death" animation parameter
        animator.SetTrigger("Death");

        // Disable the collider to prevent further interactions
        zoneCollider.enabled = false;

        // Destroy the object after the animation finishes
        StartCoroutine(DestroyAfterAnimation());

        EnemySpawner.DecrementEnemyCount();
        Debug.Log("Enemy Killed");
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for the "Death" animation to complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the GameObject
        Destroy(gameObject);
    }
}

