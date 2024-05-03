using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class DisplayCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [SerializeField]
    TMP_Text _TitleText;
    // Active Card
    [SerializeField]
    GameObject _ActiveCardStats;
    [SerializeField]
    TMP_Text _DamageText;
    [SerializeField]
    TMP_Text _CooldownText;
    [SerializeField]
    TMP_Text _RangeText;
    // Passive Card
    [SerializeField]
    GameObject _PassiveCardStats;
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
    public ActiveItemSO activeItemSO;

    public void SetItemSO(ItemSO item)
    {
        if (item is ActiveItemSO)
        {
            activeItemSO = item as ActiveItemSO;
            _ActiveCardStats.SetActive(true);
            _PassiveCardStats.SetActive(false);
            UpdateActiveCardDisplay();
        }
        else if (item is PassiveItemSO)
        {
            passiveItemSO = item as PassiveItemSO;
            _ActiveCardStats.SetActive(false);
            _PassiveCardStats.SetActive(true);
            UpdatePassiveCardDisplay();
        }
    }

    private void UpdateActiveCardDisplay()
    {
        _TitleText.text = activeItemSO.itemName;
        _DamageText.text = "Damage: " + activeItemSO.damage.ToString("F2");
        _CooldownText.text = "Cooldown: " + activeItemSO.cooldown.ToString("F2");
        _RangeText.text = "Range: " + activeItemSO.attackRange.ToString("F2");
    }

    private void UpdatePassiveCardDisplay()
    {
        if(passiveItemSO.passiveEffects.Count > 3)
        {
            Debug.LogError("Too many passive effects for card: " + passiveItemSO.itemName);
            return;
        }

        _TitleText.text = passiveItemSO.itemName;
        for (int i = 0; i < passiveItemSO.passiveEffects.Count; i++)
        {
            switch (i)
            {
                case 0:
                    _EffectType_1_Text.text = passiveItemSO.passiveEffects[i].ToString();
                    _EffectValue_1_Text.text = passiveItemSO.effectValues[i].ToString("F2");
                    break;
                case 1:
                    _EffectType_2_Text.text = passiveItemSO.passiveEffects[i].ToString();
                    _EffectValue_2_Text.text = passiveItemSO.effectValues[i].ToString("F2");
                    break;
                case 2:
                    _EffectType_3_Text.text = passiveItemSO.passiveEffects[i].ToString();
                    _EffectValue_3_Text.text = passiveItemSO.effectValues[i].ToString("F2");
                    break;
            }
        }

        if(passiveItemSO.passiveEffects.Count < 3)
        {
            _EffectType_3_Text.text = "";
            _EffectValue_3_Text.text = "";
        }
        else if (passiveItemSO.passiveEffects.Count < 2)
        {
            _EffectType_2_Text.text = "";
            _EffectValue_2_Text.text = "";
        }

    }

    // Implement the IPointerEnterHandler interface
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("MouseEnter");
        // animation
    }

    // Implement the IPointerExitHandler interface
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("MouseExit");
        // cancel animation
    }

    // Implement the IPointerClickHandler interface
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MouseClick");
        InventoryViewManager.Instance.OpenInventory();
        if(activeItemSO != null)
        {
            CraftingTableItemManager.Instance.SpawnAndEquipActiveCard(activeItemSO);
        }
        else if (passiveItemSO != null)
        {
            CraftingTableItemManager.Instance.SpawnAndEquipPassiveCard(passiveItemSO);
        }
        ChooseNewCardManager.Instance.CloseDisplay();
    }

}
