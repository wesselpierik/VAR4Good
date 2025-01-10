using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PokeToFinger : MonoBehaviour
{
    public Transform PokePoint;
    private XRPokeInteractor _pokeInteractor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pokeInteractor = transform.parent.parent.GetComponentInChildren<XRPokeInteractor>();
        SetPokePoint();
    }

    // Update is called once per frame
    void SetPokePoint()
    {
        _pokeInteractor.attachTransform = PokePoint;
    }
}
