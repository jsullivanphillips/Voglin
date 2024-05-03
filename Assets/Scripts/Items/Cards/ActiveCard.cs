using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveCard : DraggableCard
{
    [SerializeField]
    TMP_Text _TitleText;
    [SerializeField]
    TMP_Text _DamageText;
    [SerializeField]
    TMP_Text _CooldownText;
    [SerializeField]
    TMP_Text _RangeText;
    [SerializeField]
    public Image _CooldownFill;

    public ActiveItemSO activeItemSO;

    public bool isInActiveItemsZone = false;

    public void SetActiveItemSO(ActiveItemSO activeItemSO)
    {
        this.activeItemSO = activeItemSO;
        UpdateCardDisplay();
    }

    public void SetCooldownPercentage(float cooldownPercentage)
    {
        _CooldownFill.fillAmount = cooldownPercentage;
    }

    public void SetIsDraggable(bool isDraggable)
    {
        this.isDraggable = isDraggable;
    }

    private void UpdateCardDisplay()
    {
        _TitleText.text = activeItemSO.itemName;
        _DamageText.text = "Damage: " + activeItemSO.damage;
        _CooldownText.text = "Cooldown: " + activeItemSO.cooldown;
        _RangeText.text = "Range: " + activeItemSO.attackRange;
    }

    public ActiveItemSO GetActiveItemSO()
    {
        return activeItemSO;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }
        
        base.OnBeginDrag(eventData);

        // if this card was in activeItems, remove it
        if(isInActiveItemsZone)
        {
            HUDActiveItemZone.Instance.RemoveActiveItem(this.GetId());
            PlayerActiveItems.Instance.RemoveActiveItem(this.GetId());
        }
        isInActiveItemsZone = false;
    }

    public void EquipCard()
    {
        ActiveItemZone.Instance.EquipCard(this);
    }
}
