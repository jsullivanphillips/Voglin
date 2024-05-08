using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

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
    Image bagIcon;

    private bool isBagCooldownActive = false;
    private float bagCooldownTimer = 0f;
    private float bagCooldown = 1.5f;

    [SerializeField]
    Image _BasicAttackIcon;
    [SerializeField]
    Image _BasicAttackFillIcon;

    private bool isBasicAttackCooldownActive = false;
    private float basicAttackCooldownTimer = 0f;
    private float basicAttackCooldown = 1.5f;

    [SerializeField]
    TMP_Text _LevelText;
    [SerializeField]
    TMP_Text _AvailableSkillPointsText;
    

    public void SetBagIconCooldown(float cooldownTime)
    {
        isBagCooldownActive = true;
        bagCooldownTimer = cooldownTime;
        bagCooldown = cooldownTime;
    }

    public void SetBasicAttackIconCooldown(float cooldownTime)
    {
        isBasicAttackCooldownActive = true;
        basicAttackCooldownTimer = cooldownTime;
        basicAttackCooldown = cooldownTime;
    }

    public void SetLevelText(int level)
    {
        _LevelText.text = level.ToString();
    }

    public void SetAvailableSkillPointsText(int skillPoints)
    {
        _AvailableSkillPointsText.transform.parent.gameObject.SetActive(skillPoints > 0);
        _AvailableSkillPointsText.text = skillPoints.ToString();
    }

    private void Update()
    {
        if(GameStateManager.Instance.IsPaused())
        {
            return;
        }

        if (isBagCooldownActive)
        {
            bagCooldownTimer -= Time.deltaTime;
            bagIcon.fillAmount = bagCooldownTimer / bagCooldown;
            if (bagCooldownTimer <= 0)
            {
                isBagCooldownActive = false;
            }
        }

        if (isBasicAttackCooldownActive)
        {
            basicAttackCooldownTimer -= Time.deltaTime;
            _BasicAttackFillIcon.fillAmount = basicAttackCooldownTimer / basicAttackCooldown;
            if (basicAttackCooldownTimer <= 0)
            {
                isBasicAttackCooldownActive = false;
            }
        }
    }

}
