using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunScript : MonoBehaviour
{
    [SerializeField] private GameObject projectile; 
    [SerializeField] private Transform shootPoint; //Projectile origin
    [SerializeField] private Transform gunParent; //Rotation point of gun
    [SerializeField] private SpriteRenderer gunSprite;

    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private bool canShoot = true;

    private float xScale;
    private float yScale;

    private List<GameObject> projectiles = new List<GameObject>();

    [Range(-40f, 220f)]
    [SerializeField] private float aimAngle = 0f;

    void Start()
    {
        xScale = gunParent.transform.localScale.x;
        yScale = gunParent.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        gunParent.rotation = Quaternion.Euler(0f, 0f, aimAngle);
        
        if (aimAngle > 90f && aimAngle < 270f)
        {
            
            //Flip the sprite
            gunParent.localScale = new Vector3((xScale), -(yScale), 1f);
        }
        else
        {
            //Reset the flip
            gunParent.localScale = new Vector3((xScale), (yScale), 1f);
        }
        
    }

    void Shoot()
    {
        if (canShoot)
        {
            Vector2 aimDirection = CalculateAimDirection();

            //Create projectile at shoot point
            GameObject ball = Instantiate(projectile, shootPoint.position, Quaternion.identity);

            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = aimDirection.normalized * projectileSpeed;

            //Add projectile to list
            projectiles.Add(ball);

            //Maintain a maximum of one projectile
            if (projectiles.Count > 1)
            {
                Destroy(projectiles[0]);
                projectiles.RemoveAt(0);
            }
        }
    }

    Vector2 CalculateAimDirection()
    {
        //Convert aim angle (degrees) to radians
        float radians = aimAngle * Mathf.Deg2Rad;

        //Calculate direction vector based on angle
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);

        return new Vector2(x, y);
    }
}
