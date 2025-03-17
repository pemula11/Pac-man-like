using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    public List<Transform> wayPoint = new List<Transform>();
    [SerializeField] public float chaseDistance;
    [SerializeField] public Player player;


    private BaseState _currentState;

    [HideInInspector]
    public PatrolState patrolState = new PatrolState();
    [HideInInspector]
    public RetreatState retreatState =new RetreatState();
    [HideInInspector]
    public ChaseState chaseState = new ChaseState();
    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    private void Awake()
    {

        _currentState = patrolState;
        _currentState.EnterState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (player != null)
        {
            player.OnPowerUpEnd += stopRetreating;
            player.OnPowerUpStart += startRetreating;
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {

            _currentState.UpdateState(this);
        }

        

    }

    public void switchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void startRetreating()
    {
        Debug.Log("4444444444Retreating");
        switchState(retreatState);
    }

    private void stopRetreating()
    {
        switchState(patrolState);
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.OnPowerUpEnd -= stopRetreating;
            player.OnPowerUpStart -= startRetreating;
        }
    }


}
