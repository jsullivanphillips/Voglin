using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BasicAttack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private AbilitySO basicAttack;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        {
            TooltipManager.Instance.ShowAbilityTooltip(basicAttack, this.transform.position);
        }
        // else, showing crafting result from two items
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideAbilityTooltip();
    }
}
