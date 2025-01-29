using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    private bool hand = true; // RIGHT HAND
    private bool pan = false;

    public override Transform GetAttachTransform(IXRInteractor interactor)
    {
        if (attachTransform == null) { return base.GetAttachTransform(interactor); }

        if ((interactor.transform.parent.name == "Left Controller" && hand == true) || (interactor.transform.parent.name == "Right Controller" && hand == false))
        {
            Vector3 newLocalPosition = attachTransform.localPosition;
            newLocalPosition.z = -newLocalPosition.z;
            attachTransform.localPosition = newLocalPosition;
            hand = !hand;
        }

        return base.GetAttachTransform(interactor);
    }

    public void PanTrigger(bool value)
    {
        pan = value;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        IXRSelectInteractable obj = args.interactableObject;
        Rigidbody rb = obj.transform.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        if (obj.transform.CompareTag("Ingredient") && pan)
        {
            PanTrigger(false);
            obj.transform.parent = null;
            obj.transform.GetComponent<Collider>().excludeLayers = 0;
        }

        base.OnSelectExited(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        IXRSelectInteractable obj = args.interactableObject;

        if (obj.transform.CompareTag("Ingredient") && pan) {
            IngredientCooking ingredient = obj.transform.GetComponent<IngredientCooking>();
            ingredient.StopCooking();
        }

        base.OnSelectEntered(args);
    }
}
