using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnemyStartState : IState
{
    EnemyStateCycle stateCycle;
    List<EnemyCapture> enemyCapture;

    public EnemyStartState(EnemyStateCycle stateCycle, List<EnemyCapture> enemyCapture)
    {
        this.stateCycle = stateCycle;
        this.enemyCapture = enemyCapture;
    }

    public void Enter()
    {
        if (stateCycle.GetComponent<Animator>())
        {
            stateCycle.GetComponent<Animator>().SetBool("isStart", true);
        }
    }

    public void Tick()
    {
        GoCapturePoint();
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        if (stateCycle.GetComponent<Animator>())
        {
            stateCycle.GetComponent<Animator>().SetBool("isStart", false);
        }
    }

    private void GoCapturePoint()
    {
        float totalProbability = 0.0f;

        foreach (var item in enemyCapture)
        {
            totalProbability += item.probability;
        }

        float randomValue = Random.Range(0.0f, totalProbability);

        for (int i = 0; i < enemyCapture.Count; i++)
        {
            if (randomValue < enemyCapture[i].probability)
            {
                EnemyObjectPooler.instance.chosenPoint = enemyCapture[i].capturePoint;
                stateCycle.ChangeState(stateCycle.moveState);
            }
            else
            {
                randomValue -= enemyCapture[i].probability;
            }
        }
    }
}
