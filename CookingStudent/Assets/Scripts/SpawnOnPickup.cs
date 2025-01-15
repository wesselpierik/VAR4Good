using UnityEngine;

public class SpawnOnPickup : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject objectPrefab;
    private bool hasBeenPickedUp = false;

    public void OnPickup()
    {
        if (hasBeenPickedUp) return;

        hasBeenPickedUp = true;
        GameObject prefabToSpawn = objectPrefab != null ? objectPrefab : gameObject;

        if (spawnLocation != null)
        {
            Instantiate(prefabToSpawn, spawnLocation.position, spawnLocation.rotation);
        }
    }
}