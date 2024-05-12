using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;

public class AbilityCard : MonoBehaviour, IPointerClickHandler
{
    private AbilitySO _ability;
    private int _abilitySlot;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text nameText, subHeading, cooldown, description, uniquePassiveText;


    public void SetAbility(AbilitySO ability, int abilitySlot)
    {
        nameText.text = ability.name;
        _ability = ability;
        _abilitySlot = abilitySlot;
        ShowBasicDescription();
    }

    private void ShowBasicDescription()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = _ability.summary;
        if(_ability.IsType(AbilityType.Orbiter))
            cooldown.text = "Passive Ability";
        else
            cooldown.text = _ability.cooldown.ToString("F1") + " sec Cooldown";
        description.text = ParseAbilityDescription();
        uniquePassiveText.text = "";
    }

    private void ShowDetailedDescription()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = _ability.summary;
        if(_ability.IsType(AbilityType.Orbiter))
            cooldown.text = "Passive Ability";
        else
            cooldown.text = _ability.cooldown.ToString("F1") + " sec Cooldown";
        description.text = ParseAbilityDescriptionDetailed();
        uniquePassiveText.text = "";
    }

    
    private string ParseAbilityDescription()
    {
        string damageVar = $"<color=\"yellow\">{_ability.damage.ToString("F0")}</color> ";
        int i = 0;
        foreach(ScalingStats stat in _ability.scalingStats)
        {
            string statColour = PlayerItems.Instance.GetStatHexColor(stat.scalingStat);
            string statDamage = (stat.scaling * PlayerItems.Instance.GetScalingStat(stat.scalingStat)).ToString("F0");
            damageVar += $"{statColour}(+{statDamage})</color>";

            if(i != _ability.scalingStats.Count - 1)
            {
                damageVar += " ";
            }
            i++;
        }

        float cooldown = _ability.cooldown; // Plus items
        string parsedDescription = _ability.description.Replace("<damage>", damageVar);
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldown.ToString("F1"));
        parsedDescription = parsedDescription.Replace("<attackRange>", _ability.attackRange.ToString("F0"));
        return parsedDescription;
    }

    private string ParseAbilityDescriptionDetailed()
    {
        string damageVar = $"<color=\"yellow\">{_ability.damage.ToString("F0")}</color> ";
        int i = 0;
        foreach(ScalingStats stat in _ability.scalingStats)
        {
            string statColour = PlayerItems.Instance.GetStatHexColor(stat.scalingStat);
            string scalingStatValue = PlayerItems.Instance.GetScalingStat(stat.scalingStat).ToString("F0");
            string scalingValue = (stat.scaling * 100).ToString("F0");
            string statName = BreakEnumToString(stat.scalingStat);
            damageVar += $"{statColour}(+{scalingValue}% of {scalingStatValue} {statName})</color>";

            if(i != _ability.scalingStats.Count - 1)
            {
                damageVar += " ";
            }
            i++;
        }

        float cooldown = _ability.cooldown; // Plus items
        string parsedDescription = _ability.description.Replace("<damage>", damageVar);
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldown.ToString("F1"));
        parsedDescription = parsedDescription.Replace("<attackRange>", _ability.attackRange.ToString("F0"));
        return parsedDescription;
    }

    private string BreakEnumToString(ScalingStat stat)
    {
        string statString = stat.ToString();
        string finalString = Regex.Replace(statString, "(?<!^)([A-Z])", " $1");
        return finalString;
    }

    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                ShowDetailedDescription();
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                ShowBasicDescription();
            }
        }
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
