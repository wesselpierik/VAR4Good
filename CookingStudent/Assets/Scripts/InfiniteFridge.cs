using UnityEngine;

public class InfiniteFridge : MonoBehaviour
{
    public GameObject lettuce;
    public GameObject tomato;
    public GameObject onion;
    public GameObject cheese;
    public GameObject burger;
    public GameObject bunTop;
    public GameObject bunBottom;

    GameObject GetNewObject(string name)
    {
        switch (name)
        {
            case "food_ingredient_lettuce":
                return lettuce;
            case "food_ingredient_tomato":
                return tomato;
            case "food_ingredient_onion":
                return onion;
            case "food_ingredient_cheese":
                return cheese;
            case "food_ingredient_burger_uncooked":
                return burger;
            case "food_ingredient_bun_top":
                return bunTop;
            case "food_ingredient_bun_bottom":
                return bunBottom;
            default:
                Debug.Log("Unknown fridge object: " + name);
                return lettuce; // default to lettuce, but this should never happen
        }
    }

    Transform GetSpawnPoint(string name)
    {
        Debug.Log(name);
        return GameObject.Find($"{name}_spawnPoint").transform;
    }


    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ingredient")) return;

        Transform spawnPoint = GetSpawnPoint(other.gameObject.name);
        Vector3 newPos = spawnPoint.position;

        GameObject newObject = Instantiate(GetNewObject(other.name), newPos, Quaternion.identity);
        newObject.name = other.name;

        other.GetComponent<Contamination>().canReceiveContamination = true;

        other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        other.gameObject.layer = 6;
    }
}
