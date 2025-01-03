using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour, IControllable
{
    // References to objects related to the turret and shooting mechanics
    [SerializeField] private Transform shootPoint; // Point where projectile comes from
    [SerializeField] private SpriteRenderer turretSprite; // Visual representation of the turret
    [SerializeField] private Transform turretParent; // Parent transform for turret rotation or positioning

    // Settings for projectile behavior
    [SerializeField] private float projectileSpeed = 3f; // Speed at which the projectile travels
    [SerializeField] private float timeBetweenShots = 2f; // Cooldown time between shots
    [SerializeField] private int maxProjectiles = 10; // Maximum number of projectiles allowed

    // Aiming and rotation settings
    [Range(-40f, 220f)]
    [SerializeField] private float aimAngle = 0f; // Current aim angle of the turret
    [SerializeField] private bool canShoot = true; // Flag to control if the turret can shoot
    public bool autoRotate = false; // Whether the turret automatically rotates

    // Auto-rotation specific settings
    [HideInInspector] public float autoRotateSpeed = 10f; // Speed of auto-rotation
    [HideInInspector] public float autoRotateMinAngle = -40f; // Minimum angle for auto-rotation
    [HideInInspector] public float autoRotateMaxAngle = 220f; // Maximum angle for auto-rotation

    // Shooting timer and projectile list
    private float shootTimer = 0f; // Tracks cooldown time between shots
    private List<GameObject> projectiles = new List<GameObject>(); // List of active projectiles
    [SerializeField] private GameObject[] projectilesColours = new GameObject[3]; // Array of different projectile colors

    // UI reference
    public ButtonScript button; // Reference to a button script

    private void Start()
    {
        if (button != null)
        {
            button.ButtonPressed += OnActivation;
        }
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        // Only shoot if we can shoot and the time has passed
        if (canShoot && shootTimer >= timeBetweenShots)
        {
            Shoot();
            shootTimer = 0f; //Reset timer after shooting
        }

        if (autoRotate) //Check for auto-rotation
        {
            AutoRotateTurret();
        }
        else
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

            // Create and fire a projectile
            int randomIndex = UnityEngine.Random.Range(0, projectilesColours.Length);
            GameObject projectilePrefab = projectilesColours[randomIndex];
            GameObject ball = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = aimDirection.normalized * projectileSpeed;

            projectiles.Add(ball);

            // Destroy old projectiles if exceeded maxProjectiles
            if (projectiles.Count > maxProjectiles)
            {
                Destroy(projectiles[0]);
                projectiles.RemoveAt(0);
            }
        }
    }

    Vector2 CalculateAimDirection()
    {
        // Calculate the aim direction based on the aim angle
        float radians = aimAngle * Mathf.Deg2Rad;
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

    // React to external inputs (like button presses)
    public void OnActivation(object sender, ItemActivatedEventArgs e)
    {
        // Update turret's state based on the button's activation state
        canShoot = e.isActive;
    }

    // Clean up event subscription to avoid memory leaks
    void OnDestroy()
    {
        if (button != null)
        {
            button.ButtonPressed -= OnActivation;
        }
    }

    // Trigger detection (Ex. for collisions with bullets)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gun_Bullet"))
        {
            canShoot = !canShoot; // Toggle shooting ability on collision
        }
    }
}