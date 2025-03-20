
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{

    private List<Pickable> pickables = new List<Pickable>();
    [SerializeField] private Player player;
    [SerializeField] ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        initPickableList();
    }

    private void initPickableList()
    {
        Pickable[] pickableArray = GameObject.FindObjectsByType<Pickable>(FindObjectsSortMode.None);
        foreach (Pickable pickable in pickableArray)
        {
            pickable.onPicked += OnPickablePicked;
            pickables.Add(pickable);
        }
        Debug.Log("Pickable list initialized with " + pickables.Count + " pickables");

        scoreManager.SetMaxScore(pickables.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        Debug.Log("Pickable left" + pickables.Count);
        if (pickable.pickableType == PickableType.PowerUp)
        {
          
            player?.pickPowerUp();
        }

        pickables.Remove(pickable);
        if (pickables.Count <= 0)
        {
            Debug.Log("All pickables have been picked");
        }
        scoreManager?.AddScore(1);
    }
}