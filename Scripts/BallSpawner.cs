using Godot;
using System;

public partial class BallSpawner : Node2D
{
	[Export] private Node2D spawnPoint;
	[Export] private PackedScene ballPrefab;

	private float spawnDelay = 1f;
	private float spawnDelayTimer;
	private float timePerCycle = 5f;
	private float distanceTravelledFromOrigin = 490f;

	private float time;
	
	public override void _Ready()
	{

	}

	
	public override void _Process(double delta)
	{
		time += (float) delta;

		Position = new Vector2(Mathf.Sin(time / timePerCycle * 2 * Mathf.Pi) * distanceTravelledFromOrigin, Position.Y);

		spawnDelayTimer -= (float) delta;

		if (spawnDelayTimer < 0){
			if (!ObjectAmountManager.Instance.IsObjectAtMaxAmount(ObjectAmountManager.Object.Ball)) SpawnBall();
			spawnDelayTimer += spawnDelay;
		}
	}

	private void SpawnBall(){
		Node2D ball = (Node2D) ballPrefab.Instantiate();
		ball.Position = spawnPoint.Position;
		AddChild(ball);
		ObjectAmountManager.Instance.IncreaseAmount(ObjectAmountManager.Object.Ball);
	}
}
