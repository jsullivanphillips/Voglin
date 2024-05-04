using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField]
    TMP_Text _DamageText;

    public void SetDamage(int damage)
    {
        _DamageText.text = damage.ToString();
        BubbleBlowUp();
    }

    private void BubbleBlowUp()
    {
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}
