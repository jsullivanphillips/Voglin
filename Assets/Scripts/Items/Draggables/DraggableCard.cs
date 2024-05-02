using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    Active,
    Passive
}
public class DraggableCard : DraggableObject
{
    protected int id;
    protected CardType cardType;

    private Coroutine fillBarAnimation;

    [SerializeField]
    private SimpleCardAnimations simpleCardAnimations;

    [SerializeField]
    private Image _CraftingFillBar;
    [SerializeField]
    private GameObject _FillBarGO;

    private DraggableCard craftingPartner;
    public bool isCrafting = false;

    public void SetId(int _id)
    {
        id = _id;
    }

    public int GetId()
    {
        return id;
    }

    public void SetCardType(CardType _cardType)
    {
        cardType = _cardType;
    }

    public CardType GetCardType()
    {
        return cardType;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }
        canvasGroup.blocksRaycasts = true;
        simpleCardAnimations.SetOriginalPosition(transform.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }
        base.OnDrag(eventData);
        simpleCardAnimations.StartCardBeingDraggedAnimation();
        StopCraftingAnimation();
    }

    #region Crafting
    public void SetCraftingFlagTrue(DraggableCard craftingPartner)
    {
        this.craftingPartner = craftingPartner;
        isCrafting = true;
    }

    

    public void StartCraftingAnimation()
    {
        isCrafting = true;
        _FillBarGO.SetActive(true);
        fillBarAnimation = StartCoroutine(FillCraftingBar());
    }

    private IEnumerator FillCraftingBar()
    {
        float fillAmount = 0;
        while(fillAmount < 1)
        {
            fillAmount += Time.deltaTime * 0.75f; // Adjust the fill speed here
            _CraftingFillBar.fillAmount = fillAmount;
            yield return null;
        }
        _FillBarGO.SetActive(false);
        isCrafting = false;
    }

    public void StopCraftingAnimation()
    {
        if(isCrafting && craftingPartner != null)
        {
            isCrafting = false;
            craftingPartner.StopCraftingAnimation();
            craftingPartner = null;
        }
        
        if(fillBarAnimation != null)
        {
            StopCoroutine(fillBarAnimation);
        }
        _FillBarGO.SetActive(false);
        _CraftingFillBar.fillAmount = 0;
        isCrafting = false;

        
    }

    #endregion
}
