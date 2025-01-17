using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    private bool hand = true; // RIGHT HAND

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
