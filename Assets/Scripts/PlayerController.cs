using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	float speed = 5f;
	private Rigidbody rig;
	private Camera mainCamera;
	public Inventory inventory;
	private InventoryUI closestInventory = null;
	public float strengthOfAttraction = 10f;
	public float pickUpDistance = 1.35f;
	public float collectableDistance = 6f;

	void Start() 
	{
		rig = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
	}

	void Update()
	{
		HandlePickup();
		
		if (Input.GetMouseButtonDown(0))
			Interact();

		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength;

		if (groundPlane.Raycast(cameraRay, out rayLength))
		{
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}

		HandleClosestInventory();
	}

	void HandleClosestInventory()
	{
		Collider[] inventoriesInRange = Physics.OverlapSphere(GetComponent<Collider>().bounds.center, 35, LayerMask.GetMask("Inventory"));
        Transform _closestInventory = Utilities.GetClosest(inventoriesInRange, transform.position);

		if (closestInventory != _closestInventory)
		{
			InventoryUI newUI = _closestInventory.GetComponent<InventoryUI>();

			if (newUI == null)
				return;

			if (closestInventory != null)
				closestInventory.Highlight();
				
			closestInventory = newUI;
			closestInventory.Highlight();
		}
	}

	void Interact()
	{
		Collider[] interactableThings = Physics.OverlapSphere(transform.position, 1.5f, LayerMask.GetMask("Interactable"));

		foreach(Collider interactable in interactableThings)
		{
			Vector3 directionToTarget = transform.position - interactable.transform.position;
			float angle = Vector3.Angle(transform.forward, directionToTarget);

			if (Mathf.Abs(angle) > 150 && Mathf.Abs(angle) < 180)
			{
				Harvestable harvest = interactable.GetComponent<Harvestable>();
				if (harvest == null)
				{
					return;
				}

				harvest.Collect();
			}
		}
	}

	void HandlePickup()
	{
		Collider[] pickableThings = Physics.OverlapSphere(transform.position, collectableDistance, LayerMask.GetMask("Collectable"));

		foreach(Collider pickable in pickableThings)
		{
			float distance = Vector3.Distance(transform.position, pickable.transform.position);

			if (distance <= pickUpDistance)
			{
				Collectable item = pickable.GetComponent<Collectable>();
				item.Collect(gameObject);

				/* 
				Item item = pickable.GetComponent<Item>();
				
				if (inventory.TryAddItem(item) == true)
				{
					pickable.GetComponent<Collectable>().Collect();
				}
				return;
				*/
			}

			Rigidbody rigid = pickable.GetComponent<Rigidbody>();
			if (rigid == null) 
				return;

			Vector3 pullDirection = transform.position - pickable.transform.position;
			rigid.AddForce(strengthOfAttraction * pullDirection);
		}
	}

	void FixedUpdate () {
		Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed;
		rig.velocity = movement;
	}
}
