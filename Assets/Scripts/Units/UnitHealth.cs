using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitHealth : MonoBehaviour
{
    public float health = 100f;
    private float originalHealth;

    // Start is called before the first frame update
    void Start()
    {
        originalHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetHealth() <= 0f)
        {
            if (gameObject.GetComponent<Unit>())
            {
                if (gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Scout)
                {
                    UnitTracker.instance.scoutCount--;
                }
                if (gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Ranged)
                {
                    UnitTracker.instance.rangedCount--;
                }
                if (gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Melee)
                {
                    UnitTracker.instance.meleeCount--;
                }
                if (gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Tank)
                {
                    UnitTracker.instance.tankCount--;
                }
            }
            if (gameObject.GetComponent<EnemyUnit>())
            {
                if (gameObject.GetComponent<EnemyUnit>().enemyUnitType == EnemyUnit.EnemyUnitType.Scout)
                {
                    EnemyUnitTracker.instance.scoutCount--;
                }
                if (gameObject.GetComponent<EnemyUnit>().enemyUnitType == EnemyUnit.EnemyUnitType.Ranged)
                {
                    EnemyUnitTracker.instance.rangedCount--;
                }
                if (gameObject.GetComponent<EnemyUnit>().enemyUnitType == EnemyUnit.EnemyUnitType.Melee)
                {
                    EnemyUnitTracker.instance.meleeCount--;
                }
                if (gameObject.GetComponent<EnemyUnit>().enemyUnitType == EnemyUnit.EnemyUnitType.Tank)
                {
                    EnemyUnitTracker.instance.tankCount--;
                }
                gameObject.transform.position = new Vector3(-170.0f, 1.0f, -130.0f);
            }
            health = originalHealth;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void DecreaseHealth(float newValue)
    {
        health -= newValue;
    }

    public void IncreaseHealth(float newValue)
    {
        health += newValue;
    }

    public float GetHealth()
    {
        return health;
    }
}
