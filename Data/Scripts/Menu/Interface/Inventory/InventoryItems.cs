using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class InventoryItems : Control
{
    const int FIRST_ACTIVE_ITEM = 16;
	private PlayerInventory _playerInventory;
	private int lineCount = 0;
    private float CellYSize;

    public List<Cell> Cells { get; private set; } = new List<Cell>();
    [Export] public int SellInLine { get; set; } = 6;
    [Export] public ItemType Type { get; set; } = ItemType.Item;
    [Export] public float PixelYSize { get; set; }

    public override void _Ready()
    {
        Global.SceneObjects.PlayerChanged += TakePlayer;
    }

    public void TakePlayer(Node player)
    {
        _playerInventory = Global.Inventory;
        ShowInventory();
    }

    public override void _ExitTree() =>
        Global.SceneObjects.PlayerChanged -= TakePlayer;

    public void ShowInventory()
    {
        AddCells();
        float cellSize = AddCells();
        float bufferSize = cellSize / 16;
        if (Type == ItemType.Shard)
        {
            float angelDistance = 2 * MathF.PI / 3;
            Cell mainCell = Cell.CreateCell(new Vector2(((cellSize + bufferSize) * SellInLine - cellSize) / 2, -Size.Y * 0.8f), new Vector2(cellSize, cellSize), this, FIRST_ACTIVE_ITEM);
            Label label = new Label
            {
                Text = 0.ToString()
            };
            mainCell.AddChild(label);
            AddChild(mainCell);
            for (int i = 0; i < 3; i++)
            {
                Cell cell = Cell.CreateCell(mainCell.Position + new Vector2(MathF.Cos(i * angelDistance - MathF.PI / 2), MathF.Sin(i * angelDistance - MathF.PI / 2)) * cellSize * 1.2f, new Vector2(cellSize, cellSize), this, FIRST_ACTIVE_ITEM + i + 1);
                label = new Label
                {
                    Text = i.ToString()
                };
                cell.AddChild(label);
                AddChild(cell);
            }
            StateCellMethods.CheckActiveShards();
        }
        else if (Type == ItemType.Armor)
        {
            Cell mainCell = Cell.CreateCell(new Vector2(((cellSize + bufferSize) * SellInLine - cellSize) / 2, -Size.Y * 0.8f), new Vector2(cellSize, cellSize), this, FIRST_ACTIVE_ITEM);
            Label label = new Label
            {
                Text = 0.ToString()
            };
            mainCell.AddChild(label);
            AddChild(mainCell);
        }
    }

    public float AddCells()
    {
        float cellSize = Size.Y * 32f/PixelYSize;
        for (int i = 0; i < SellInLine; i++)
        {
            float sizeBuffer = 2f/PixelYSize;
            Cell cell = Cell.CreateCell(new Vector2(i * (cellSize + Size.Y * sizeBuffer), lineCount * (cellSize + Size.Y * sizeBuffer)), new Vector2(cellSize, cellSize), this, i + (lineCount * SellInLine));
            Label label = new Label
            {
                Text = (i + lineCount * SellInLine).ToString()
            };
            cell.AddChild(label);
            AddChild(cell);
            Cells.Add(cell);
        }
        lineCount++;
        return cellSize;
    }
}
