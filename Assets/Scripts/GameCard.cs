using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameCard : MonoBehaviour, IPointerClickHandler
{
    public bool isFaceUp, isClicked, isSelected;
    [SerializeField]
    char suit, num;
    public GameObject cardBack, cardFront;
    public InGamePlayer owner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardBack = transform.GetChild(0).gameObject;
        cardFront = transform.GetChild(1).gameObject;
        cardFront.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        isFaceUp = false;
    }

    public void HandChecking()
    {
        // this should be off when it is off my hand.
        cardFront.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
    }

    public void Selected()
    {
        Vector2 tmp = gameObject.GetComponent<RectTransform>().localPosition;
        gameObject.GetComponent<RectTransform>().localPosition = tmp + new Vector2(0, 50);
        if (owner.isYou) GameManager.gameManager.SelectingCard(this);
        else GameManager.gameManager.SendingCard(owner, this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("clicked");
        if (isClicked)
        {
            GameManager.gameManager.SelectingCard(this);
            isSelected = true;
        }
        else isClicked = true;
        // isFaceUp = !isFaceUp;
        // cardFront.GetComponent<Image>().color = new Color(1, 1, 1, isFaceUp ? 1 : 0);
    }
}
