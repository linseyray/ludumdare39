using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HotspotController : MonoBehaviour 
{
	[Header("References")]
	public KeyboardInput keyboardInput;
	public GameObject[] rowObjects;

	[Header("Settings")]
	public bool repositionOnLoad = false;
	public float offsetBetweenHotspots = 1;

	public Dictionary<KeyCode, Hotspot> hotspots { get; private set;}
	public bool initialised { get; private set; }

	void Awake()
	{
	}

	void Start() 
	{
		LoadHotspots();
	}

	void Update() 
	{
	}

	public void LoadHotspots()
	{
		hotspots = new Dictionary<KeyCode, Hotspot>();

		LoadRow(rowObjects[0], 2);
		LoadRow(rowObjects[1], 1);
		LoadRow(rowObjects[2], 0);

		initialised = true;
	}

	private void LoadRow(GameObject row, float y)
	{
		Hotspot[] hotspots = row.GetComponentsInChildren<Hotspot>();

		for (int i = 0; i < hotspots.Length; i++)
		{
			Hotspot hotspot = hotspots[i];
			this.hotspots.Add(hotspot.keyCode, hotspot);

			if (repositionOnLoad)
			{
				hotspot.transform.position = new Vector2(i * offsetBetweenHotspots, y);
			}
		}
	}	
}
