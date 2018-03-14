using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseItem : ScriptableObject {
	public abstract void Use(GameObject user, Item item);
}


[CreateAssetMenu (menuName = "UseItemScripts/Edible")]
public class Edible : UseItem {
    
	public int foodAmount = 10;
	public override void Use(GameObject user, Item item)
    {
		Debug.Log("Eating and removing " + foodAmount + " hunger");

		Needs needs = user.GetComponent<Needs>();
		
		needs.SatisfyNeed(NeedType.Hunger, foodAmount);
    }
}

[CreateAssetMenu (menuName = "UseItemScripts/DamageOnUse")]
public class DamageOnUse : UseItem {
    
	public int damage = 5;
	public override void Use(GameObject user, Item item)
    {
		Debug.Log("Taking damage: " + damage);
    }
}


[CreateAssetMenu (menuName = "UseItemScripts/Energy")]
public class Energy : UseItem {
    
	public int energy = 5;
	public override void Use(GameObject user, Item item)
	{
		Needs needs = user.GetComponent<Needs>();
		needs.SatisfyNeed(NeedType.Energy, energy);
	}
}