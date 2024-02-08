using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E; // Key to pick up or drop guns
    public float pickupRange = 5f; // Range within which the player can pick up a gun
    public Transform gunHolder; // The empty object on the player to hold the gun
    public Camera playerCamera; // Reference to the player's camera

    private Gun currentGun = null; // Currently held gun, if any

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (currentGun != null)
            {
                DropGun();
            }
            else
            {
                TryPickup();
            }
        }
    }

    void TryPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            Gun gun = hit.collider.gameObject.GetComponent<Gun>();
            if (gun != null && gun.gameObject.GetComponent<Rigidbody>() != null)
            {
                PickUpGun(gun);
            }
        }
    }

    void PickUpGun(Gun gun)
    {
        currentGun = gun;
        currentGun.PickUp();

        Rigidbody gunRb = currentGun.gameObject.GetComponent<Rigidbody>();
        gunRb.isKinematic = true; // Disable physics

        currentGun.transform.SetParent(gunHolder);
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;
    }

    void DropGun()
    {
        Rigidbody gunRb = currentGun.gameObject.GetComponent<Rigidbody>();
        gunRb.isKinematic = false; // Enable physics

        currentGun.transform.SetParent(null); // Detach the gun from the holder

        // Apply a slight force or offset if you want the gun to drop more naturally
        gunRb.AddForce(gunHolder.forward * 1f, ForceMode.VelocityChange);

        currentGun.Drop();
        currentGun.gameObject.SetActive(true); // Make the gun visible in the world
        currentGun = null; // Indicate that the player is no longer holding a gun
    }
}
