using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName="Inventory/Key Item")]
public class InventoryItem : ScriptableObject {

	[Tooltip("The name of the item show when mouse-hovered in-game.")]
	public string itemName;

	[Tooltip("The description of the item to be show in-game on an inspect window.")]
	public string itemDescription;

	[Tooltip("The image to use on the inventory slot UI in-game.")]
	public Sprite itemIcon;

	[Tooltip("How many uses the item can successfully have before it is used.")]
	public int itemCharge;
}
