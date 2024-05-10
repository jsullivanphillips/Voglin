using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ItemComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image _Icon;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Image _CraftingFillBar;
    [SerializeField]
    private GameObject _FillBarGO;
    [SerializeField]
    private ComponentCraftingHandler craftingHandler;
 
    private ItemComponent craftingPartner;
    private ComponentSO componentSO;
    private Vector2 originalPosition;
    private Coroutine fillBarAnimation;

    
    protected bool isDraggable = true;

    
    [HideInInspector] public RectTransform boundsBox;
    [HideInInspector] public Transform craftingArea;
    [HideInInspector] public Transform originalParent;
    [HideInInspector] public ItemSlot currentItemSlot;
    [HideInInspector] public bool isInRack = false;
    [HideInInspector] public bool isCrafting = false;
    [HideInInspector] public int id;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }
        
        originalPosition = this.transform.position;

        this.transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }
        // Calculate the position of the item
        Vector2 position = eventData.position;

        // Set this objects position
        this.transform.position = position;

        StopCraftingAnimation();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }

        // Get the corners of the item in screen space
        Vector3[] itemCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(itemCorners);

        // Convert the bounds of the box to screen space
        Vector3[] boxCorners = new Vector3[4];
        boundsBox.GetWorldCorners(boxCorners);

        // Check if the item is outside the bounds box and not in an item slot
        if ((itemCorners[0].x < boxCorners[0].x || itemCorners[2].x > boxCorners[2].x || 
            itemCorners[0].y < boxCorners[0].y || itemCorners[2].y > boxCorners[2].y) &&
            !ItemSlotManager.Instance.isItemSlotMousedOver())
        {
            this.transform.position = originalPosition;
        }

        if(ItemSlotManager.Instance.isItemSlotMousedOver())
        {
            SetOriginalParent();
        }
        else if (!(itemCorners[0].x < boxCorners[0].x || itemCorners[2].x > boxCorners[2].x || 
            itemCorners[0].y < boxCorners[0].y || itemCorners[2].y > boxCorners[2].y))
        {
            SetCraftingAreaParent();
            
        }


        canvasGroup.blocksRaycasts = true;
    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        {
            TooltipManager.Instance.ShowComponentTooltip(componentSO, this.transform.position);
        }
        // else, showing crafting result from two items
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideComponentTooltip();
    }

    public Vector3 GetOriginalPosition()
    {
        return originalPosition;
    }

    public void SetOriginalPosition(Vector3 newPosition)
    {
        originalPosition = newPosition;
    }

    public void SetOriginalParent()
    {
        this.transform.SetParent(originalParent);
        isInRack = true;
    }

    public void SetCraftingAreaParent()
    {
        this.transform.SetParent(craftingArea);
        if(isInRack)
        {
            PlayerItems.Instance.RemoveItem(GetComponentSO());
        }
        isInRack = false;
    }

    public ComponentSO GetComponentSO()
    {
        return componentSO;
    }

    public void SetComponentSO(ComponentSO _componentSO)
    {
        componentSO = _componentSO;
        _Icon.sprite = _componentSO.icon;
    }

    public void SetCraftingFlagTrue(ItemComponent otherComponent)
    {
        isCrafting = true;
        otherComponent.isCrafting = true;
        craftingPartner = otherComponent;
    }

    public void StartCraftingAnimation(ItemComponent otherComponent)
    {
        isCrafting = true;
        otherComponent.isCrafting = true;
        craftingPartner = otherComponent;
        _FillBarGO.SetActive(true);
        fillBarAnimation = StartCoroutine(FillCraftingBar());
    }

    private IEnumerator FillCraftingBar()
    {
        float fillAmount = 0;
        while(fillAmount < 1)
        {
            fillAmount += Time.deltaTime * 0.75f; // Adjust the fill speed here
            _CraftingFillBar.fillAmount = fillAmount;
            yield return null;
        }
        craftingHandler.CraftItems(this.GetComponentSO(), craftingPartner.GetComponentSO());
        _FillBarGO.SetActive(false);
        isCrafting = false;
    }

    public void StopCraftingAnimation()
    {
        if(isCrafting && craftingPartner != null)
        {
            isCrafting = false;
            craftingPartner.StopCraftingAnimation();
            craftingPartner = null;
        }
        
        if(fillBarAnimation != null)
        {
            StopCoroutine(fillBarAnimation);
        }
        _FillBarGO.SetActive(false);
        _CraftingFillBar.fillAmount = 0;
        isCrafting = false;
    }
}
