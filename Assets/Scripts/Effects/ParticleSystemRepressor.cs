using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemRepressor : MonoBehaviour 
{
	public float fadeTime = 5;
	public float strengthOnSize;
	public float strengthOnLifetime;

	private ParticleSystem ps;
	private ParticleSystem.MainModule mainModule;

	// -- Start Size --
	private ParticleSystem.MinMaxCurve psStartSize;
	private float currentStartSize;
	private float defaultStartSize;

	// -- Start Lifetime --
	private ParticleSystem.MinMaxCurve psStartLifetime;
	private float currentStartLifetime;
	private float defaultStartLifetime;

	void Awake()
	{
		ps = GetComponent<ParticleSystem>();
		mainModule = ps.main;

		// -- Initialise start time --
		psStartSize = ps.main.startSize;
		defaultStartSize = GetStartSize();

		// -- Initialise start lifetime --
		psStartLifetime = mainModule.startLifetime;
		defaultStartLifetime = GetLifetime();
	}

	void Start() 
	{
		enabled = false;
	}
	
	void Update() 
	{
		// -- Reduce start size --
		currentStartSize -= Mathf.Clamp(Time.deltaTime / strengthOnSize, 0, defaultStartSize);
		SetStartSize(currentStartSize);

		// -- Reduce start lifetime --
		currentStartLifetime -= Mathf.Clamp(Time.deltaTime / (defaultStartLifetime * strengthOnLifetime), 0, defaultStartLifetime);
		SetStartLifetime(currentStartLifetime);

		if (currentStartSize <= 0f && currentStartLifetime <= 0f)
			this.enabled = false;

	}

	void OnEnable()
	{
		currentStartSize = GetStartSize();
	}

	void OnDisable()
	{
	}

	private float GetStartSize()
	{
		return psStartSize.constant;
	}

	private void SetStartSize(float size)
	{
		currentStartSize = size;
		psStartSize.constantMin = size;
		psStartSize.constantMax = size;
		mainModule.startSize = psStartSize;
		//Debug.Log("Start size: " + size);
	}

	private float GetLifetime()
	{
		return psStartSize.constant;
	}


	private void SetStartLifetime(float time)
	{
		currentStartLifetime = time;
		psStartLifetime.constantMin = time;
		psStartLifetime.constantMax = time;
		mainModule.startLifetime = psStartLifetime;
		//Debug.Log("Lifetime: " + psStartLifetime.constantMin);
	}
}
