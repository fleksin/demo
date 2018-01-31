using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	public static Board board = null;

	// Use this for initialization
	void Start () {
	}

	public void InitializeBoard (){
		board = GameObject.Find("Battlefield").GetComponent<Board>();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public static void Dispatch (IAction action) {
		if (board == null) {
			Debug.Log ("Board is not initialized");
			return;
		}
			
		if (action is REARRANGE_BOTTOM) {
			Debug.Log ("rearrange bottom");
			REARRANGE_BOTTOM rearrangeBottom = action as REARRANGE_BOTTOM;
			board.RearrangeBottomBoard (rearrangeBottom.complete);
		}
			
	}

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}
}

