using Godot;
using System;

[Tool]
public partial class DirtRandomaizer : Node
{
    [Export] public TileMapLayer TileMap { get; set; }
    [Export] public bool Activate { get => false;
        set 
        {
            ReplaceTiles();
        } 
    }

    public void ReplaceTiles()
    {
        Vector2I[] sourceTiles = new Vector2I[] {
            new Vector2I(0, 0), new Vector2I(1, 0), new Vector2I(2, 0), new Vector2I(3, 0), new Vector2I(4, 0), new Vector2I(5, 0), new Vector2I(6, 0), new Vector2I(7, 0),
            new Vector2I(0, 1), new Vector2I(1, 1), new Vector2I(2, 1), new Vector2I(3, 1), new Vector2I(4, 1), new Vector2I(5, 1), new Vector2I(6, 1), new Vector2I(7, 1),
        };
        Vector2I[] replacementTiles = new Vector2I[] { 
            new Vector2I(0, 0), new Vector2I(1, 0), new Vector2I(2, 0), new Vector2I(3, 0), new Vector2I(4, 0), new Vector2I(5, 0), new Vector2I(6, 0), new Vector2I(7, 0),
            new Vector2I(0, 1), new Vector2I(1, 1), new Vector2I(2, 1), new Vector2I(3, 1), new Vector2I(4, 1), new Vector2I(5, 1), new Vector2I(6, 1), new Vector2I(7, 1),
        };

        if (TileMap == null || sourceTiles.Length == 0 || replacementTiles.Length == 0)
            return;
        var usedCells = TileMap.GetUsedCells();

        foreach (Vector2I cell in usedCells)
        {
            var tileData = TileMap.GetCellTileData(cell);
            if (tileData != null)
            {
                var atlasCoords = TileMap.GetCellAtlasCoords(cell);

                if (Array.Exists(sourceTiles, tile => tile == atlasCoords))
                {
                    var randomIndex = GD.RandRange(0, replacementTiles.Length - 1);
                    var sourceId = TileMap.GetCellSourceId(cell);

                    TileMap.SetCell(cell, sourceId, replacementTiles[randomIndex]);
                }
            }
        }
    }
}
