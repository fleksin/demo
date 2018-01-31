// Copyright (C) 2016-2017 David Pol. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

using TMPro;

using CCGKit;

public class CardListItem : MonoBehaviour
{
    public DeckButton deckButton;
    public Card card;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardCostText;
    public TextMeshProUGUI cardAmountText;

    public int count = 1;

    public void AddCard()
    {
        ++count;
        cardAmountText.text = "x" + count;
    }

    public void OnDeleteButtonPressed()
    {
        deckButton.deck.RemoveCards(card);
        deckButton.UpdateDeckInfo();
        Destroy(gameObject);
    }
}
