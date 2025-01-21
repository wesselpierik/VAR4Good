using UnityEngine;
using UnityEngine.InputSystem;

public class OpenRecipe : MonoBehaviour
{
    public InputActionReference openRecipe;
    public Canvas canvas;

    private bool showCanvas = true;
    void Start()
    {
        openRecipe.action.started += ButtonPressed;
        //openRecipe.action.canceled += ButtonReleased;
    }

    void ButtonPressed(InputAction.CallbackContext context)
    {
        canvas.enabled = showCanvas;
        showCanvas = !showCanvas;
    }

    //void ButtonReleased(InputAction.CallbackContext context)
    //{
    //    canvas.enabled = true;
    //}

}
