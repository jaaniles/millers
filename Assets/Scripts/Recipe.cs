using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
[Serializable]
public class Recipe : ScriptableObject {
	public string recipeName = "New recipe";

	[SerializeField]
	public List<InventoryItem> inputItems = new List<InventoryItem>();
	[SerializeField]
	public List<InventoryItem> outputItems = new List<InventoryItem>();
	public float processTimeRequired = 5;
	public void Complete(Inventory inventory, Inventory outputInventory)
	{
		foreach(InventoryItem item in inputItems)
			inventory.TryRemoveItem(item.item, item.quantity);
		

		foreach(InventoryItem item in outputItems)	
			outputInventory.TryAddItem(item.item, item.quantity);
	}

	public void SpitOut(Inventory inventory, Vector3 pos)
	{
		foreach(InventoryItem item in inputItems)
			inventory.TryRemoveItem(item.item, item.quantity);

		foreach(InventoryItem _item in outputItems)
		{
			for (int i = 0; i < _item.quantity; i++)
			{
				//GameObject item = Instantiate(_item.item.worldItem, pos + new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;

				//item.transform.rotation = UnityEngine.Random.rotation;
				//item.GetComponent<Rigidbody>().AddForce(item.transform.forward * 100);
			}
		}
	}

	public bool CheckRequirements(Inventory inventory)
	{
		for (int i = 0; i < inputItems.Count; i++)
		{
			InventoryItem reqItem = inputItems[i];

			if (inventory.ItemQuantityOverOrSame(reqItem.quantity, reqItem.item) == false)
				return false;
		}

		return true;
	}
}