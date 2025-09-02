using System.Collections.Generic;
using UnityEngine;

public class InGamePlayer : MonoBehaviour
{
    public bool isReady, isYou;
    public float time;
    public List<GameCard> hand; // trump card
    public GameObject PlayerBoard;
    GameBoard gameBoard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hand = new List<GameCard>();
        gameBoard = PlayerBoard.GetComponent<GameBoard>();
    }

    public void ReadyButton() { isReady = !isReady; }
    public void GettingCard(GameCard card)
    {
        hand.Add(card);
        if (isYou) card.HandChecking();
        gameBoard.CardAdd(card.gameObject);
    }
    public void TurnOnPlayerTimer(bool isOn) { }
    public void PregameDiscard(GameCard card1, GameCard card2)
    {
        hand.Remove(card1);
        hand.Remove(card2);
        gameBoard.CardRemove(card1.gameObject);
        gameBoard.CardRemove(card2.gameObject);
    }
    public void InGameDiscard(GameCard card)
    {
        hand.Add(card);
    }
    public void TakingCards() { }
    public void Doubting() { }
    public void DeclaringImpossible() { }
    public void Passing() { }
}