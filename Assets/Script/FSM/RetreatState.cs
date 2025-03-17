using UnityEngine;

public class RetreatState :  BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Entering Retreat State");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting Retreat State");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.player != null)
        {
            enemy.navMeshAgent.destination = enemy.transform.position - enemy.player.transform.position;

        }
    }
}
