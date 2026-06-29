using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private GameObject grassPrefab;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] enemies;

    public List<List<GameObject>> grid {get; private set;}
    
    public Dictionary<PlayerType, Vector2Int> playerPosition;
    public Dictionary<EnemyType, Vector2Int> enemyPosition;
    
    private List<List<TerrainType>> map;

    private void Start()
    {
        MapBuilder mapBuilder = new MapBuilder();
        map = mapBuilder.GenerateMap();

        InitializeDictionaries();
        InitializeMap(map);
    }

    private void InitializeDictionaries()
    {
        playerPosition = new Dictionary<PlayerType, Vector2Int>();
        enemyPosition = new Dictionary<EnemyType, Vector2Int>();
    }

    public Vector2Int GetPosibleNewPosition(int movx, int movy)
    {
        return new Vector2Int(0, 0);
        //return new Vector2Int( characterPosition.x + movx, characterPosition.y + movy);
    }

    public bool CheckIsValidPosition(Vector2Int position)
    {
        return position.x >= 0 &&
            position.x < map[position.y].Count &&
            position.y < map.Count &&
            position.y >= 0 &&
        map[position.y][position.x] != TerrainType.GRASS;
    }

    public void CheckWinCondition()
    {
    }

    public void InitializeMap(List<List<TerrainType>> map)
    {
        grid = new List<List<GameObject>>();

        for (var row = 0; row < map.Count; row++)
        {
            var gridRow = new List<GameObject>();
            for (var column = 0; column < map[row].Count; column++)
            {
                var gridCell = Instantiate(grassPrefab, transform);
                gridCell.transform.localPosition = new Vector3(column * gridCellSize, row * gridCellSize, 1);
                gridRow.Add(gridCell);
                PlaceCharacterOnMap(row, column);
            }
            grid.Add(gridRow);
        }
    }

    private void PlaceCharacterOnMap(int row, int column)
    {
        float randomNumber = Random.Range(0f, 1f);
        Debug.Log(randomNumber);
        if (randomNumber <= .5f)
        {
            for (int i = 0; i == players.Length - 1; i++)
            {
                if (!players[i].activeSelf)
                {
                    PlayerController playerController = players[i].GetComponent<PlayerController>();
                    PlayerType playerType = playerController.playerType;
                    playerPosition.Add(playerType, new Vector2Int(row, column));
                    
                    PlaceCharacterOnMap(players[i], row, column);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i == enemies.Length - 1; i++)
            {
                if (!enemies[i].activeSelf)
                {
                    EnemyController enemyController = enemies[i].GetComponent<EnemyController>();
                    EnemyType enemyType = enemyController.enemyType;
                    enemyPosition.Add(enemyType, new Vector2Int(row, column));
                    PlaceCharacterOnMap(enemies[i], row, column);
                    break;
                }
            }
        }
    }
    
    private void PlaceCharacterOnMap(GameObject character, int row, int column)
    {
        GameObject startGridCell = grid[row][column];
        character.SetActive(true);
        character.transform.SetParent(startGridCell.transform);
        character.transform.localPosition = Vector3.zero;
        Debug.Log(character);
    }
    
}

