using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AbilityCard : MonoBehaviour, IPointerClickHandler
{
    private AbilitySO _ability;
    private int _abilitySlot;

    [SerializeField]
    TMP_Text title;


    public void SetAbility(AbilitySO ability, int abilitySlot)
    {
        title.text = ability.name;
        _ability = ability;
        _abilitySlot = abilitySlot;
    }

    public void ChooseAbility()
    {
        AbilityUpgradeManager.Instance.ChooseAbility(_ability, _abilitySlot);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChooseAbility();
    }
}
