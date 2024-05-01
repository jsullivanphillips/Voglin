using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetBagIconCooldown(float cooldownTime)
    {
        isBagCooldownActive = true;
        bagCooldownTimer = cooldownTime;
        bagCooldown = cooldownTime;
    }

    private void Update()
    {
        if (isBagCooldownActive)
        {
            bagCooldownTimer -= Time.deltaTime;
            bagIcon.fillAmount = bagCooldownTimer / bagCooldown;
            if (bagCooldownTimer <= 0)
            {
                isBagCooldownActive = false;
            }
        }
    }
}
