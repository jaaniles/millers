using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler {
	public void OnDrop(PointerEventData eventData)
	{
		RectTransform invPanel = transform as RectTransform;
		Vector3 worldpoint;

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(invPanel, Input.mousePosition, eventData.pressEventCamera, out worldpoint))
		{
			Item droppedItem = eventData.pointerDrag.GetComponent<InventorySlot>().item;
			if (droppedItem == null)
			{
				Debug.Log("Dropped item == null");
				return;
			}

			Inventory sourceInventory = GetSourceInventory(eventData);
			Inventory inventory = GetCorrectInventory();

			if (inventory == null || sourceInventory == null)
			{
				Debug.Log("No inventory or source inventory");
				return;
			}

			if (inventory.TryAddItem(droppedItem))
			{
				sourceInventory.TryRemoveItem(droppedItem);
			} 
		}
	}

	public Inventory GetSourceInventory(PointerEventData source)
	{
		return source.pointerDrag.GetComponentInParent<Inventory>();
	}

	public Inventory GetCorrectInventory()
	{
		return GetComponentInParent<Inventory>();
	}
}
