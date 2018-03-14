using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NeedType {
	Hunger, Thirst, Energy
}
public class Need {
	public NeedType type;
	public float currentNeedAmount;
	public float maxNeedAmount;
	public bool depletesWithTime = false;

	public Need(int max, NeedType _type, bool depletes = false)
	{
		currentNeedAmount = max;
		maxNeedAmount = max;
		type = _type;
		depletesWithTime = depletes;
	}
}

public class Needs : MonoBehaviour {
	public List<Need> needs = new List<Need>();
	public delegate void OnNeedChanged();
	public OnNeedChanged onNeedChangedCallback;

	public Needs()
	{
		needs.Add(new Need(20, NeedType.Energy, true));
		needs.Add(new Need(50, NeedType.Hunger));
	}

	void Update()
	{
		for (int i = 0; i < needs.Count; i++)
			UpdateNeed(needs[i]);

		WatchEnergy();
	}

	public void SatisfyNeed(NeedType need, float amount)
	{
		Need needToSatisfy = needs.Find(n => n.type == need);
		needToSatisfy.currentNeedAmount = Mathf.Clamp(needToSatisfy.currentNeedAmount + amount, 0, needToSatisfy.maxNeedAmount);
	}

	public void WatchEnergy()
	{
		Need energy = needs.Find(n => n.type== NeedType.Energy);
		if (energy.currentNeedAmount < 1)
			GameManager.instance.EndDay();
	}		

	private void UpdateNeed(Need need)
	{
		if (need.depletesWithTime == false)
			return;

		need.currentNeedAmount -= Time.deltaTime;
		if (onNeedChangedCallback != null)
			onNeedChangedCallback.Invoke();
	}
}
