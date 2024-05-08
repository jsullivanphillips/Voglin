using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField]
    private Image _AbilityImage;
    [SerializeField]
    private Image _CooldownImage;
    [SerializeField]
    private GameObject _SkillPointBtn;

    private float cooldown;
    private float cooldownTimer;

    public void SetAbilityImage(Sprite image)
    {
        _AbilityImage.gameObject.SetActive(true);
        _AbilityImage.sprite = image;
        _CooldownImage.sprite = image;
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
