using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] Image experienceBarImage;
    [SerializeField] Player player;

    public float maxExperience = 100;
    private float currentExperience = 100;

    private void Start()
    {
        player.OnXPChanged += UpdateExperience;
    }

    public void UpdateExperience(int experience)
    {
        currentExperience = experience;
        experienceBarImage.fillAmount = currentExperience / maxExperience;
    }
}
