using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    int totalEnemyCount;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Grid grid;

    private int enemyDiedCount = 0;

    private void OnEnable()
    {   
        Events.OnNewLevelGeneratedEvent += SpawnEnemysAtRandomCell;
        Events.OnEnemyDeathSequenceCompleteEvent += CheckEnemyCount;
    }

    private void OnDisable()
    {
        Events.OnNewLevelGeneratedEvent -= SpawnEnemysAtRandomCell;
        Events.OnEnemyDeathSequenceCompleteEvent -= CheckEnemyCount;
    }

    void SpawnEnemysAtRandomCell(Dictionary<Vector3Int,bool> allCellDict)
    {
        enemyDiedCount = 0;

        if(allCellDict.Count >= totalEnemyCount)
        {
            var emptyCells = allCellDict.Where(item => item.Value == false).Select(item => item.Key).ToList();
            int spawnedEnemyCount = 0;
            while(spawnedEnemyCount<totalEnemyCount)
            {
                int randomIndex = Random.Range(0, emptyCells.Count);
                Vector3Int pos = emptyCells[randomIndex];
                Instantiate(enemyPrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                spawnedEnemyCount++;
                emptyCells.RemoveAt(randomIndex);
            }
        }
        else
        {
            Debug.LogError("Not much cells available to spawn required number of enemies!");
            totalEnemyCount = allCellDict.Count;
            SpawnEnemysAtRandomCell(allCellDict);
        }
        
    }

    void CheckEnemyCount()
    {
        enemyDiedCount++;
        if(enemyDiedCount == totalEnemyCount)
        {
            Events.OnAllEnemiesClearedEvent?.Invoke();
        }
    }
}
