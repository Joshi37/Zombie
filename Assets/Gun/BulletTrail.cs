using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    public float duration = 0.5f; // Duration in seconds the trail is visible

    private void OnEnable()
    {
        Invoke("DeactivateTrail", duration);
    }

    private void DeactivateTrail()
    {
        gameObject.SetActive(false);
        // If using a pool, return it to the pool instead of deactivating
    }

    private void OnDisable()
    {
        CancelInvoke("DeactivateTrail"); // Ensure that the invoke is cancelled if the object is disabled before the time is up
    }
}
