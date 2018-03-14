using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

[Serializable]
public class Item : ScriptableObject {
	public Sprite icon = null;

	[SerializeField]
	public List<UseItem> useScripts = new List<UseItem>();

	public void UseItem(GameObject user, Item item, Inventory sourceInventory)
	{
		if (useScripts.Count < 1 || item == null)
			return;

		foreach(UseItem useScript in useScripts)
			useScript.Use(user, item);

		if (sourceInventory != null)
			sourceInventory.TryRemoveItem(item);
	}
}