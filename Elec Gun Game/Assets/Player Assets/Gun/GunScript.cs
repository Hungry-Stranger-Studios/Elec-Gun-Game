using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
public class GunScript : MonoBehaviour
{
    [SerializeField] private Transform spotlight; //Light attached to gun (if wanted)
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPoint; // Projectile origin
    [SerializeField] private Transform gunParent; // Rotation point of gun
    [SerializeField] private SpriteRenderer gunSprite;
    [SerializeField] private Camera playerCam;
    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private float minMouseDistance = 10.10f;
    [SerializeField] private bool canShoot = true;

    private float xScale;
    private float yScale;

    private List<GameObject> projectiles = new List<GameObject>();

   
    private Vector3 mousePos;

    void Start()
    {
        xScale = gunParent.transform.localScale.x;
        yScale = gunParent.transform.localScale.y;
    }
    void Update()
    {
        // Update mouse position
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        AimGun();

        // Check for shooting input
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void AimGun()
    {
        //Calculate the direction to the mouse position
        Vector3 directionToMouse = mousePos - gunParent.position;
        float distanceToMouse = directionToMouse.magnitude;

        //Only rotate if mouse is outside minimum radius
        if (distanceToMouse > minMouseDistance)
        {
            //Calculate angle towards mouse position
            float rotZ = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
            gunParent.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        //Flip the gun sprite based on rotation
        Vector3 localScale = gunParent.localScale;
        if (directionToMouse.x < 0)
        {
            gunParent.localScale = new Vector3(xScale, -yScale, 1f);
        }
        else
        {
            gunParent.localScale = new Vector3(xScale, yScale, 1f);
        }

        if (spotlight != null) {
            spotlight.rotation = Quaternion.Euler(0f, 0f, gunParent.rotation.eulerAngles.z - 90f);
        }
    }


    private void Shoot()
    {
        if (canShoot)
        {
            //Calculate aim direction
            Vector2 aimDirection = shootPoint.right;

            //Create projectile at shoot point
            GameObject ball = Instantiate(projectile, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = aimDirection * projectileSpeed;

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
}
