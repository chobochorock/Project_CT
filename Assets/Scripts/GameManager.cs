using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isAllPlayerReady;
    public InGamePlayer turnPlayer;
    public InGamePlayer[] players; // 4 player
    public List<InGamePlayer> remainPlayers;
    public List<GameCard> decklist; // trump card
    public List<GameCard> selectedCards; // trump card
    public GameObject deck;
    public static GameManager gameManager;

    void Start()
    {
        StartCoroutine(InitGame());
    }

    IEnumerator InitGame()
    {
        /**
        Setting and ready for the game.
        */
        isAllPlayerReady = false;
        CreateDeck();
        //yield return StartCoroutine(WaitingAllPlayersReady());
        print("init game done");
        // yield return null;

        yield return StartCoroutine(PreGame());
    }

    #region Init Game
    void CreateDeck()
    {
        GameCard card;
        decklist = new List<GameCard>();
        for (int i = 0; i < 52; i++)
        {
            card = deck.transform.GetChild(i).GetComponent<GameCard>();
            decklist.Add(card);
        }
        print(decklist[1].name);
    }
    IEnumerator WaitingAllPlayersReady()
    {
        isAllPlayerReady = false;
        while (!isAllPlayerReady)
        {
            yield return null;
            isAllPlayerReady = true;
            foreach (InGamePlayer player in players) isAllPlayerReady &= player.isReady;
        }
    }
    #endregion

    IEnumerator PreGame()
    {
        /**
        1. distribute players with the same amount of cards.
        2. players can discard as many pairs of same number cards as they want.
        3. if finished, enter main game.
        */
        ShuffleDeck();
        yield return StartCoroutine(DistributingCards());
        // yield return StartCoroutine(FreeDiscardingTime());
        // print("pregame done");
        yield return null;

        // StartCoroutine(MainGame());
    }

    #region Pregame
    void ShuffleDeck()
    {
        int random1, random2;
        GameCard temp;

        for (int i = 0; i < decklist.Count; ++i)
        {
            random1 = Random.Range(0, decklist.Count);
            random2 = Random.Range(0, decklist.Count);

            temp = decklist[random1];
            decklist[random1] = decklist[random2];
            decklist[random2] = temp;
        }

    }
    IEnumerator DistributingCards()
    {
        int i = 0;
        foreach (GameCard card in decklist)
        {
            players[i++ % 4].GettingCard(card);
            yield return null;
        }
    }
    IEnumerator FreeDiscardingTime()
    {
        isAllPlayerReady = false;
        while (!isAllPlayerReady)
        {
            yield return null;
            isAllPlayerReady = true;
            foreach (InGamePlayer player in players) isAllPlayerReady &= player.isReady;
        }
    }
    #endregion

    IEnumerator MainGame()
    {
        /**
        1. turn player picks and takes a card from prev player's hand.
        2. turn player chooses to discard a pair of cards or declare impossible or just pass
        3. if the turn player declares to discard, next player can suspect it
        4. if the turn player declares impossible, the others can throw the card away
        5. repeat the above, until the last one who couldn't throw all cards away remains
        */
        ChooseFirstPlayer();
        while (remainPlayers.Count > 1)
        {
            yield return StartCoroutine(TurnPlayerTakingCard());
            yield return StartCoroutine(TurnPlayerChoosingActions());
            NextTurnPlayer();
        }
        yield return null;
    }
    #region Main Game
    void ChooseFirstPlayer() { turnPlayer = remainPlayers[Random.Range(0, 4)]; }
    IEnumerator TurnPlayerTakingCard()
    {
        while (true)
        {
            yield return null;
        }
    }
    IEnumerator TurnPlayerChoosingActions()
    {
        while (true)
        {
            yield return null;
        }
    }
    void NextTurnPlayer()
    {
        turnPlayer = remainPlayers[(remainPlayers.IndexOf(turnPlayer) + 1) % remainPlayers.Count];
    }
    #endregion
    void GameEnd()
    {
        /**
        check the results. and clean the game.
        */
    }

    #region Other Methods
    public void SendingCard(InGamePlayer from, GameCard card)
    {
        
    }

    public void DiscardingCard(InGamePlayer player, GameCard card)
    {

    }

    public void SelectingCard(GameCard card)
    {
        if (selectedCards.Count >= 2) selectedCards.Remove(selectedCards[0]);
        selectedCards.Add(card);
    }
    #endregion
}