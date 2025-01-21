using UnityEngine;

public class SpawnOnPickup : MonoBehaviour
{
    public Transform spawnlocation;
    public GameObject objectPrefab;
    private bool HasBeenPickedUp = false;

    public void OnPickup()
    {
        if (HasBeenPickedUp) return;

        HasBeenPickedUp = true;
        GameObject prefabToSpawn = objectPrefab != null ? objectPrefab : gameObject;

        if (spawnlocation != null)
        {
            Instantiate(prefabToSpawn = objectPrefab, spawnlocation.position, spawnlocation.rotation);
                
        }
    }
}