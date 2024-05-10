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

        icon.sprite = ability.icon;
        nameText.text = ability.name;
        subHeading.text = BreakEnumToString(ability.scalingStat);
        cooldown.text = ability.cooldown + " sec Cooldown";
        description.text = ParseAbilityDescription();
        uniquePassiveText.text = "";
    }

    private string BreakEnumToString(ScalingStat stat)
    {
        string statString = stat.ToString();
        string finalString = Regex.Replace(statString, "(?<!^)([A-Z])", " $1");
        return finalString;
    }

    private string ParseAbilityDescription()
    {
        float damage = _ability.damage; // Plus scaling with items
        float cooldown = _ability.cooldown; // Plus items
        string parsedDescription = _ability.description.Replace("<damage>", "<color=\"yellow\">" + damage.ToString() + "</color>");
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldown.ToString());
        parsedDescription = parsedDescription.Replace("<attackRange>", _ability.attackRange.ToString());
        return parsedDescription;
    }

}
