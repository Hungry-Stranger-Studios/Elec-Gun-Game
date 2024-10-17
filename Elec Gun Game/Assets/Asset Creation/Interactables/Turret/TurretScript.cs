using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretScript : MonoBehaviour
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

    [SerializeField] private Boolean canShoot = true;
    public Boolean autoRotate = false;

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

        if (button != null)
        {
            button.ButtonPressed += OnActivation;
        }
    }
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
            int randomIndex = UnityEngine.Random.Range(0, projectilesColours.Length);
            GameObject projectilePrefab = projectilesColours[randomIndex];
            GameObject ball = Instantiate(projectilePrefab, new Vector3(shootPoint.position.x, shootPoint.position.y, shootPoint.position.z + 1), Quaternion.identity);
            Transform ballTransform = ball.transform;

            //Set direction and speed of projectile
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = aimDirection.normalized * projectileSpeed;

            ballTransform.localScale = Vector3.one;

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
        //Degrees to rad
        float radians = aimAngle * Mathf.Deg2Rad;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        { 
            if (collision.CompareTag("Gun_Bullet"))
            {
                canShoot = !canShoot;
                Debug.Log("Hit");
            }
        }
    }

    //Function to control turret by external input (Ex. Button)
    //Change 'canShoot' to whatever boolean you want to change
    private void OnActivation(object sender, ItemActivatedEventArgs e)
    {
        // Update turrets state based on the event activation 
        canShoot = e.isActive;
        Debug.Log("Turret is now " + (canShoot ? "active" : "inactive"));
    }

    void OnDestroy()
    {
        //Unsubscribe from the event to prevent memory leaks
        if (button != null)
        {
            button.ButtonPressed -= OnActivation;
        }
    }
}
