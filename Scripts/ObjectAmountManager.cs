using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectAmountManager : Node
{
	public static ObjectAmountManager Instance {get; private set;}

	public event EventHandler OnObjectMaxAmountChanged;

	public enum Object{
		Ball,
		Peg,
	}

	private List<int> startingAmounts = new List<int>{1, 3};
	private Dictionary<Object, int> objectMaxAmountDictionary;
	private Dictionary<Object, int> objectAmountDictionary;
	
	public ObjectAmountManager(){
		Instance = this;

		objectMaxAmountDictionary = new Dictionary<Object, int>();

		foreach (Object obj in Enum.GetValues(typeof(Object))){
			objectMaxAmountDictionary[obj] = startingAmounts[(int) obj];
		}

		objectAmountDictionary = new Dictionary<Object, int>();

		foreach (Object obj in Enum.GetValues(typeof(Object))){
			objectAmountDictionary[obj] = 0;
		}
	}

	public int GetMaxAmount(Object obj){
		return objectMaxAmountDictionary[obj];
	}

	public int GetAmount(Object obj){
		return objectAmountDictionary[obj];
	}

	public void IncreaseMaxAmount(Object obj, int amount = 1){
		objectMaxAmountDictionary[obj] += amount;
		OnObjectMaxAmountChanged?.Invoke(this, EventArgs.Empty);
	}

	public void IncreaseAmount(Object obj, int amount = 1){
		objectAmountDictionary[obj] += amount;
	}

	public void DecreaseMaxAmount(Object obj, int amount = 1){
		objectMaxAmountDictionary[obj] -= amount;
		OnObjectMaxAmountChanged?.Invoke(this, EventArgs.Empty);
	}

	public void DecreaseAmount(Object obj, int amount = 1){
		objectAmountDictionary[obj] -= amount;
	}

	public bool IsObjectAtMaxAmount(Object obj){
		return objectAmountDictionary[obj] == objectMaxAmountDictionary[obj];
	}
}
