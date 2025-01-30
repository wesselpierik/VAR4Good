using UnityEngine;

public class Table : MonoBehaviour
{
    string burgerName = "food_burger";
    UIManager uiManager;
    bool done = false;

    void OnTriggerEnter(Collider other) {
        if (!done && other.transform.name == burgerName) {
            uiManager.ToggleResult;
            done = true;
        }
    }
}
