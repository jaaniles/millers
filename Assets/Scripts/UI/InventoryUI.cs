using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class InventoryUI : MonoBehaviour {
	public Transform itemsParent;
	public Image highlighter;
	public bool isPlayerInventory = false;
	public Inventory inventory = null;
	List<InventorySlot> slots = new List<InventorySlot>();
	void Start()
	{
		if (isPlayerInventory == true)
			inventory =	GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

		if (inventory == null)
			inventory = GetComponentInParent<Inventory>();

		if (inventory == null)
		{
			Debug.LogWarning("Failed to find inventory!!");
			return;
		}

		RenderSlots(inventory);

		inventory.onItemChangedCallback += UpdateUI;
		slots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList();

		UpdateUI();
	}

	void RenderSlots(Inventory inventory)
	{
		List<Item> acceptedItems = inventory.acceptedItems;

		if (acceptedItems.Count < 1) // Render basic slots
		{	
			int slotsAmount = inventory.space;
			for (int i = 0; i < slotsAmount; i++)
			{
				string slotPrefab = isPlayerInventory == true ? "HUDSlot" : "ItemUI";

				GameObject slot = (GameObject)Instantiate(Resources.Load(slotPrefab));
				slot.transform.SetParent(itemsParent, false);
			}
			return;
		}

		for (int i = 0; i < acceptedItems.Count; i++)
		{
			Item item = acceptedItems[i];

			GameObject slot = (GameObject)Instantiate(Resources.Load("ItemUI"));
			slot.GetComponentInChildren<InventorySlot>().SetSpecificItem(item);
			slot.transform.SetParent(itemsParent, false);
		}
	}

	void UpdateUI()
	{
		List<InventoryItem> itemsToRender = new List<InventoryItem>(inventory.items);
		List<InventorySlot> specificSlots = slots.FindAll(s => s.specificItem != null);
		List<InventorySlot> normalSlots = slots.FindAll(s => s.specificItem == null);

		// Specific slots, populate them first
		for (int i = 0; i < specificSlots.Count; i++)
		{
			// Find item to fill the slot with
			InventoryItem matchingItem = itemsToRender.Find(it => it.item == specificSlots[i].specificItem);
			if (matchingItem != null)
			{
				specificSlots[i].RenderItem(matchingItem);
				itemsToRender.Remove(matchingItem);	// Remove so we don't render twice		
			} else {
				specificSlots[i].Reset();
			}
		}

		for (int i = 0; i < normalSlots.Count; i++)
		{
			InventorySlot slot = normalSlots[i];
			if (i < itemsToRender.Count) {
				slot.RenderItem(itemsToRender[i]);
			} else {
				slot.ClearSlot();
			}
		}
	}

	public void Highlight()
	{
		if (highlighter == null)
			return;

		CanvasGroup cg = highlighter.GetComponent<CanvasGroup>();
		if (cg == null)
			return;

		int alpha = cg.alpha == 1 ? 0 : 1;

		cg.alpha = alpha;
	}
}
