using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour {

	public GameObject harvestItem;

	public void Collect()
	{
		int harvestAmount = Random.Range(3, 6);
		for (int i = 0; i < harvestAmount; i++)
		{
			CreateHarvest();
		}

		Destroy(gameObject);
	}

	private void CreateHarvest()
	{
		GameObject item = Instantiate(harvestItem, gameObject.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;
        item.transform.rotation = Random.rotation;
        item.GetComponent<Rigidbody>().AddForce(item.transform.forward * 100);
	}
}
