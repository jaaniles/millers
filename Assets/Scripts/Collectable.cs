using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public Item item;
	public void Collect(GameObject collector)
	{
		Inventory inventory = collector.GetComponent<Inventory>();

		if (inventory.TryAddItem(item) == true)
		{
			Destroy(gameObject);
		}

	}
}
