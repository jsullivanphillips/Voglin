using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobHUD : MonoBehaviour
{
    [SerializeField]
    private Image _HealthBar;

    private Mob _Mob;

    void Start()
    {
        _HealthBar.fillAmount = 1;
    }

    public void SetMob(Mob mob)
    {
        _Mob = mob;
        _Mob.OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(float health, float maxHealth)
    {
        _HealthBar.fillAmount = health / maxHealth;
    }
}
