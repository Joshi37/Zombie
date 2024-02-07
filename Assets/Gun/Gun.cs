using UnityEngine.Events;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public UnityEvent OnGunShoot;
    public float FireCooldown;
    public bool Automatic;

    private float CurrentCooldown;
    public bool isHeld = false; // Flag to check if the gun is held by the player

    void Start()
    {
        CurrentCooldown = FireCooldown;
    }

    void Update()
    {
        if (!isHeld) return; // Do not proceed if the gun is not held

        if (Automatic)
        {
            if (Input.GetMouseButton(0) && CurrentCooldown <= 0f)
            {
                OnGunShoot?.Invoke();
                CurrentCooldown = FireCooldown;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && CurrentCooldown <= 0f)
            {
                OnGunShoot?.Invoke();
                CurrentCooldown = FireCooldown;
            }
        }

        CurrentCooldown -= Time.deltaTime;
    }

    // Call this method to indicate the player has picked up the gun
    public void PickUp()
    {
        isHeld = true;
    }

    // Call this method to indicate the player has dropped the gun
    public void Drop()
    {
        isHeld = false;
    }
}
