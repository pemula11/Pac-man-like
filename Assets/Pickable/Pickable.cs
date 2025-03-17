using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] public PickableType pickableType;

    public Action<Pickable> onPicked;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with " + other.name);
            Debug.Log("Pickable type: " + pickableType);

            onPicked?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
