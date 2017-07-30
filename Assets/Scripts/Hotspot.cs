using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotspot : MonoBehaviour 
{
	public KeyCode keyCode;

	[Header("General Settings")]
	public bool useColour = false;
	public bool useLights = false;

	public Material defaultSprite;
	public Material diffuseSprite;

	[Header("Colour")]
	//public Color inactiveColor;
	public Color activeColor;

	private SpriteRenderer spriteRenderer;
	private SpriteFadeIn spriteFadeIn;
	private SpriteFadeOut spriteFadeOut;

	[Header("Light")]
	public float maxLightIntensity = 5f;
	public float minLightIntensity = 0f;

	private Light light;
	private LightFadeIn lightFadeIn;
	private LightFadeOut lightFadeOut;

	[Header("Shake")]
	public float minShakeStrength = 0.085f;
	public float maxShakeStrength = 0.2f;

	[Header("Interaction")]
	public float lingerTime = 0.25f;
	public float activationTime = 0.25f;

	// -- General --
	private int contaminationLevel = 100;
	private ParasiteCell parasiteCell;

	// -- Particle System --

	void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		light = GetComponentInChildren<Light>();

		SetupEffects();
		SetupParasite();

		if (gameObject.name != "Hotspot") // the prefab one I use to test stuff
			keyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), gameObject.name);
	}

	private void SetupEffects()
	{
		if (useColour)
		{
			spriteFadeIn = GetComponentInChildren<SpriteFadeIn>();
			spriteFadeOut = GetComponentInChildren<SpriteFadeOut>();

			activeColor.a = 0f;

			spriteRenderer.color = activeColor;
			spriteRenderer.material = defaultSprite;
			spriteFadeIn.fadeTime = activationTime;
			spriteFadeOut.fadeTime = lingerTime;
		}

		if (useLights)
		{
			lightFadeIn = GetComponentInChildren<LightFadeIn>();
			lightFadeOut = GetComponentInChildren<LightFadeOut>();

			light.enabled = true;
			light.intensity = 0f;

			spriteRenderer.material = diffuseSprite;
			lightFadeIn.fadeTime = activationTime;
			lightFadeOut.fadeTime = lingerTime;
			lightFadeIn.minIntensity = minLightIntensity;
			lightFadeIn.maxIntensity = maxLightIntensity;
			lightFadeOut.minIntensity = minLightIntensity;
			lightFadeOut.maxIntensity = maxLightIntensity;

			//spriteRenderer.enabled = false;
		}
		else
		{
			light.enabled = false;
		}
	}

	private void SetupParasite()
	{
		parasiteCell = GetComponentInChildren<ParasiteCell>();
	}

	void Start() 
	{
	}
	
	void Update() 
	{
	}

	public void OnKeyDown()
	{
		// -- Start effects --
		if (useColour)
		{
			spriteFadeOut.enabled = false;
			spriteFadeIn.enabled = true;
		}
		if (useLights)
		{
			lightFadeOut.enabled = false;
			lightFadeIn.enabled = true;
		}

		// -- Signal to the parasite --
		if (parasiteCell && parasiteCell.enabled)
			parasiteCell.OnKeyDown();
	}

	public void OnKeyUp()
	{
		// -- Stop effects --
		if (useColour)
		{
			spriteFadeIn.enabled = false;
			spriteFadeOut.enabled = true;
		}
		if (useLights)
		{
			lightFadeIn.enabled = false;
			lightFadeOut.enabled = true;
		}

		// -- Signal to the parasite --
		if (parasiteCell && parasiteCell.enabled)
			parasiteCell.OnKeyUp();
	}
}
