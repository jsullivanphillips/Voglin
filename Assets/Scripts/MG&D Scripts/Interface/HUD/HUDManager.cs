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
    Image _BasicAttackIcon;
    [SerializeField]
    Image _BasicAttackFillIcon;
    [SerializeField]
    TMP_Text _LevelText;
    [SerializeField]
    TMP_Text _AvailableSkillPointsText;
    [SerializeField]
    Player _Player;
    [SerializeField]
    Image _HealthBar;
    [SerializeField]
    TMP_Text _HealthText;
    [SerializeField]
    TMP_Text _HealthRegenText;

    [SerializeField]
    private GameObject _DarkenedBackgroundForHUDHighLight, _SpendSkillPointsTutorialText;

    private bool isBasicAttackCooldownActive = false;
    private float basicAttackCooldownTimer = 0f;
    private float basicAttackCooldown = 1.5f;

    void Start()
    {
        _Player.OnHealthChanged += UpdateHealth;
    }
    
    private void UpdateHealth(float health, float maxHealth)
    {
        _HealthBar.fillAmount = health / maxHealth;
        _HealthText.text = health.ToString("F0") + " / " + maxHealth.ToString("F0");

        if(_HealthBar.fillAmount == 1)
        {
            _HealthRegenText.transform.gameObject.SetActive(false);
        }
        else
        {
            _HealthRegenText.transform.gameObject.SetActive(true);
        }
    }

    public void SetBasicAttackIconCooldown(float cooldownTime)
    {
        isBasicAttackCooldownActive = true;
        basicAttackCooldownTimer = cooldownTime;
        basicAttackCooldown = cooldownTime;
    }

    public void SetDarkenedBackgroundForHUDHighLight(bool active)
    {
        _DarkenedBackgroundForHUDHighLight.SetActive(active);
    }

    public void SetSpendAbilityPointsText(bool active)
    {
        _SpendSkillPointsTutorialText.SetActive(active);
    }

    public void LevelUp(int level)
    {
        _LevelText.text = level.ToString();

        if (level == 2)
        {
            GameStateManager.Instance.PauseGame();
            _DarkenedBackgroundForHUDHighLight.SetActive(true);
            _SpendSkillPointsTutorialText.SetActive(true);
        }
    }

    public void SetHealthRegenText(float healthRegen)
    {
        if(_HealthBar.fillAmount == 1)
        {
            _HealthRegenText.transform.gameObject.SetActive(false);
        }
        else
        {
            _HealthRegenText.transform.gameObject.SetActive(true);
        }
        _HealthRegenText.text = healthRegen.ToString("F2");
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
