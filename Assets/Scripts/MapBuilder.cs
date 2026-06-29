using System.Collections.Generic;
using UnityEngine;

public class MapBuilder
{
    private static int GridWidth = 4;
    private static int GridHeight = 6;

    public virtual List<List<TerrainType>> GenerateMap()
    {
        var map = new List<List<TerrainType>>();

        for (var width = 0; width < GridWidth; width++)
        {
            var row = new List<TerrainType>();
            for (var height = 0; height < GridHeight; height++)
            { 
                row.Add(TerrainType.GRASS);
            }
            map.Add(row);
        }
            
        return map;
    }
}
