using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventorySlotClick : MonoBehaviour, IPointerClickHandler {
	public int INVENTORY_RANGE = 15;
	public bool isPlayerHUDSlot = false;
	Inventory sourceInventory;

	void Start()
	{
		if (isPlayerHUDSlot == true)
			sourceInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		else 
			sourceInventory = GetComponentInParent<Inventory>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
			TransferItem();
		else if (eventData.button == PointerEventData.InputButton.Right)
			Consume();
	}

	public void Consume()
	{
		Item item = GetComponent<InventorySlot>().item;

		GameObject player = GameObject.FindWithTag("Player");
		if (item == null)
			return;

		item.UseItem(player, item, sourceInventory);
	}

	public void TransferItem()
	{
		Item item = GetComponent<InventorySlot>().item;
		GameObject player = GameObject.FindWithTag("Player");

		if (item == null || sourceInventory == null)
			return;
		
        Collider[] inventoriesInRange = Physics.OverlapSphere(player.GetComponent<Collider>().bounds.center, INVENTORY_RANGE, LayerMask.GetMask("Inventory"));
        Transform closestInventory = Utilities.GetClosest(inventoriesInRange, player.transform.position);

		if (closestInventory == null)
			return;

		Inventory targetInventory;
		// If moving items from other than playerinventory, fastclick target is always players inventory
		if (sourceInventory.isPlayerInventory == false)
			targetInventory = player.GetComponent<Inventory>();
		else
			targetInventory = closestInventory.GetComponent<Inventory>();

		sourceInventory.TryRemoveItem(item);
		targetInventory.TryAddItem(item);
	}
}
