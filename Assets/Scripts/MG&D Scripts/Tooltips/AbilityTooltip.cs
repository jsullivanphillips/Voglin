using System;
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
    [SerializeField]
    private bool isUpgradeTooltip = false;

    private AbilitySO _ability;
    private AbilitySO _upgradeAbility;


    public void SetAbility(AbilitySO ability)
    {
        _ability = ability;
        ShowBasicTooltip();
    }

    public void SetUpgradeAbility(AbilitySO ability, AbilitySO upgradeAbility)
    {
        _ability = ability;
        _upgradeAbility = upgradeAbility;
        ShowUpgradeTooltip();
    }


    private string BreakEnumToString(Stat stat)
    {
        string statString = stat.ToString();
        string finalString = Regex.Replace(statString, "(?<!^)([A-Z])", " $1");
        return finalString;
    }

    private string GetCooldownText()
    {
        string cooldownText = "";
        if(_ability.abilityType == AbilityType.BasicAttack)
        {
            float cooldownReduction = PlayerItems.Instance.GetAttackSpeed() / 100f;
            cooldownReduction = Mathf.Min(cooldownReduction, 0.65f); // Ensure the reduction does not exceed 65%
            cooldownText = (_ability.cooldown * (1 - cooldownReduction)).ToString("F1") + "s";
        }
        else
        {
            cooldownText = _ability.cooldown.ToString("F1") + "s"; // Plus items
        }   
        return cooldownText;
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


        string cooldownText = GetCooldownText();
        string parsedDescription = _ability.description.Replace("<damage>", damageVar);
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldownText);
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

        string cooldownText = GetCooldownText();
        string parsedDescription = _ability.description.Replace("<damage>", damageVar);
        parsedDescription = parsedDescription.Replace("<cooldown>", cooldownText);
        parsedDescription = parsedDescription.Replace("<attackRange>", _ability.attackRange.ToString("F0"));
        return parsedDescription;
    }

    private string ParseUpgradeDescription()
    {
        string upgradeChanges = "";
        if(_upgradeAbility.damage != _ability.damage)
        {
            if(_ability.abilityType == AbilityType.AttackSpeed)
            {
                upgradeChanges += $"<color=\"yellow\">Attack Speed:</color> {_ability.damage}% -> {_upgradeAbility.damage}%\n";
            }
            else
            {
                upgradeChanges += $"<color=\"yellow\">Damage:</color> {_ability.damage} -> {_upgradeAbility.damage}\n";
            }
        }
        if(_upgradeAbility.attackRange != _ability.attackRange)
        {
            upgradeChanges += $"<color=\"yellow\">Attack Range:</color> {_ability.attackRange} -> {_upgradeAbility.attackRange}\n";
        }
        if(_upgradeAbility.cooldown != _ability.cooldown)
        {
            upgradeChanges += $"<color=\"yellow\">Cooldown:</color> {_ability.cooldown} -> {_upgradeAbility.cooldown}\n";
        }
        if(_upgradeAbility.numberOfPierces != _ability.numberOfPierces)
        {
            upgradeChanges += $"<color=\"yellow\">Pierces:</color> {_ability.numberOfPierces} -> {_upgradeAbility.numberOfPierces}\n";
        }
        if(_upgradeAbility.isStutter != _ability.isStutter)
        {
            upgradeChanges += $"<color=\"yellow\">Stutter:</color> {_ability.isStutter} -> {_upgradeAbility.isStutter}\n";
        }
        if(_upgradeAbility.isSplitOnCrit != _ability.isSplitOnCrit)
        {
            upgradeChanges += $"<color=\"yellow\">Split on Crit:</color> {_ability.isSplitOnCrit} -> {_upgradeAbility.isSplitOnCrit}\n";
        }
        if(_upgradeAbility.appliesOnHit != _ability.appliesOnHit)
        {
            upgradeChanges += $"<color=\"yellow\">Applies on Hit:</color> {_ability.appliesOnHit} -> {_upgradeAbility.appliesOnHit}\n";
        }
        if(_upgradeAbility.lobDelayTime != _ability.lobDelayTime)
        {
            upgradeChanges += $"<color=\"yellow\">Lob Delay Time:</color> {_ability.lobDelayTime} -> {_upgradeAbility.lobDelayTime}\n";
        }
        if(_upgradeAbility.explosionRadius != _ability.explosionRadius)
        {
            upgradeChanges += $"<color=\"yellow\">Explosion Radius:</color> {_ability.explosionRadius} -> {_upgradeAbility.explosionRadius}\n";
        }
        if(_upgradeAbility.projectileSpeed != _ability.projectileSpeed)
        {
            upgradeChanges += $"<color=\"yellow\">Projectile Speed:</color> {_ability.projectileSpeed} -> {_upgradeAbility.projectileSpeed}\n";
        }
        foreach(ScalingStats stat in _upgradeAbility.scalingStats)
        {
            if(_ability.scalingStats.Find(x => x.scalingStat == stat.scalingStat).scaling == stat.scaling) continue; // Skip if the scaling is the same (no change
            string statName = BreakEnumToString(stat.scalingStat);
            string scalingValue = (stat.scaling * 100).ToString("F0");
            string statColour = PlayerItems.Instance.GetStatHexColor(stat.scalingStat);
            string scalingStatValue = PlayerItems.Instance.GetScalingStat(stat.scalingStat).ToString("F0");
            upgradeChanges += $"{PlayerItems.Instance.GetStatHexColor(stat.scalingStat)}{statName}</color>: {_ability.scalingStats.Find(x => x.scalingStat == stat.scalingStat).scaling * 100}% -> {scalingValue}%\n";
        }

        return upgradeChanges;
    }

    private void ShowBasicTooltip()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = _ability.summary;
        cooldown.text = GetCooldownText() + "ec Cooldown";
        description.text = ParseAbilityDescription();
        uniquePassiveText.text = "";
    }

    private void ShowDetailedTooltip()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = _ability.summary;
        cooldown.text = GetCooldownText() + "ec Cooldown";
        description.text = ParseAbilityDescriptionDetailed();
        uniquePassiveText.text = "";
    }

    private void ShowUpgradeTooltip()
    {
        icon.sprite = _ability.icon;
        nameText.text = _ability.name;
        subHeading.text = _ability.summary;
        cooldown.text = GetCooldownText() + "ec Cooldown";
        description.text = ParseUpgradeDescription();
        uniquePassiveText.text = "";
    }

    void Update()
    {
        if(isUpgradeTooltip)
            return;
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
