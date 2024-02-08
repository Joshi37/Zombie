using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public UnityEvent OnGunShoot;
    public float FireCooldown;
    public bool Automatic;
    public ParticleSystem muzzleFlash; // Reference to the muzzle flash particle system
    public GameObject bulletTrailPrefab; // Reference to the bullet trail prefab
    public Transform bulletTrailStartPoint;


    private float CurrentCooldown;
    public bool isHeld = false; // Flag to check if the gun is held by the player

    void Start()
    {
        CurrentCooldown = FireCooldown;
    }

    void Update()
    {
        if (!isHeld) return; // Do not proceed if the gun is not held

        if (Automatic && Input.GetMouseButton(0) && CurrentCooldown <= 0f)
        {
            Shoot();
        }
        else if (!Automatic && Input.GetMouseButtonDown(0) && CurrentCooldown <= 0f)
        {
            Shoot();
        }

        CurrentCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        OnGunShoot?.Invoke();
        muzzleFlash.Play(); // Play the muzzle flash effect
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f)) // Assuming 100 units as the max range
        {
            CreateBulletTrail(hit.point);
        }
        else
        {
            CreateBulletTrail(transform.position + transform.forward * 100f); // No hit, extend to max range
        }
        CurrentCooldown = FireCooldown;
    }

    private void CreateBulletTrail(Vector3 hitPoint)
    {
        var trailInstance = Instantiate(bulletTrailPrefab, bulletTrailStartPoint.position, Quaternion.identity);
        LineRenderer lr = trailInstance.GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.SetPosition(0, bulletTrailStartPoint.position); // Start at the bulletTrailStartPoint's position
            lr.SetPosition(1, hitPoint); // End at the hit point or max range
        }
        Destroy(trailInstance, 0.5f); // Adjust the time according to how long you want the trail to be visible
    }



    public void PickUp()
    {
        isHeld = true;
    }

    public void Drop()
    {
        isHeld = false;
    }
}
