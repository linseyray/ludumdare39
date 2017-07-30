using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour 
{
	public float shakeStrength { get; set; }

	void Start() 
	{
	}
	
	void Update() 
	{
	}

	void FixedUpdate()
	{
		Shake();
	}

	void OnDisable()
	{
		gameObject.transform.localPosition = Vector2.zero;
	}

	private void Shake()
	{
		Vector2 shakePosition = Random.insideUnitCircle * shakeStrength;
		gameObject.transform.localPosition = shakePosition;
	}

}
