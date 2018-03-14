using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler {

	public bool isPlayerDragHandler = false;
	public void OnDrag(PointerEventData eventData)
	{
		InventorySlot slot = GetComponent<InventorySlot>();

		if (slot != null && slot.item != null && slot.draggable == true)
		{
			GetComponent<CanvasGroup>().blocksRaycasts = false;

			if (isPlayerDragHandler == true)
			{
				transform.position = Input.mousePosition;
			} else {
				Camera cam = Camera.main;
				Vector3 screenPoint = Input.mousePosition;
				screenPoint.z = 5.0f;
				transform.position = cam.ScreenToWorldPoint(screenPoint);
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		transform.localPosition = Vector3.zero;
	}
}
