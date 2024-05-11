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
        if(_basicAttack.scalingStat == ScalingStat.PhysicalPower)
        {
            scalingStatValue = PlayerItems.Instance.GetPhysicalPower();
            statColor = "<color=\"orange\">";
        }
        else if(_basicAttack.scalingStat == ScalingStat.MagicPower)
        {
            scalingStatValue = PlayerItems.Instance.GetMagicPower();
            statColor = "<color=#00FFFF>";
        }

        float scalingDamage = (_basicAttack.scaling * scalingStatValue); 
        float cooldown = _basicAttack.cooldown; // Plus items
        string parsedDescription = _basicAttack.description.Replace("<damage>", $"<color=\"yellow\">{_basicAttack.damage.ToString()}</color>" 
        + $" {statColor}(+{scalingDamage})</color>");
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldown.ToString());
        parsedDescription = parsedDescription.Replace("<attackRange>", _basicAttack.attackRange.ToString());
        return parsedDescription;
    }

    private string ParseAbilityDescriptionDetailed()
    {
        string statColor = "";
        float scalingStatValue = 0f;
        if(_basicAttack.scalingStat == ScalingStat.PhysicalPower)
        {
            scalingStatValue = PlayerItems.Instance.GetPhysicalPower();
            statColor = "<color=\"orange\">";
        }
        else if(_basicAttack.scalingStat == ScalingStat.MagicPower)
        {
            scalingStatValue = PlayerItems.Instance.GetMagicPower();
            statColor = "<color=#00FFFF>";
        }

        float scalingDamage = (_basicAttack.scaling * scalingStatValue); 
        float cooldown = _basicAttack.cooldown; // Plus items
        string parsedDescription = _basicAttack.description.Replace("<damage>", $"<color=\"yellow\">{_basicAttack.damage.ToString()}</color>" 
        + $" {statColor}(+{_basicAttack.scaling * 100}% of {scalingStatValue} {BreakEnumToString(_basicAttack.scalingStat)})</color>");
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldown.ToString());
        parsedDescription = parsedDescription.Replace("<attackRange>", _basicAttack.attackRange.ToString());
        return parsedDescription;
    }

    private void ShowDetailedTooltip()
    {
        icon.sprite = _basicAttack.icon;
        nameText.text = _basicAttack.name;
        subHeading.text = BreakEnumToString(_basicAttack.scalingStat);
        cooldown.text = _basicAttack.cooldown + " sec Cooldown";
        description.text = ParseAbilityDescriptionDetailed();
    }

    private void ShowBasicTooltip()
    {
        icon.sprite = _basicAttack.icon;
        nameText.text = _basicAttack.name;
        subHeading.text = BreakEnumToString(_basicAttack.scalingStat);
        cooldown.text = _basicAttack.cooldown + " sec Cooldown";
        description.text = ParseAbilityDescription();
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
