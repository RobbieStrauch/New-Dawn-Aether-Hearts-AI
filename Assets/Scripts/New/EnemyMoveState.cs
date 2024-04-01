using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : IState
{
    EnemyStateCycle stateCycle;
    NavMeshAgent agent;
    GameObject chosenPoint;

    public EnemyMoveState(EnemyStateCycle stateCycle, NavMeshAgent agent)
    {
        this.stateCycle = stateCycle;
        this.agent = agent;
    }

    public void Enter()
    {
        chosenPoint = EnemyObjectPooler.instance.chosenPoint;
        agent.SetDestination(chosenPoint.transform.position);

        if (stateCycle.GetComponent<Animator>())
        {
            stateCycle.GetComponent<Animator>().SetBool("isStart", false);
            stateCycle.GetComponent<Animator>().SetBool("isAttack", false);
        }
    }

    public void Tick()
    {

    }

    public void FixedTick()
    {
        const int raycastCount = 100;

        for (int i = 0; i < raycastCount; i++)
        {
            Quaternion raycastRotation = Quaternion.AngleAxis((i / (float)raycastCount) * 180.0f - 90.0f, Vector3.up);
            Vector3 desiredDirection = stateCycle.transform.rotation * raycastRotation * Vector3.forward;
            desiredDirection.y = 0;

            RaycastHit hit;
            if (Physics.Raycast(stateCycle.transform.position, desiredDirection, out hit, stateCycle.attackRange, stateCycle.player2))
            {
                stateCycle.ChangeState(stateCycle.attackState);
            }
            else
            {
                Debug.DrawRay(stateCycle.transform.position, desiredDirection * stateCycle.attackRange, Color.white);
            }
        }

        if (Vector3.Distance(stateCycle.transform.position, chosenPoint.transform.position) < agent.stoppingDistance + 3)
        {
            if (chosenPoint.GetComponent<Capture>().BCaptureStatus)
            {
                stateCycle.ChangeState(stateCycle.startState);
            }
        }
    }

    public void Exit()
    {

    }
}
