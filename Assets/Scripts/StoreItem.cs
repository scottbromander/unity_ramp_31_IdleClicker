using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ItemType {
	ClickPower, PerSecondIncrease
};

public class StoreItem : MonoBehaviour {
	[Tooltip("How much will this upgrade cost?")]
	public float cost;

	public ItemType itemType;

	[Tooltip("If purchased, how much will it increase this?")]
	public float increaseAmount;

	private int qty;

	public Text costText;
	public Text qtyText;

	private GameController controller;
	private Button button;

	// Use this for initialization
	void Start () {
		qty = 0;
		qtyText.text = qty.ToString ();
		costText.text = "$" + cost.ToString ();

		button = transform.GetComponent<Button> ();
		button.onClick.AddListener (this.ButtonClicked);

		controller = GameObject.FindObjectOfType<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Whether or not the upgrade is available (Does the player have enough money)
		button.interactable = (controller.Cash >= cost);
	}

	public void ButtonClicked(){
		controller.Cash -= cost;
		cost *= 1.05f;
		costText.text = "$" + cost.ToString ("F2");

		switch (itemType) {
		case ItemType.ClickPower:
			controller.cashPerClick += increaseAmount;
			break;
		case ItemType.PerSecondIncrease:
			controller.CashPerSecond += increaseAmount;
			break;
		}

		qty++;
		qtyText.text = qty.ToString ();
	}
}
