using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFadeIn : MonoBehaviour 
{
	public float fadeTime { get; set; }
	public float minIntensity { get; set; }
	public float maxIntensity { get; set; }

	private Light light;
	private float currentIntensity;

	void Awake()
	{
		light = GetComponent<Light>();
	}

	void Start()
	{
		this.enabled = false;
	}

	void Update() 
	{
		currentIntensity += Mathf.Clamp(Time.deltaTime / fadeTime, minIntensity, maxIntensity);
		light.intensity = currentIntensity;

		if (currentIntensity >= maxIntensity)
		{
			this.enabled = false;
		}
	}

	// Start fade in
	void OnEnable()
	{
		currentIntensity = light.intensity;
	}
}
