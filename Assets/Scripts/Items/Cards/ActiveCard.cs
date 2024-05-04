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
    TMP_Text _SpecialEffect1Text;
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
        image.sprite = activeItemSO.itemSprite;
        _DamageText.text = "Damage: " + activeItemSO.damage.ToString("F2");
        _CooldownText.text = "Cooldown: " + activeItemSO.cooldown.ToString("F2");
        _RangeText.text = "Range: " + activeItemSO.attackRange.ToString("F2");
        if(activeItemSO.isPiercing)
        {
            _SpecialEffect1Text.text = "Piercing: " + activeItemSO.numberOfPierces;
        }
        else if(activeItemSO.isStutter)
        {
            _SpecialEffect1Text.text = "Stutter: " + activeItemSO.stutterDuration + "s";
        }
        else if(activeItemSO.passiveEffects.Count != 0)
        {
            _SpecialEffect1Text.text = activeItemSO.passiveEffects[0].ToString() + ": " + activeItemSO.effectValues[0].ToString();
        }
        else if(activeItemSO.isSplitOnCrit)
        {
            _SpecialEffect1Text.text = "Splits in " + activeItemSO.numberOfSplits + " on critical hit dealing 50% damage";
        }
        else
        {
            _SpecialEffect1Text.text = "";
        }
        
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
