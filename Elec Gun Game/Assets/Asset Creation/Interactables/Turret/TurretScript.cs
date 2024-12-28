using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour, IControllable
{
    [SerializeField] private Transform shootPoint; //Point where projectile comes from
    [SerializeField] private SpriteRenderer turretSprite;
    [SerializeField] private Transform turretParent;

    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private float timeBetweenShots = 2f;
    [SerializeField] private int maxProjectiles = 10;

    [Range(-40f, 220f)]
    [SerializeField] private float aimAngle = 0f;
    private float shootTimer = 0f;

    [SerializeField] private bool canShoot = true;
    public bool autoRotate = false;

    [HideInInspector]
    public float autoRotateSpeed = 10f;

    [HideInInspector]  //Hide unless autoRotate is true
    public float autoRotateMinAngle = -40f;

    [HideInInspector]  //Hide unless autoRotate is true
    public float autoRotateMaxAngle = 220f;

    private List<GameObject> projectiles = new List<GameObject>(); //List of projectiles

    [SerializeField] private GameObject[] projectilesColours = new GameObject[3];

    public ButtonScript button;

    private void Start()
    {
        button = FindObjectOfType<ButtonScript>();

        // Ensure the button is found before subscribing to event
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
    private void OnActivation(object sender, ItemActivatedEventArgs e)
    {
        // Update turret's state based on the button's activation state
        canShoot = e.isActive;
        Debug.Log("Turret is now " + (canShoot ? "active" : "inactive"));
    }

    // Clean up event subscription to avoid memory leaks
    void OnDestroy()
    {
        if (button != null)
        {
            button.ButtonPressed -= OnActivation;
        }
    }

    // Toggle the shooting state (via external control)
    public void ToggleState(bool isActive)
    {
        this.canShoot = isActive;
        Debug.Log("Turret state toggled: " + (isActive ? "active" : "inactive"));
    }

    // Trigger detection (e.g., for collisions with bullets)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gun_Bullet"))
        {
            canShoot = !canShoot; // Toggle shooting ability on collision
            Debug.Log("Hit by bullet. Turret shooting: " + canShoot);
        }
    }
}