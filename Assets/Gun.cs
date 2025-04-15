using UnityEngine;

// This script controls the gun behavior, including shooting bullets.
public class Gun : MonoBehaviour
{
    // Public variables exposed in the Unity Inspector
    public OVRInput.RawButton shootingButton; // The VR controller button used to shoot
    public GameObject bulletPrefab;           // The prefab for the bullet to be instantiated
    public Transform bulletSpawnPoint;        // The point where the bullet will spawn
    public float bulletSpeed = 20f;           // The speed at which the bullet travels
    public AudioSource source;
    public AudioClip clip;
    // Start is called once before the first frame update
    void Start()
    {
        // Initialization code can go here (currently empty)
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the designated shooting button is pressed down in this frame
        if (OVRInput.GetDown(shootingButton))
        {
            // If the button is pressed, call the Shoot method
            Shoot();
        }
    }

    // Method to handle the shooting logic
    public void Shoot()
    {
        source.PlayOneShot(clip); // Play the shooting sound effect
        Debug.Log("Shoot method called."); // Log message for debugging

        // Instantiate the bullet prefab at the spawn point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // --- Modification Start ---
        // Rotate the instantiated bullet 90 degrees around its local Y-axis (upward direction)
        // This effectively rotates it "to the right" relative to its initial forward direction.
        bullet.transform.Rotate(0, 90, 0);
        // --- Modification End ---

        // Get the Rigidbody component attached to the bullet prefab
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bool hasHit = false; // Initialize a flag to check if the bullet hit something
        // Check if the bullet has a Rigidbody component
        if (bulletRb != null)
        {
            hasHit = true;
            // Perform a raycast from the bullet's position in the forward direction
            RaycastHit hit;
            // The raycast checks for collisions with objects in the scene
            // The ray starts at the bullet's position and goes in the direction of the bullet's forward vector
            if (Physics.Raycast(bullet.transform.position, bullet.transform.forward, out hit))
            {
                // If the raycast hits something, log the hit information
                Debug.Log("Hit: " + hit.collider.name);
                // Optionally, you can apply damage or effects to the object hit
                // For example: hit.collider.GetComponent<Enemy>().TakeDamage(damageAmount);
            }
            else
            {
                // If the raycast does not hit anything, log that no hit was detected
                Debug.Log("No hit detected.");
            }

            // Check if the Rigidbody component exists
            if (bulletRb != null)
            {
                // If it exists, set its velocity to move forward from the spawn point
                // Note: We use bulletSpawnPoint.forward here to ensure the initial velocity direction
                // matches the gun's forward direction, even though the bullet itself is rotated.
                // If you wanted the velocity aligned with the *rotated* bullet, you'd use bullet.transform.forward.
                bulletRb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
            }
            else
            {
                // Log an error if the bullet prefab is missing the Rigidbody component
                Debug.LogError("Bullet prefab does not have a Rigidbody component.");
            }

            // Destroy the bullet GameObject after 2 seconds to prevent clutter
            Destroy(bullet, 2f);
        }
    }
}
