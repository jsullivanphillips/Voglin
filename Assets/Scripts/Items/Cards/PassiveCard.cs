using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PassiveCard : DraggableCard
{
    [SerializeField]
    TMP_Text _TitleText;
    [SerializeField]
    TMP_Text _EffectType_1_Text;
    [SerializeField]
    TMP_Text _EffectValue_1_Text;
    [SerializeField]
    TMP_Text _EffectType_2_Text;
    [SerializeField]
    TMP_Text _EffectValue_2_Text;
    [SerializeField]
    TMP_Text _EffectType_3_Text;
    [SerializeField]
    TMP_Text _EffectValue_3_Text;

    public PassiveItemSO passiveItemSO;

    public bool isInPassiveItemZone = false;

    public void SetPassiveItemSO(PassiveItemSO passiveItemSO)
    {
        this.passiveItemSO = passiveItemSO;
        UpdateCardDisplay();
    }

    public void SetIsDraggable(bool isDraggable)
    {
        this.isDraggable = isDraggable;
    }

    private void UpdateCardDisplay()
    {
        _TitleText.text = passiveItemSO.itemName;
        for (int i = 0; i < passiveItemSO.passiveEffects.Count; i++)
        {
            switch (i)
            {
                case 0:
                    _EffectType_1_Text.text = passiveItemSO.passiveEffects[i].ToString();
                    _EffectValue_1_Text.text = passiveItemSO.effectValues[i].ToString();
                    break;
                case 1:
                    _EffectType_2_Text.text = passiveItemSO.passiveEffects[i].ToString();
                    _EffectValue_2_Text.text = passiveItemSO.effectValues[i].ToString();
                    break;
                case 2:
                    _EffectType_3_Text.text = passiveItemSO.passiveEffects[i].ToString();
                    _EffectValue_3_Text.text = passiveItemSO.effectValues[i].ToString();
                    break;
            }
        }

        if(passiveItemSO.passiveEffects.Count > 3)
        {
            Debug.LogError("Too many passive effects on " + passiveItemSO.itemName);
        }
    }

    public PassiveItemSO GetPassiveItemSO()
    {
        return passiveItemSO;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }

        base.OnBeginDrag(eventData);

        if(isInPassiveItemZone)
        {
            HUDPassiveItemZone.Instance.RemovePassiveItem(this.GetId());
            PlayerPassiveItems.Instance.RemovePassiveItem(this.GetId());
        }
        isInPassiveItemZone = false;
    }

    public void EquipCard()
    {
        PassiveItemZone.Instance.EquipCard(this);
    }


}
