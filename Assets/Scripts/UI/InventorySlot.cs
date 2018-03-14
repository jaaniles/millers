using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {
	public Item item;
	public Item specificItem;
	public Image icon;
	public Button removeButton;
	public TextMeshProUGUI amount;
	public bool draggable = false;
	Inventory inventory;

	void Start()
	{
		inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
	}

	public void RenderItem(InventoryItem _item)
	{
		item = _item.item;

		icon.sprite = item.icon;
		icon.enabled = true;
		amount.enabled = true;
		amount.text = _item.quantity.ToString();
		draggable = true;

//		removeButton.interactable = true;
	}

	public void ClearSlot()
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		amount.text = "0";
		draggable = false;

//		removeButton.interactable = false;
	}

	public void OnRemoveButton()
	{
		Debug.Log("On remove button");
		inventory.TryRemoveItem(item);
	}

	public void SetSpecificItem(Item _item)
	{
		specificItem = _item;
		icon.sprite = _item.icon;
		icon.enabled = true;
		amount.enabled = true;
		amount.text = "0";
	}

	public void Reset()
	{
		item = null;
		draggable = false;
		amount.text = "0";
	}
}
