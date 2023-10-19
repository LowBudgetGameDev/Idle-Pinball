using Godot;
using System;

public partial class ScoreUI : Node
{
	[Export] private RichTextLabel scoreText;

    public override void _Ready()
    {
        ScoreManager.Instance.OnScoreChanged += (object sender, EventArgs e) => {
			scoreText.Text = "[center]$" + ScoreManager.Instance.GetScore().ToString() + "[/center]";
		};

		scoreText.Text = "[center]$" + ScoreManager.Instance.GetScore().ToString() + "[/center]";
    }
}
