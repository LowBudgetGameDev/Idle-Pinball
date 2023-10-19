using Godot;
using System;
using System.Diagnostics;

public partial class PegBuilding : Node2D
{

	[Export] private PackedScene pegPrefab;
	[Export] private PackedScene pegBuildingSpotPrefab;

	private PegGrid pegGrid;
	
	public override void _Ready()
	{
		pegGrid = new PegGrid(new Vector2(-560, -400), 7, 6, 150);
		ResetGrid();
	}

    public override void _UnhandledInput(InputEvent @event)
    {
		if (Input.IsActionJustReleased("LeftClick") && !ObjectAmountManager.Instance.IsObjectAtMaxAmount(ObjectAmountManager.Object.Peg)){
			BuildPeg(GetGlobalMousePosition());
		}
    }

    private void BuildPeg(Vector2 worldPosition){
		if (!pegGrid.IsValidWorldPosition(worldPosition) || pegGrid.GetValue(worldPosition) is StaticBody2D) return;

		Node2D peg = (Node2D) pegPrefab.Instantiate();
		AddChild(peg);
		pegGrid.SetValue(peg, worldPosition);
		ObjectAmountManager.Instance.IncreaseAmount(ObjectAmountManager.Object.Peg);
	}

	private void ResetGrid(){

		for (int i = 0; i < pegGrid.GetWidth(); i++){
			for (int j = 0; j < pegGrid.GetHeight(); j++){
				Node2D emptyBuildSpot = (Node2D) pegBuildingSpotPrefab.Instantiate();
				AddChild(emptyBuildSpot);
				pegGrid.SetValue(emptyBuildSpot, i, j);
			}
		}
	}
}
