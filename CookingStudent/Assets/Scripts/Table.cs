using UnityEngine;

public class Table : MonoBehaviour
{
    private string burgerName = "food_burger";
    public UIManager uiManager;
    private bool done = false;

    void OnTriggerEnter(Collider other) {
        if (!done && other.transform.name == burgerName) {
            uiManager.ToggleResult();
            done = true;
        }
    }
}
