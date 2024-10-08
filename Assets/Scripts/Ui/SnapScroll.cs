using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SnapScroll : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    [SerializeField] int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    [SerializeField] Image BG_Image;
    [SerializeField] Sprite[] BG_Sprite = new Sprite[3];

    float dragThreshould;

    public Button[] uiButton;

    public bool isTurning = false;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.width/ 15;
    }

    private void Update()
    {
        ConvertUI();
        UpdateImage();
    }
    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }

    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }

    }

    private void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        isTurning = true;
        UpdateImage();
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();

            if (eventData.position.x < eventData.pressPosition.x) Next();
        }
        else
        {
            MovePage();
        }
    }

    private void UpdateImage()
    {
        foreach (var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;

        if (currentPage % 4 == 1)
        {
            BG_Image.sprite = BG_Sprite[0];
        }
        if (currentPage % 4 == 2)
        {
            BG_Image.sprite = BG_Sprite[1];
        }
        if (currentPage % 4 == 3)
        {
            BG_Image.sprite = BG_Sprite[2];
        }
        if (currentPage % 4 == 0)
        {
            BG_Image.sprite = BG_Sprite[3];
        }
    }

    private void ConvertUI()
    {
        for (int i = 0; i < uiButton.Length; i++)
        {
            if (isTurning && currentPage == i + 1)
            {
                ColorBlock cb = uiButton[i].colors;
                cb.normalColor = new Color(255, 0, 0, 255);
                uiButton[i].colors = cb;

                if (i < uiButton.Length - 1)
                {
                    cb = uiButton[i + 1].colors;
                    cb.normalColor = Color.white;
                    uiButton[i + 1].colors = cb;
                }
                if (i > 0)
                {
                    cb = uiButton[i - 1].colors;
                    cb.normalColor = Color.white;
                    uiButton[i - 1].colors = cb;
                }

                isTurning = false;
            }

        }
    }
}
