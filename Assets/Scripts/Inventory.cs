using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class InventoryItem
{
	public InventoryItem(Item _item, int _quantity)
	 {
		 item = _item;
		 quantity = _quantity;
	 }
	public Item item;
	public int quantity;
}

public class Inventory : MonoBehaviour {
	[SerializeField]
	public List<InventoryItem> items = new List<InventoryItem>();
	[SerializeField]
	public List<Item> acceptedItems = new List<Item>();
	public int space = 10;
	public bool isPlayerInventory = false;

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;


	public bool TryAddItem(Item item, int amount = 1)
	{
		bool isSuitableItem = ItemSuitableForInventory(item);
		if (isSuitableItem == false)
		{
			return false;
		}

		bool alreadyInInventory = ItemIsInInventory(item);
		if (alreadyInInventory)
			IncrementQuantity(item, amount);
		else 
			Add(item, amount);
	

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();

		return true;
	}

	public bool TryRemoveItem(Item item, int amount = 1)
	{
		if (ItemIsInInventory(item) == false)
		{
			Debug.LogWarning("Item not in inventory!");
			return false;
		}

		if (ItemQuantityOver(1, item))
			DecrementQuantity(item, amount);
		else 
			Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();

		return true;
	}

	private void Add(Item item, int amount = 1)
	{
		items.Add(new InventoryItem(item, amount));
	}

	private void Remove(Item item)
	{
		InventoryItem itemToRemove = items.Find(it => it.item == item);
		items.Remove(itemToRemove);
	}

	public bool ItemQuantityOver(int amount, Item item)
	{
		if (ItemIsInInventory(item) == false)
		{
			return false;
		}

		InventoryItem it = items.Find(i => i.item == item);
		return it.quantity > amount;
	}

	public bool ItemQuantityOverOrSame(int amount, Item item)
	{
		if (ItemIsInInventory(item) == false)
		{
			return false;
		}

		InventoryItem it = items.Find(i => i.item == item);

		return it.quantity >= amount;
	}

	public bool ItemIsInInventory(Item item)
	{
		InventoryItem it = items.Find(i => i.item == item);
		return it != null;
	}

	public void IncrementQuantity(Item item, int amount = 1)
	{
		InventoryItem it = items.Find(i => i.item == item);
		it.quantity += amount;
	}
	public void DecrementQuantity(Item item, int amount = 1)
	{
		InventoryItem it = items.Find(i => i.item == item);
		it.quantity -= amount;

		if (it.quantity < 1)
			Remove(it.item);	
	}

	public bool ItemSuitableForInventory(Item item)
	{
		if (acceptedItems.Count < 1) // No specified items for inventory
		{
			return true;
		}

		Item isInAcceptedList = acceptedItems.Find(i => i == item);
		return isInAcceptedList != null;
	}
}
