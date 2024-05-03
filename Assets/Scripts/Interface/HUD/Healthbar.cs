using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image healthBarImage;
    [SerializeField] Player player;

    private float maxHealth = 100;
    private float currentHealth = 100;

    private void Start()
    {
        player.OnHealthChanged += UpdateHealth;
    }

    public void UpdateHealth(float health)
    {
        currentHealth = health;
        healthBarImage.fillAmount = currentHealth / maxHealth;
    }
}
