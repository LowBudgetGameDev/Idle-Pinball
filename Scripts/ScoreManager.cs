using Godot;
using System;

public partial class ScoreManager : Node2D
{
	public static ScoreManager Instance {get; private set;}
	
	public event EventHandler OnScoreChanged;

	private int score;
	private int scoreMultiplier;

	public ScoreManager(){
		Instance = this;

		scoreMultiplier = 1;
	}
	
	public void IncreaseScore(int amount){
		score += amount * scoreMultiplier;
		OnScoreChanged?.Invoke(this, EventArgs.Empty);
	}

	public void DecreaseScore(int amount){
		score -= amount;
		OnScoreChanged?.Invoke(this, EventArgs.Empty);
	}

	public void IncreaseScoreMultiplier(int amount = 1){
		scoreMultiplier += amount;
	}

	public int GetScore(){
		return score;
	}

	public int GetScoreMultiplier(){
		return scoreMultiplier;
	}
}
