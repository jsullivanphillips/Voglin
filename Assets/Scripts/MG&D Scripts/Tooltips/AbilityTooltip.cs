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
        string statColor = "";
        float scalingStatValue = 0f;
        if(_ability.scalingStat == ScalingStat.PhysicalPower)
        {
            scalingStatValue = PlayerItems.Instance.GetPhysicalPower();
            statColor = "<color=\"orange\">";
        }
        else if(_ability.scalingStat == ScalingStat.MagicPower)
        {
            scalingStatValue = PlayerItems.Instance.GetMagicPower();
            statColor = "<color=#00FFFF>";
        }

        float scalingDamage = (_ability.scaling * scalingStatValue); // Plus scaling with items
        float cooldown = _ability.cooldown; // Plus items
        string parsedDescription = _ability.description.Replace("<damage>", $"<color=\"yellow\">{_ability.damage.ToString()}</color>" 
        + $" {statColor}(+{scalingDamage})</color>");
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldown.ToString());
        parsedDescription = parsedDescription.Replace("<attackRange>", _ability.attackRange.ToString());
        return parsedDescription;
    }

    private string ParseAbilityDescriptionDetailed()
    {
        string statColor = "";
        float scalingStatValue = 0f;
        if(_ability.scalingStat == ScalingStat.PhysicalPower)
        {
            scalingStatValue = PlayerItems.Instance.GetPhysicalPower();
            statColor = "<color=\"orange\">";
        }
        else if(_ability.scalingStat == ScalingStat.MagicPower)
        {
            scalingStatValue = PlayerItems.Instance.GetMagicPower();
            statColor = "<color=#00FFFF>";
        }

        float scalingDamage = (_ability.scaling * scalingStatValue); 
        float cooldown = _ability.cooldown; // Plus items
        string parsedDescription = _ability.description.Replace("<damage>", $"<color=\"yellow\">{_ability.damage.ToString()}</color>" 
        + $" {statColor}(+{_ability.scaling * 100}% of {scalingStatValue} {BreakEnumToString(_ability.scalingStat)})</color>");
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldown.ToString());
        parsedDescription = parsedDescription.Replace("<attackRange>", _ability.attackRange.ToString());
        return parsedDescription;
    }

    private void ShowBasicTooltip()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = BreakEnumToString(_ability.scalingStat);
        cooldown.text = _ability.cooldown + " sec Cooldown";
        description.text = ParseAbilityDescription();
        uniquePassiveText.text = "";
    }

    private void ShowDetailedTooltip()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = BreakEnumToString(_ability.scalingStat);
        cooldown.text = _ability.cooldown + " sec Cooldown";
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
