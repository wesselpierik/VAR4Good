using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class HandAnimations : MonoBehaviour
{
    public InputActionReference gripInputAction;
    public InputActionReference triggerInputAction;

    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _handAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Grip();
        Trigger();
    }

    private void Grip()
    {
        _gripValue = gripInputAction.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", _gripValue);
    }

    private void Trigger()
    {
        _triggerValue = triggerInputAction.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
    }
}
