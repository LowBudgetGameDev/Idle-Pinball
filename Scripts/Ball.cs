using Godot;
using System;

public partial class Ball : RigidBody2D
{
	public override void _Ready()
	{
		ContactMonitor = true;
		MaxContactsReported = 3;

		BodyEntered += (Node body) => {
			if (body is StaticBody2D staticBody2D){
				int pegCollisionLayer = 1 << 0; // 1
				int despawnerCollisionLayer = 1 << 7; // 128

				if (staticBody2D.CollisionLayer == pegCollisionLayer){
					ScoreManager.Instance.IncreaseScore(1);
				} 

				if (staticBody2D.CollisionLayer == despawnerCollisionLayer){
					QueueFree();
					ObjectAmountManager.Instance.DecreaseAmount(ObjectAmountManager.Object.Ball);
				}
			}
		};
	}
}
