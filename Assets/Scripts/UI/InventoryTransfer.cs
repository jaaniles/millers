using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTransfer : MonoBehaviour {

	public int INVENTORY_RANGE = 15;
	public void TransferItem()
	{
		Item item = GetComponent<InventorySlot>().item;
		Inventory sourceInventory = GetComponentInParent<Inventory>();
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
