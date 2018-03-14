using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedSlot : MonoBehaviour {
	public Image needSlot;
	public Need need;

	void Start()
	{
		switch(need.type) {
			case NeedType.Hunger:
				needSlot.color = new Color32(60, 61, 182, 255);
				break;
			case NeedType.Thirst:
				needSlot.color = new Color32(185, 60, 60, 255);
				break;
			default:
				break;
		}
	}

	void Update()
	{
		SetFill();
	}

	public NeedSlot(Need _need)
	{
		need = _need;
	}

	public void SetFill()
	{
		needSlot.fillAmount = need.currentNeedAmount / need.maxNeedAmount;
	}
}
