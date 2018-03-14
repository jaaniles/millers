using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PrepareMenuHandlePlayerNeeds : MonoBehaviour {

	public Transform needsParent;
	Needs playerNeeds;
	List<NeedSlot> needSlots = new List<NeedSlot>();

	void Start()
	{
		playerNeeds = GameObject.FindGameObjectWithTag("Player").GetComponent<Needs>();

		foreach(Need need in playerNeeds.needs)
		{
			GameObject slot = (GameObject)Instantiate(Resources.Load("NeedSlot"));
			slot.transform.SetParent(needsParent, false);
			slot.GetComponent<NeedSlot>().need = need;
		}

		needSlots = needsParent.GetComponentsInChildren<NeedSlot>().ToList();
	}

	void UpdateUI()
	{
		for (int i = 0; i < needSlots.Count; i++)
			needSlots[i].GetComponent<NeedSlot>().SetFill();
	}
}

