using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFadeIn : MonoBehaviour 
{
	public float fadeTime { get; set; }
	private SpriteRenderer spriteRenderer;
	private Color currentColor;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		this.enabled = false;
	}

	void Update() 
	{
		currentColor.a += Mathf.Clamp01(Time.deltaTime / fadeTime);
		spriteRenderer.color = currentColor;

		if (currentColor.a >= 1f)
		{
			spriteRenderer.color = currentColor;
			this.enabled = false;
		}
	}

	// Start fade in
	void OnEnable()
	{
		currentColor = spriteRenderer.color;
		spriteRenderer.color = currentColor;
	}
}
