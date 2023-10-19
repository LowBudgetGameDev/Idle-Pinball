using Godot;
using System;

public partial class MouseBallSpawn : Node2D
{
	[Export] private PackedScene ballPrefab;

	private int spawnYPosition = -850;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("LeftClick")){
			Node2D ball = (Node2D) ballPrefab.Instantiate();
			ball.Position = new Vector2(GetGlobalMousePosition().X, spawnYPosition);
			AddSibling(ball);
		}
	}
}
