using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;                                    //Point where projectile comes from
    [SerializeField] private SpriteRenderer turretSprite;
    [SerializeField] private Transform turretParent;

    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private float timeBetweenShots = 2f;
    [SerializeField] private int maxProjectiles = 10;

    [Range(-40f, 220f)]
    [SerializeField] private float aimAngle = 0f;
    private float shootTimer = 0f;

    [SerializeField] private Boolean canShoot = true;
    public Boolean autoRotate = false;

    [HideInInspector]
    public float autoRotateSpeed = 10f; 

    [HideInInspector]  //Hide unless autoRotate is true
    public float autoRotateMinAngle = -40f;

    [HideInInspector]  //Hide unless autoRotate is true
    public float autoRotateMaxAngle = 220f;

    private List<GameObject> projectiles = new List<GameObject>(); //List of projectiles

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= timeBetweenShots)
        {
            Shoot();
            shootTimer = 0f; //Reset timer
        }

        if (autoRotate) //Check for rotation setting
        {
            if (autoRotateMinAngle < -40f)
            {
                autoRotateMinAngle = -40f;
            }

            if (autoRotateMaxAngle > 220f)
            {
                autoRotateMaxAngle = 220f;
            }

            AutoRotateTurret();
        } else
        {
            RotateTurret();
        }

    }

    void Shoot()
    {
        if (canShoot)
        {
            Vector2 aimDirection = CalculateAimDirection();
            transform.rotation = Quaternion.Euler(Vector3.forward * aimDirection);

            //Create projectile
            GameObject ball = Instantiate(projectilePrefab, new Vector3(shootPoint.position.x, shootPoint.position.y, shootPoint.position.z + 1), Quaternion.identity);

            //Set direction and speed of projectile
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = aimDirection.normalized * projectileSpeed;

            projectiles.Add(ball);

            if (projectiles.Count > maxProjectiles)
            {
                Destroy(projectiles[0]);
                projectiles.RemoveAt(0); 
            }
        }
    }

    Vector2 CalculateAimDirection()
    {
        float radians = aimAngle * Mathf.Deg2Rad; //Degrees to rad

        //Calculate circumference of aim
        float x = Mathf.Cos(radians) * 5;
        float y = Mathf.Sin(radians) * 5;

        return new Vector2(x, y);
    }

    void RotateTurret()
    {
        turretParent.rotation = Quaternion.Euler(0f, 0f, aimAngle);
    }

    void AutoRotateTurret()
    {
        float pingPongValue = Mathf.PingPong(Time.time * autoRotateSpeed, autoRotateMaxAngle - autoRotateMinAngle);
        aimAngle = autoRotateMinAngle + pingPongValue;

        turretParent.rotation = Quaternion.Euler(0f, 0f, aimAngle);
    }
}
