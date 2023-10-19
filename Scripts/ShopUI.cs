using Godot;
using System;
using System.Diagnostics;

public partial class ShopUI : Control
{
	[Export] private Button buyBallButton;
	[Export] private RichTextLabel maxBallAmountText;
	[Export] private RichTextLabel ballCostText;
	[Export] private Button buyPegButton;
	[Export] private RichTextLabel maxPegAmountText;
	[Export] private RichTextLabel pegCostText;
	[Export] private Button buyUpgradeButton;
	[Export] private RichTextLabel multiplierText;
	[Export] private RichTextLabel upgradeCostText;
	[Export] private Button openShopButton;
	[Export] private Button closeShopButton;
	[Export] private RichTextLabel scoreText;
	[Export] private AnimationPlayer animationPlayer;

	private int startBallCost = 10;
	private int ballsBought;
	private int startPegCost = 2;
	private int pegsBought;
	private int startUpgradeCost = 100;
	private int upgradesBought;
	private bool isShopOpen;
	
	public override void _Ready()
	{
		buyBallButton.Pressed += () => {
			if (!IsScoreHighEnough(CalculateBallCost())) return;

			int cost = CalculateBallCost();
			ObjectAmountManager.Instance.IncreaseMaxAmount(ObjectAmountManager.Object.Ball);
			ballsBought++;
			UpdateCosts();
			ScoreManager.Instance.DecreaseScore(cost);
		};

		buyPegButton.Pressed += () => {
			if (!IsScoreHighEnough(CalculatePegCost())) return;

			int cost = CalculatePegCost();
			ObjectAmountManager.Instance.IncreaseMaxAmount(ObjectAmountManager.Object.Peg);
			pegsBought++;
			UpdateCosts();
			ScoreManager.Instance.DecreaseScore(cost);
		};

		buyUpgradeButton.Pressed += () => {
			if (!IsScoreHighEnough(CalculateUpgradeCost())) return;

			int cost = CalculateUpgradeCost();
			ScoreManager.Instance.IncreaseScoreMultiplier();
			upgradesBought++;
			UpdateCosts();
			UpdateAmounts();
			ScoreManager.Instance.DecreaseScore(cost);
		};

		openShopButton.Pressed += () => {
			if (!isShopOpen) {
				animationPlayer.Play("OpenShop");
				isShopOpen = true;
			}
		};

		closeShopButton.Pressed += () => {
			if (isShopOpen) {
				animationPlayer.Play("CloseShop");
				isShopOpen = false;
			}
		};

		ObjectAmountManager.Instance.OnObjectMaxAmountChanged += (object sender, EventArgs e) => {
			UpdateAmounts();
		};

		ScoreManager.Instance.OnScoreChanged += (object sender, EventArgs e) => {
			scoreText.Text = "[center]$" + ScoreManager.Instance.GetScore() + "[/center]";
			ToggleButtonDisable();
		};

		UpdateCosts();
		UpdateAmounts();
		ToggleButtonDisable();
		scoreText.Text = "[center]$" + ScoreManager.Instance.GetScore() + "[/center]";
		isShopOpen = false;
	}

	private void ToggleButtonDisable(){
		if (!IsScoreHighEnough(CalculateBallCost())){
				buyBallButton.Disabled = true;
			}else{
				buyBallButton.Disabled = false;
			}

			if (!IsScoreHighEnough(CalculatePegCost())){
				buyPegButton.Disabled = true;
			}else{
				buyPegButton.Disabled = false;
			}

			if (!IsScoreHighEnough(CalculateUpgradeCost())){
				buyUpgradeButton.Disabled = true;
			}else{
				buyUpgradeButton.Disabled = false;
			}
	}

    private void UpdateCosts(){
		ballCostText.Text = "[right]$" + CalculateBallCost() + "[/right]";
		pegCostText.Text = "[right]$" + CalculatePegCost() + "[/right]";
		upgradeCostText.Text = "[right]$" + CalculateUpgradeCost() + "[/right]";
	}

	private void UpdateAmounts(){
		maxBallAmountText.Text = ObjectAmountManager.Instance.GetMaxAmount(ObjectAmountManager.Object.Ball).ToString();
		maxPegAmountText.Text = ObjectAmountManager.Instance.GetMaxAmount(ObjectAmountManager.Object.Peg).ToString();
		multiplierText.Text = "x" + ScoreManager.Instance.GetScoreMultiplier().ToString();
	}

	private bool IsScoreHighEnough(int price){
		return ScoreManager.Instance.GetScore() >= price;
	}

	private int CalculateBallCost(){
		return Mathf.FloorToInt(15 * ScoreManager.Instance.GetScoreMultiplier() * Mathf.Log(ballsBought + 1) + startBallCost);
	}

	private int CalculatePegCost(){
		return Mathf.FloorToInt(8 * ScoreManager.Instance.GetScoreMultiplier()* Mathf.Log(pegsBought + 1) + startPegCost);
	}

	private int CalculateUpgradeCost(){
		return Mathf.FloorToInt(200 * upgradesBought * Mathf.Log(upgradesBought * upgradesBought * upgradesBought + 1) + startUpgradeCost);
	}
}
