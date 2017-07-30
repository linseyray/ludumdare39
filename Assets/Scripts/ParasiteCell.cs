using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteCell : MonoBehaviour 
{
	[Header("Shake Settings")]
	public bool shakeOnStart = true;
	public bool shakeOnRest = true;
	public float shakeStrengthOnRest = 0.02f;
	public float shakeStrengthOnRepress = 0.1f;

	[Header("Respawn Settings")]
	public bool respawn = true;
	public float timeTilRespawn = 2f;

	// -- Repression --
	private float timeRepressed = 0f;
	private float timeAtRepressionStart;

	// -- References --
	private ObjectShake objectShake;
	private ParticleSystemRepressor particleSystemRepressor;

	void Awake()
	{
		particleSystemRepressor = GetComponent<ParticleSystemRepressor>();
		objectShake = GetComponent<ObjectShake>();
	}

	void Start() 
	{
		objectShake.enabled = shakeOnStart;
		objectShake.shakeStrength = shakeStrengthOnRest;
	}
	
	void Update() 
	{
	}

	public void OnKeyDown()
	{
		particleSystemRepressor.enabled = true;
		objectShake.enabled = true;
		objectShake.shakeStrength = shakeStrengthOnRepress;

		timeAtRepressionStart = Time.time;
	}

	public void OnKeyUp()
	{
		particleSystemRepressor.enabled = false;
		objectShake.enabled = shakeOnRest;
		objectShake.shakeStrength = shakeStrengthOnRest;

		timeRepressed += Time.time - timeAtRepressionStart;
		Debug.Log(timeRepressed);
	}

	public float GetTimeRepressed()
	{
		return timeRepressed;
	}

	public void ResetRepressionTime()
	{
		timeRepressed = 0f;
	}
}
