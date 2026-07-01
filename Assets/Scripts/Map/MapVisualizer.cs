using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private GameObject grassPrefab;

    public List<List<GameObject>> Grid { get; private set; }

    public void BuildGrid(List<List<TerrainType>> map)
    {
        Grid = new List<List<GameObject>>();

        for (var row = 0; row < map.Count; row++)
        {
            var gridRow = new List<GameObject>();
            for (var column = 0; column < map[row].Count; column++)
            {
                var gridCell = Instantiate(grassPrefab, transform);
                gridCell.transform.localPosition = new Vector3(column * gridCellSize, row * gridCellSize, 1);
                gridRow.Add(gridCell);
            }
            Grid.Add(gridRow);
        }
    }

    public bool IsValidPosition(Vector2Int position, List<List<TerrainType>> map)
    {
        return position.x >= 0 &&
               position.x < map[0].Count &&
               position.y >= 0 &&
               position.y < map.Count;
    }

    public void MoveCharacterToCell(GameObject character, int row, int column)
    {
        GameObject gridCell = Grid[row][column];
        character.transform.SetParent(gridCell.transform);
        character.transform.localPosition = Vector3.zero;
    }
}