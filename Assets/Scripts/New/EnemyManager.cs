using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    //public List<Capture> allCapturePoints;
    public List<EnemyUnitData> allData;

    private List<EnemyUnitStats> enemyUnitsStats = new List<EnemyUnitStats>();
    private int enemyUnitCount = 0;
    private int enemyMaxUnitCount = 0;
    private float minSpawnTime = 1.0f;
    private float maxSpawnTime = 5.0f;
    private Coroutine spawnCoroutine;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyUnitsStats.Add(new EnemyUnitStats(null, allData[0]._cost, allData[0]._probability, allData[0]._name));
        enemyUnitsStats.Add(new EnemyUnitStats(null, allData[1]._cost, allData[1]._probability, allData[1]._name));
        enemyUnitsStats.Add(new EnemyUnitStats(null, allData[2]._cost, allData[2]._probability, allData[2]._name));
        enemyUnitsStats.Add(new EnemyUnitStats(null, allData[3]._cost, allData[3]._probability, allData[3]._name));

        spawnCoroutine = StartCoroutine(SpawnEnemyRandom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemyRandom()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnEnemyUnit();
        }
    }

    public void SpawnEnemyUnit()
    {
        float totalProbability = 0.0f;
        
        foreach (var item in enemyUnitsStats)
        {
            totalProbability += item.Probability();
        }

        float randomValue = Random.Range(0.0f, totalProbability);

        for (int i = 0; i < enemyUnitsStats.Count; i++)
        {
            if (randomValue < enemyUnitsStats[i].Probability())
            {
                CheckEnemyUnitCount(enemyUnitsStats[i]);
                if ((ResourceManager.instance.Bgold >= enemyUnitsStats[i].Cost()) && enemyUnitCount < enemyMaxUnitCount)
                {
                    enemyUnitsStats[i].Clone().Spawn();
                    ResourceManager.instance.Bgold -= enemyUnitsStats[i].Cost();
                }
                else
                {
                    //Debug.Log("CANNOT SPAWN");
                }
            }
            else
            {
                randomValue -= enemyUnitsStats[i].Probability();
            }
        }
    }

    public void CheckEnemyUnitCount(EnemyUnitStats enemyUnitStat)
    {
        if (enemyUnitStat.Name().Contains("Scout"))
        {
            enemyUnitCount = EnemyUnitTracker.instance.scoutCount;
            enemyMaxUnitCount = 10;
        }
        if (enemyUnitStat.Name().Contains("Ranged"))
        {
            enemyUnitCount = EnemyUnitTracker.instance.rangedCount;
            enemyMaxUnitCount = 6;
        }
        if (enemyUnitStat.Name().Contains("Melee"))
        {
            enemyUnitCount = EnemyUnitTracker.instance.meleeCount;
            enemyMaxUnitCount = 4;
        }
        if (enemyUnitStat.Name().Contains("Tank"))
        {
            enemyUnitCount = EnemyUnitTracker.instance.tankCount;
            enemyMaxUnitCount = 2;
        }
    }
}
