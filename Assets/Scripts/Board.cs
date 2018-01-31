using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using UnityEngine.Rendering;

using DG.Tweening;
using TMPro;

using CCGKit;

public class Board : MonoBehaviour {

	public List<BoardCreature> opponentBoardCards;
	public List<BoardCreature> playerBoardCards;
	private GameObject playerBoard;
	private GameObject boardCreaturePrefab;
	private BoardCreature currentCreature ;
	protected List<CardView> playerHandCards = new List<CardView>();


	// Use this for initialization
	void Start () {
		GameObject.Find("StateManager").GetComponent<StateManager>().InitializeBoard ();
		playerBoard = GameObject.Find ("PlayerBoard");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RearrangeTopBoard(Action onComplete = null)
	{
		var boardWidth = 0.0f;
		var spacing = 0.5f;
		var cardWidth = 0.0f;
		foreach (var card in opponentBoardCards)
		{
			cardWidth = card.GetComponent<SpriteRenderer>().bounds.size.x;
			boardWidth += cardWidth;
			boardWidth += spacing;
		}
		boardWidth -= spacing;

		var newPositions = new List<Vector2>(opponentBoardCards.Count);
		var pivot = GameObject.Find("OpponentBoard").transform.position;
		for (var i = 0; i < opponentBoardCards.Count; i++)
		{
			var card = opponentBoardCards[i];
			newPositions.Add(new Vector2(pivot.x - boardWidth / 2 + cardWidth / 2, pivot.y));
			pivot.x += boardWidth / opponentBoardCards.Count;
		}

		var sequence = DOTween.Sequence();
		for (var i = 0; i < opponentBoardCards.Count; i++)
		{
			var card = opponentBoardCards[i];
			sequence.Insert(0, card.transform.DOMove(newPositions[i], 0.4f).SetEase(Ease.OutSine));
		}
		sequence.OnComplete(() =>
			{
				if (onComplete != null)
				{
					onComplete();
				}
			});
	}

	public void RearrangeBottomBoard(Action onComplete = null)
	{
		var boardWidth = 0.0f;
		var spacing = 0.5f;
		var cardWidth = 0.0f;
		foreach (var card in playerBoardCards)
		{
			cardWidth = card.GetComponent<SpriteRenderer>().bounds.size.x;
			boardWidth += cardWidth;
			boardWidth += spacing;
		}
		boardWidth -= spacing;

		var newPositions = new List<Vector2>(playerBoardCards.Count);
		var pivot = playerBoard.transform.position;
		for (var i = 0; i < playerBoardCards.Count; i++)
		{
			var card = playerBoardCards[i];
			newPositions.Add(new Vector2(pivot.x - boardWidth / 2 + cardWidth / 2, pivot.y));
			pivot.x += boardWidth / playerBoardCards.Count;
		}

		var sequence = DOTween.Sequence();
		for (var i = 0; i < playerBoardCards.Count; i++)
		{
			var card = playerBoardCards[i];
			sequence.Insert(0, card.transform.DOMove(newPositions[i], 0.4f).SetEase(Ease.OutSine));
		}
		sequence.OnComplete(() =>
		{
			if (onComplete != null)
			{
				onComplete();
			}
		});
	}


	public void CreatureAttack(BoardCreature initiator, BoardCreature receiver){
		
	}

	public void PopUp(){
	}

	public void PlaySpell(CardView card, BoardCreature target){
	}

//	public void DeployCreature(CardView card){
//		var boardCreature = Instantiate(boardCreaturePrefab);
//
//		var board = GameObject.Find("PlayerBoard");
//		boardCreature.tag = "PlayerOwned";
//		boardCreature.transform.parent = board.transform;
//		boardCreature.transform.position = new Vector2(1.9f * playerBoardCards.Count, 0);
//		boardCreature.GetComponent<BoardCreature>().ownerPlayer = this;
//		boardCreature.GetComponent<BoardCreature>().PopulateWithInfo(card.card);
//
//		playerHandCards.Remove(card);
//		RearrangeHand();
//		playerBoardCards.Add(boardCreature.GetComponent<BoardCreature>());
//
//		Destroy(card.gameObject);
//
//		currentCreature = boardCreature.GetComponent<BoardCreature>();
//
//		RearrangeBottomBoard (() =>
// {			//				StateManager.Dispatch (new REARRANGE_BOTTOM (()=>
//			var triggeredAbilities = libraryCard.abilities.FindAll (x => x is TriggeredAbility);
//			TriggeredAbility targetableAbility = null;
//			foreach (var ability in triggeredAbilities) {
//				var triggeredAbility = ability as TriggeredAbility;
//				var trigger = triggeredAbility.trigger as OnCardEnteredZoneTrigger;
//				if (trigger != null && trigger.zoneId == boardZone.zoneId && triggeredAbility.target is IUserTarget) {
//					targetableAbility = triggeredAbility;
//					break;
//				}
//			}
//
//			// Preemptively move the card so that the effect solver can properly check the availability of targets
//			// by also taking into account this card (that is trying to be played).
//			playerInfo.namedZones ["Hand"].RemoveCard (card.card);
//			playerInfo.namedZones ["Board"].AddCard (card.card);
//
//			if (targetableAbility != null && effectSolver.AreTargetsAvailable (targetableAbility.effect, card.card, targetableAbility.target)) {
//				var targetingArrow = Instantiate (spellTargetingArrowPrefab).GetComponent<SpellTargetingArrow> ();
//				boardCreature.GetComponent<BoardCreature> ().abilitiesTargetingArrow = targetingArrow;
//				targetingArrow.effectTarget = targetableAbility.target;
//				targetingArrow.targetType = targetableAbility.target.GetTarget ();
//				targetingArrow.onTargetSelected += () => {
//					PlayCreatureCard (card.card, targetingArrow.targetInfo);
//					effectSolver.MoveCard (netId, card.card, "Hand", "Board", targetingArrow.targetInfo);
//					currentCreature = null;
//					gameUI.endTurnButton.SetEnabled (true);
//				};
//				targetingArrow.Begin (boardCreature.transform.localPosition);
//			} else {
//				PlayCreatureCard (card.card);
//				effectSolver.MoveCard (netId, card.card, "Hand", "Board");
//				currentCreature = null;
//				gameUI.endTurnButton.SetEnabled (true);
//			}
//			boardCreature.GetComponent<BoardCreature> ().fightTargetingArrowPrefab = fightTargetingArrowPrefab;
//		});
//	}

	protected virtual void RearrangeHand()
	{
		var handWidth = 0.0f;
		var spacing = -1.0f;
		foreach (var card in playerHandCards)
		{
			handWidth += card.GetComponent<SpriteRenderer>().bounds.size.x;
			handWidth += spacing;
		}
		handWidth -= spacing;

		var pivot = Camera.main.ViewportToWorldPoint(new Vector3(0.55f, 0.08f, 0.0f));
		var totalTwist = -20f;
		if (playerHandCards.Count == 1)
		{
			totalTwist = 0;
		}
		var twistPerCard = totalTwist / playerHandCards.Count;
		float startTwist = -1f * (totalTwist / 2);
		var scalingFactor = 0.06f;
		for (var i = 0; i < playerHandCards.Count; i++)
		{
			var card = playerHandCards[i];
			var twist = startTwist + (i * twistPerCard);
			var nudge = Mathf.Abs(twist);
			nudge *= scalingFactor;
			card.transform.position = new Vector2(pivot.x - handWidth / 2, pivot.y - nudge);
			card.transform.rotation = Quaternion.Euler(0, 0, twist);
			pivot.x += handWidth / playerHandCards.Count;
			card.GetComponent<SortingGroup>().sortingLayerName = "HandCards";
			card.GetComponent<SortingGroup>().sortingOrder = i;
		}
	}

	public void test(){
		Debug.Log ("test");
	}
}
