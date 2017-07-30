using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour 
{
	// -- State configurations --
	[Header("Runtime State Switch")]
	public ParasiteState newState;
	[InspectorButtonAttribute("OnGoToStateButtonPressed")]
	public bool goToState;

	[Header("Low Intensity State")]
	public StateSettings lowIntensitySettings;

	[Header("Mid Intensity State")]
	public StateSettings midIntensitySettings;

	[Header("High Intensity State")]
	public StateSettings highIntensitySettings;

	[Header("High Intensity State")]
	public StateSettings retreatSettings;

	// -- Variables --
	private ParasiteState currentState;
	private StateSettings currentStateSettings;
	private ParasiteCell[] cells;

	// -- Data types --
	public enum ParasiteState { LOW_INTENSITY, MID_INTENSITY, HIGH_INTENSITY, RETREATING };

	[System.Serializable]
	public class StateSettings
	{
		public float repressionTolerance;
		public float test;
	}



	void Awake()
	{
	}

	void Start() 
	{
		cells = FindObjectsOfType<ParasiteCell>();
		GoToState(ParasiteState.LOW_INTENSITY);
	}
	
	void Update() 
	{
		// -- Get Total time cells have been repressed --
		float totalTimeRepressed = CalculateTotalCellRepressionTime();

		// -- If time > certain value, go to next phase? -- 
		if (totalTimeRepressed >= currentStateSettings.repressionTolerance)
		{
			ResetCellRepressionTime();
			GoToState(GetNextState(currentState));
		}
	}

	public void OnGoToStateButtonPressed()
	{
		GoToState(newState);
	}

	private void GoToState(ParasiteState newState)
	{
		if (newState == currentState)
			return;

		ResetCellRepressionTime();

		if (newState == ParasiteState.LOW_INTENSITY)
		{
			currentStateSettings = lowIntensitySettings;
		}

		if (newState == ParasiteState.MID_INTENSITY)
		{
			currentStateSettings = midIntensitySettings;
		}

		if (newState == ParasiteState.HIGH_INTENSITY)
		{
			currentStateSettings = highIntensitySettings;
		}

		if (newState == ParasiteState.RETREATING)
		{
			currentStateSettings = retreatSettings;
		}

		currentState = newState;
	}

	private ParasiteState GetNextState(ParasiteState state)
	{
		if (state == ParasiteState.LOW_INTENSITY)
			return ParasiteState.MID_INTENSITY;
		if (state == ParasiteState.MID_INTENSITY)
			return ParasiteState.HIGH_INTENSITY;
		else
			return ParasiteState.RETREATING;
	}

	private void ResetCellRepressionTime()
	{
		for (int i = 0; i < cells.Length; i++)
		{
			ParasiteCell cell = cells[i];
			cell.ResetRepressionTime();
		}
	}


	private float CalculateTotalCellRepressionTime()
	{
		float totalTimeRepressed = 0f;

		for (int i = 0; i < cells.Length; i++)
		{
			ParasiteCell cell = cells[i];
			totalTimeRepressed += cell.GetTimeRepressed();
		}

		return totalTimeRepressed;
	}
}
