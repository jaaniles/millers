using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject spawnPoint;
	public Canvas prepareOverlay;
	
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void EndDay()
	{
		Time.timeScale = 0f;
		prepareOverlay.gameObject.SetActive(true);
	}

	public void StartDay()
	{
		Time.timeScale = 1f;
		GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoint.transform.position;
		prepareOverlay.gameObject.SetActive(false);
	}
}
