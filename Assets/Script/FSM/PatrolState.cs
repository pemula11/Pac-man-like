using UnityEngine;

public class PatrolState :  BaseState
{
    private bool _isMoving = false;
    private Vector3 _destination;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EnterState(Enemy enemy)
    {
        _isMoving = false;
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting Patrol State");
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) <= enemy.chaseDistance)
        {
            enemy.switchState(enemy.chaseState);
        }

        if (_isMoving == false)
        {
            _isMoving = true;
            int index = UnityEngine.Random.Range(0, enemy.wayPoint.Count);
            _destination = enemy.wayPoint[index].position;
            enemy.navMeshAgent.SetDestination(_destination);
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, _destination) <= .1f)
            {
                _isMoving = false;
            }
        }
    }
}
