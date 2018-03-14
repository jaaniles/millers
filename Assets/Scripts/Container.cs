using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Container : MonoBehaviour {

	public List<Canvas> canvases;

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (canvases.Count < 1)
				canvases = GetComponentsInChildren<Canvas>().ToList();

			foreach(Canvas canvas in canvases)
				canvas.GetComponent<CanvasGroup>().alpha = 1;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (canvases.Count < 1)
				canvases = GetComponentsInChildren<Canvas>().ToList();

			foreach(Canvas canvas in canvases)
				canvas.GetComponent<CanvasGroup>().alpha = 0;
		}
	}
}
