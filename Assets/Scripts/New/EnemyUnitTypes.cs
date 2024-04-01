using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyUnitTypes
{
    public abstract GameObject Spawn();
    public abstract EnemyUnitTypes Clone();
    public abstract int Cost();
    public abstract float Probability();
    public abstract string Name();
}

public class EnemyUnitStats : EnemyUnitTypes
{
    private int cost;
    private float probability;
    private string name;
    private GameObject clone;

    public EnemyUnitStats(GameObject clone, int cost, float probability, string name)
    {
        this.cost = cost;
        this.probability = probability;
        this.clone = clone;
        this.name = name;
    }

    public override GameObject Spawn()
    {
        return clone;
    }

    public override EnemyUnitTypes Clone()
    {
        return new EnemyUnitStats(EnemyObjectPooler.instance.SpawnFromPool(name), cost, probability, name);
    }

    public override int Cost()
    {
        return cost;
    }

    public override float Probability()
    {
        return probability;
    }

    public override string Name()
    {
        return name;
    }
}