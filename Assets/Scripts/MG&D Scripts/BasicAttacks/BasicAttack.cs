using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BasicAttack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private BasicAttackSO basicAttackSO;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        {
            TooltipManager.Instance.ShowBasicAttackTooltip(basicAttackSO, this.transform.position);
        }
        // else, showing crafting result from two items
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideBasicAttackTooltip();
    }
}
