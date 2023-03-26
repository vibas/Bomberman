using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Generates Random Level Map 
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] Tilemap destructibleTilemap;
    [SerializeField] Tile destructibleTile;
    [Range(5,100)]
    [SerializeField] int brickSpawnPercentage;

    Vector2Int xAxisRange = new Vector2Int(-6, 7);
    Vector2Int yAxisRange = new Vector2Int(-3, 5);

    private Dictionary<Vector3Int,bool> AllCellOnDestructibleTilemap;

    private void OnEnable()
    {
        Events.OnNewRoundRequestedEvent += CreateNewLevel;
    }

    private void OnDisable()
    {
        Events.OnNewRoundRequestedEvent -= CreateNewLevel;
    }

    void CreateNewLevel()
    {
        AllCellOnDestructibleTilemap = new Dictionary<Vector3Int, bool>();
        SpawnTileOnTilemap();
    }

    void SpawnTileOnTilemap()
    {
        // Grid in 0 & 1
        // 1 - Empty Tile. Can be travelled / placed brick
        // 0 - Blocked tile. Can not be travelled. Will not blast on explosion

        /*  1 1 1 1 1 1 1 1 1 1 1 1 1
         *  1 0 1 0 1 0 1 0 1 0 1 0 1
         *  1 1 1 1 1 1 1 1 1 1 1 1 1
         *  1 0 1 0 1 0 1 0 1 0 1 0 1
         *  1 1 1 1 1 1 1 1 1 1 1 1 1
         *  1 0 1 0 1 0 1 0 1 0 1 0 1
         *  1 1 1 1 1 1 1 1 1 1 1 1 1
         *  1 0 1 0 1 0 1 0 1 0 1 0 1
         */

        if (destructibleTilemap == null || destructibleTile == null)
        {
            Debug.LogError("Please assign the tilemap and tile to generate level map");
            return;
        }

        destructibleTilemap.ClearAllTiles();

        bool shouldSkipX = false;
        bool shouldSkipY = false;

        for (int x = xAxisRange.x; x < xAxisRange.y; x++)
        {   
            shouldSkipX = !x.IsEven();
            
            for (int y = yAxisRange.x; y < yAxisRange.y; y++)
            {   
                if(!IsPlayerSafeZone(x,y)) // Do not use player's safe zone or player will be blocked
                {
                    shouldSkipY = !Mathf.Abs(y).IsEven();
                    if (shouldSkipX && shouldSkipY)
                    {   
                        continue;
                    }

                    if (ShouldSpawnBrickTileBasedOnPercentage(brickSpawnPercentage))
                    {   
                        destructibleTilemap.SetTile(new Vector3Int(x, y), destructibleTile);
                        AllCellOnDestructibleTilemap.Add(new Vector3Int(x, y), true);
                    }
                    else
                    {   
                        AllCellOnDestructibleTilemap.Add(new Vector3Int(x, y),false);
                    }
                }
            }
        }
        Events.OnNewLevelGeneratedEvent?.Invoke(AllCellOnDestructibleTilemap);
    }

    bool IsPlayerSafeZone(int x, int y)
    {   
        return x <= xAxisRange.x + 1 && y >= yAxisRange.y - 2;
    }

    bool ShouldSpawnBrickTileBasedOnPercentage(int percentage)
    {
        System.Random random = new System.Random();
        int spawnPercentage = random.Next(0, 100);
        return spawnPercentage <= percentage ? true : false;
    }
}