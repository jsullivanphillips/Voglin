using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image _AbilityImage;
    [SerializeField]
    private Image _CooldownImage;
    [SerializeField]
    private GameObject _SkillPointBtn;

    private float cooldown;
    private float cooldownTimer;
    private AbilitySO _ability;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_ability != null)
        {
            if(AbilityUpgradeManager.Instance.GetAvailableSkillPoints() > 0 && _ability.upgrade != null)
            {
                TooltipManager.Instance.ShowUpgradeAbilityTooltip(_ability, _ability.upgrade, this.transform.position);
            }
            else
            {
                TooltipManager.Instance.ShowAbilityTooltip(_ability, this.transform.position);
            }
        }    
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideAbilityTooltip();
    }

    public void SetAbility(AbilitySO ability)
    {
        _ability = ability;
        _AbilityImage.gameObject.SetActive(true);
        _AbilityImage.sprite = ability.icon;
        _CooldownImage.sprite = ability.icon;
    }

    public void SetCooldown(float cooldown)
    {
        cooldownTimer = cooldown;
        this.cooldown = cooldown;
    }

    public void SetSkillPointBtn(bool state)
    {
        _SkillPointBtn.SetActive(state);
    }

    private void Update()
    {
        if(GameStateManager.Instance.IsPaused())
        {
            return;
        }

        if(cooldownTimer > 0f)
        {   
            cooldownTimer -= Time.deltaTime;
            _CooldownImage.fillAmount = cooldownTimer / cooldown;
        }
        else
        {
            _CooldownImage.fillAmount = 0f;
        }
    }
}
