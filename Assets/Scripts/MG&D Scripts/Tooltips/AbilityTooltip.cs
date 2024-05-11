using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class AbilityTooltip : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text nameText, subHeading, cooldown, description, uniquePassiveText;

    private AbilitySO _ability;

    public void SetAbility(AbilitySO ability)
    {
        _ability = ability;
        ShowBasicTooltip();
    }

    private string BreakEnumToString(ScalingStat stat)
    {
        string statString = stat.ToString();
        string finalString = Regex.Replace(statString, "(?<!^)([A-Z])", " $1");
        return finalString;
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

    private void ShowBasicTooltip()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = _ability.summary;
        cooldown.text = _ability.cooldown.ToString("F1") + " sec Cooldown";
        description.text = ParseAbilityDescription();
        uniquePassiveText.text = "";
    }

    private void ShowDetailedTooltip()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = _ability.summary;
        cooldown.text = _ability.cooldown.ToString("F1") + " sec Cooldown";
        description.text = ParseAbilityDescriptionDetailed();
        uniquePassiveText.text = "";
    }

    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                ShowDetailedTooltip();
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                ShowBasicTooltip();
            }
        }
    }

}
