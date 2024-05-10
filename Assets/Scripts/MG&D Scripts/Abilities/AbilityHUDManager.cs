using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityHUDManager : MonoBehaviour
{
    public static AbilityHUDManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Transform _AbilitySlot1;
    [SerializeField]
    private Transform _AbilitySlot2;
    [SerializeField]
    private Transform _AbilitySlot3;
    [SerializeField]
    private Transform _AbilitySlot4;
    [SerializeField]
    private Transform _AbilitySlot5;
    [SerializeField]
    private TMP_Text _availableSkillPointsText;

    public void SetSkillPoints(int skillPoints)
    {
        _availableSkillPointsText.text = skillPoints.ToString();
        if (skillPoints > 0)
        {
            _availableSkillPointsText.transform.parent.gameObject.SetActive(true);
            SetSkillPoints(true);
        }
        else
        {
            SetSkillPoints(false);
            _availableSkillPointsText.transform.parent.gameObject.SetActive(false);
        }
    }

    public void SetSkillPoints(bool state)
    {
        _AbilitySlot1.GetComponent<AbilitySlot>().SetSkillPointBtn(state);
        _AbilitySlot2.GetComponent<AbilitySlot>().SetSkillPointBtn(state);
        _AbilitySlot3.GetComponent<AbilitySlot>().SetSkillPointBtn(state);
        _AbilitySlot4.GetComponent<AbilitySlot>().SetSkillPointBtn(state);
        _AbilitySlot5.GetComponent<AbilitySlot>().SetSkillPointBtn(state);
    }

    public void SetAbilitySlot(int abilitySlot, AbilitySO ability)
    {
        switch (abilitySlot)
        {
            case 1:
                _AbilitySlot1.GetComponent<AbilitySlot>().SetAbility(ability);
                break;
            case 2:
                _AbilitySlot2.GetComponent<AbilitySlot>().SetAbility(ability);
                break;
            case 3:
                _AbilitySlot3.GetComponent<AbilitySlot>().SetAbility(ability);
                break;
            case 4:
                _AbilitySlot4.GetComponent<AbilitySlot>().SetAbility(ability);
                break;
            case 5:
                _AbilitySlot5.GetComponent<AbilitySlot>().SetAbility(ability);
                break;
        }
    }

    public void SetAbilitySlotCooldown(int abilitySlot, float cooldown)
    {
        switch (abilitySlot)
        {
            case 1:
                _AbilitySlot1.GetComponent<AbilitySlot>().SetCooldown(cooldown);
                break;
            case 2:
                _AbilitySlot2.GetComponent<AbilitySlot>().SetCooldown(cooldown);
                break;
            case 3:
                _AbilitySlot3.GetComponent<AbilitySlot>().SetCooldown(cooldown);
                break;
            case 4:
                _AbilitySlot4.GetComponent<AbilitySlot>().SetCooldown(cooldown);
                break;
            case 5:
                _AbilitySlot5.GetComponent<AbilitySlot>().SetCooldown(cooldown);
                break;
        }
    }
}
