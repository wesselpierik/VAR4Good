using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    static bool hand = true; // RIGHT HAND
    public static void ChangeTransform(SelectEnterEventArgs args) {

    }

    public static void ChangeTransform(SelectExitEventArgs args) {
        Debug.Log(args.interactableObject);

        IXRInteractor interactor = args.interactorObject;

        if (interactor.transform.name == "Left Controller") {
            Debug.Log("The left hand loses it");
        }
    }

    public override Transform GetAttachTransform(IXRInteractor interactor) {
        if ((interactor.transform.name == "Left Controller" && hand == true) || (interactor.transform.name == "Right Controller" && hand == false)) {
            Vector3 newLocalPosition = attachTransform.localPosition;
            newLocalPosition.z = -newLocalPosition.z;
            attachTransform.localPosition = newLocalPosition;
            hand = !hand;
        }

        return base.GetAttachTransform(interactor);
    }
}
