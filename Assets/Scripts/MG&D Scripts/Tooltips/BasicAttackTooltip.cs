using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class BasicAttackTooltip : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text nameText, subHeading, cooldown, description;

    private BasicAttackSO _basicAttack;

    public void SetBasicAttack(BasicAttackSO basicAttack)
    {
        _basicAttack = basicAttack;

        icon.sprite = basicAttack.icon;
        nameText.text = basicAttack.name;
        subHeading.text = BreakEnumToString(basicAttack.scalingStat);
        cooldown.text = basicAttack.cooldown + " sec Cooldown";
        description.text = ParseAbilityDescription();
    }

    private string BreakEnumToString(ScalingStat stat)
    {
        string statString = stat.ToString();
        string finalString = Regex.Replace(statString, "(?<!^)([A-Z])", " $1");
        return finalString;
    }

    private string ParseAbilityDescription()
    {
        float scalingStatValue = 0f;
        if(_basicAttack.scalingStat == ScalingStat.PhysicalPower)
        {
            scalingStatValue = PlayerItems.Instance.GetPhysicalPower();
        }
        // else if(_ability.scalingStat == ScalingStat.MagicPower)
        // {
        //     scalingStatValue = 
        // }

        float damage = _basicAttack.damage + (_basicAttack.scaling * scalingStatValue); // Plus scaling with items
        float cooldown = _basicAttack.cooldown; // Plus items
        string parsedDescription = _basicAttack.description.Replace("<damage>", "<color=\"yellow\">" + damage.ToString() + "</color>");
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldown.ToString());
        parsedDescription = parsedDescription.Replace("<attackRange>", _basicAttack.attackRange.ToString());
        return parsedDescription;
    }
}
