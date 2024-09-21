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

    public bool canTouchSnap = false;
    float dragThreshould;

    public Button[] uiButton = new Button[4];

    public bool isTurning = false;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.height/ 20;
    }

    private void Update()
    {
        ConvertUI();
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canTouchSnap)
        {
            if (Mathf.Abs(eventData.position.y - eventData.pressPosition.y) > dragThreshould)
            {
                if (eventData.position.y < eventData.pressPosition.y) Previous();
                else Next();
            }
            else
            {
                MovePage();
            }
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
