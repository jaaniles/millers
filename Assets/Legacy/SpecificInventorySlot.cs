using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificInventorySlot : InventorySlot {
	public bool isSpecific = true;
	public void InitSpecificSlot(Item _item)
	{
		icon.sprite = _item.icon;
	}
}
