using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsUI : MonoBehaviour {
	public Transform panel;
	Needs needs;
	void Start () {
		needs = GetComponentInParent<Needs>();
		if (needs == null)
			return;
		
		RenderNeedLevels();
	}

	void RenderNeedLevels()
	{
		int iterations = needs.needs.Count;
		for (int i = 0; i < iterations; i++)
		{
			GameObject needLevel = (GameObject)Instantiate(Resources.Load("NeedSlot"));
			needLevel.transform.SetParent(panel, false);

			NeedSlot needSlot = needLevel.GetComponent<NeedSlot>();
			needSlot.need = needs.needs[i];
		}
	}
}
