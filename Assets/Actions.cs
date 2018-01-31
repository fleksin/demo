using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAction {
}

public class REARRANGE_BOTTOM : IAction{

	public Action complete;

	public REARRANGE_BOTTOM(){
		complete = null;
	}

	public REARRANGE_BOTTOM(Action comp){
		complete = comp;
	}
}

public class PLAY_CREATURE : IAction{
	public PLAY_CREATURE(){
	}

	public PLAY_CREATURE(CardView card){
		
	}
}
