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
            case "food_ingredient_burger":
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


    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ingredient")) return;

        Transform spawnPoint = other.gameObject.transform.Find("spawnPoint");
        Vector3 newPos = spawnPoint.localPosition + transform.position;
        GameObject newObject = Instantiate(GetNewObject(other.name), newPos, Quaternion.identity);

        Contamination targetContamination = other.GetComponent<Contamination>();
        if (targetContamination != null)
        {
            targetContamination.canReceiveContamination = true;
        }

        other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        other.gameObject.layer = 6;
    }
}
