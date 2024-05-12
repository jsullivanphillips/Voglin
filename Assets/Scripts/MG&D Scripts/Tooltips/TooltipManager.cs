using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    #region Singleton
    public static TooltipManager Instance { get; private set; }

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
    #endregion

    [SerializeField]
    private Transform _ComponentTooltip;

    [SerializeField]
    private Transform _AbilityTooltip;


    void Start()
    {
        HideComponentTooltip();
        HideAbilityTooltip();
    }

    public void ShowComponentTooltip(ComponentSO component, Vector3 position)
    {
        _ComponentTooltip.gameObject.SetActive(true);
        _ComponentTooltip.GetComponent<ComponentTooltip>().SetComponent(component);
        _ComponentTooltip.position = position + new Vector3(-8f, 8f, 0f);
    }

    public void HideComponentTooltip()
    {
        _ComponentTooltip.gameObject.SetActive(false);
    }

    public void ShowAbilityTooltip(AbilitySO ability, Vector3 position)
    {
        _AbilityTooltip.gameObject.SetActive(true);
        _AbilityTooltip.GetComponent<AbilityTooltip>().SetAbility(ability);
        _AbilityTooltip.position = position + new Vector3(-8f, 8f, 0f);
    }

    public void HideAbilityTooltip()
    {
        _AbilityTooltip.gameObject.SetActive(false);
    }

}
