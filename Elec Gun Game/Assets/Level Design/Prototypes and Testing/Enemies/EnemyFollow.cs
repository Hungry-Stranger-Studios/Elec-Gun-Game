using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    public float speed = 2f;
    [SerializeField] private float jumpMult = 25f;
    private bool isGrounded;

    [SerializeField] private Rigidbody2D enemyRigidbody;
    private float jumpCooldown = 0.2f;
    private float nextJumpTime;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, 6 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && isGrounded && Time.time >= nextJumpTime)
        {
            Debug.Log("Wall detected! Jumping...");
            Jump();
        }
    }

    private void Jump()
    {
        enemyRigidbody.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        nextJumpTime = Time.time + jumpCooldown;
    }
}
