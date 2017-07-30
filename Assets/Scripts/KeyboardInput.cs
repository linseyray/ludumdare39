using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour 
{
	// Use the letter rows (3 total)
	// Row 1: 10 
	// Row 2: 9
	// Row 3: 7

	public int nrRows = 3;
	public int nrKeysInRowOne = 10;
	public int nrKeysInRowTwo = 9;
	public int nrKeysInRowThree = 7;

	private HotspotController hotspotController;

	void Awake()
	{
		hotspotController = FindObjectOfType<HotspotController>();
	}

	void Start() 
	{
		// Initialise keycodes (visually!!!)
	}
	
	void Update() 
	{
		if (!hotspotController.initialised)
			return;

		Dictionary<KeyCode, Hotspot> hotspotDict = hotspotController.hotspots;

		foreach (Hotspot hotspot in hotspotDict.Values)
		{
			if (Input.GetKeyDown(hotspot.keyCode))
				hotspot.OnKeyDown();

			if (Input.GetKeyUp(hotspot.keyCode))
				hotspot.OnKeyUp();
		}

	}
}
