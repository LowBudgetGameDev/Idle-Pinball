using Godot;
using System;

public partial class WinUI : Control
{
	[Export] private AnimationPlayer animationPlayer;
	[Export] private Button closeButton;

	private bool isOpen;
	
	public override void _Ready()
	{
		ScoreManager.Instance.OnScoreChanged += (object sender, EventArgs e) => {
			if (ScoreManager.Instance.GetScore() > 1000000 && !isOpen){
				animationPlayer.Play("OpenWinUI");
				isOpen = true;
			}
		};

		closeButton.Pressed += () => {
			Hide();
		};
	}

	
	public override void _Process(double delta)
	{

	}
}
