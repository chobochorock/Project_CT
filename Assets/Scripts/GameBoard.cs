using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour, IPointerClickHandler
{
    public InGamePlayer player;
    public List<GameObject> handList;
    public UIManager uiManager;
    bool isFocused;
    Vector2 originCoord;
    Quaternion originRotation;
    RectTransform rectTransform;
    void Start()
    {
        isFocused = false;
        rectTransform = GetComponent<RectTransform>();
        originCoord = rectTransform.localPosition;
        originRotation = rectTransform.rotation;
    }
    void Update()
    {

    }
    public void BoardFocusing()
    {
        isFocused = !isFocused;
        if (isFocused)
        {
            rectTransform.localPosition = Vector2.zero;
            rectTransform.rotation = Quaternion.identity;
            rectTransform.localScale *= 2;
            // transform.SetSiblingIndex(4);
            transform.SetAsLastSibling();
        }
        else
        {
            rectTransform.localPosition = originCoord;
            rectTransform.rotation = originRotation;
            rectTransform.localScale /= 2;
            transform.SetAsFirstSibling();
        }
    }
    public void CardAdd(GameObject card)
    {
        card.transform.SetParent(transform);
        handList.Add(card);
        CardAdjusting();
    }
    public void CardRemove(GameObject card)
    {
        handList.Remove(card);
        Destroy(card);
        CardAdjusting();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BoardFocusing();
    }
    void CardAdjusting()
    {
        int i = 0;
        RectTransform rect;
        foreach (GameObject card in handList)
        {
            rect = card.GetComponent<RectTransform>();
            rect.localPosition = new Vector2(-1800 + i++ * 300, 0);
            rect.localRotation = Quaternion.identity;
        }
    }
}
