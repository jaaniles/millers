using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Facility : MonoBehaviour {
	public Image progressBar = null; 
	public delegate void OnProgressChanged();
	public OnProgressChanged onProgressChangedCallback;
	public Recipe recipe = null;
	public Inventory inputInventory;
	public Inventory outputInventory;
	public bool spitsThingsOut = false;
	private float processed = 0;
	void Start()
	{
		onProgressChangedCallback += UpdateProgressUI;
	}

	void Update()
	{
		if (recipe == null || recipe.CheckRequirements(inputInventory) == false)
			return;

		if (processed < recipe.processTimeRequired)
		{
			UpdateProgress(processed += Time.deltaTime);
			return;
		}

		if (spitsThingsOut == true)
			recipe.SpitOut(inputInventory, transform.position);
		else
			recipe.Complete(inputInventory, outputInventory);

		UpdateProgress(0);
	}
	void UpdateProgress(float _progress)
	{
		processed = _progress;
		
		if(onProgressChangedCallback != null)
			onProgressChangedCallback.Invoke();
	}

	void UpdateProgressUI()
	{
		if (progressBar == null)
		{
			return;
		}

		progressBar.fillAmount = processed / recipe.processTimeRequired;
	}
}
