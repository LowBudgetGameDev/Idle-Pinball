using Godot;
using System;

public class PegGrid
{
	private Node2D[,] gridArray;

	private int width;
	private int height;
	private float cellSize;
	private Vector2 originPosition;

	public PegGrid(Vector2 originPosition, int width, int height, float cellSize){

		gridArray = new Node2D[width, height];

		this.width = width;
		this.height = height;
		this.cellSize = cellSize;
		this.originPosition = originPosition;
	}

	public void GetXYFromWorldPosition(Vector2 worldPosition, out int x, out int y){
		worldPosition -= originPosition;

		y = Mathf.FloorToInt(worldPosition.Y / cellSize);

		if (y % 2 == 0){
			x = Mathf.FloorToInt(worldPosition.X / cellSize - 0.5f);
		}else{
			x = Mathf.FloorToInt(worldPosition.X / cellSize);
		}
	}

	public Vector2 GetWorldPositionFromXY(int x, int y){
		Vector2 worldPosition = new Vector2();

		worldPosition.X = y % 2 == 0 ? originPosition.X + cellSize * x + cellSize/2 : originPosition.X + cellSize * x;
		worldPosition.Y = originPosition.Y + cellSize * y;

		return worldPosition;
	}

	public void SetValue(Node2D value, int x, int y){
		if (!IsValidXY(x, y)) return;

		value.Position = GetWorldPositionFromXY(x, y) + Vector2.One * cellSize / 2;

		if (gridArray[x, y] != null) gridArray[x, y].QueueFree();
		
		gridArray[x, y] = value;
	}

	public void SetValue(Node2D value, Vector2 worldPosition){
		GetXYFromWorldPosition(worldPosition, out int x, out int y);
		
		SetValue(value, x, y);
	}

	public Node2D GetValue(int x, int y){
		return gridArray[x, y];
	}

	public Node2D GetValue(Vector2 worldPosition){
		GetXYFromWorldPosition(worldPosition, out int x, out int y);

		return GetValue(x, y);
	}

	public bool IsValidXY(int x, int y){
		return x >= 0 && x < width && y >=0 && y < height;
	}

	public bool IsValidWorldPosition(Vector2 worldPosition){
		GetXYFromWorldPosition(worldPosition, out int x, out int y);

		return IsValidXY(x, y);
	}

	public int GetWidth(){
		return width;
	}

	public int GetHeight(){
		return height;
	}
}
