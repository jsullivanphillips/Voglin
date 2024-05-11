using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    Image icon;
    [SerializeField]
    private TMP_Text nameText, summaryText, statRow1, statRow2, statRow3, uniquePassiveText;


    private ComponentSO _component;

    public void SetComponentSO(ComponentSO component)
    {
        _component = component;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        icon.sprite = _component.icon;
        nameText.text = _component.name;
        summaryText.text = _component.summary;
        FillStatRows();
        uniquePassiveText.text = "";
    }

    private void FillStatRows()
    {
        statRow1.gameObject.SetActive(false);
        statRow2.gameObject.SetActive(false);
        statRow3.gameObject.SetActive(false);

        for(int i = 0; i < _component.stats.Count; i++)
        {
            switch(i)
            {
                case 0:
                    statRow1.gameObject.SetActive(true);
                    statRow1.text = "+ " + _component.stats[i].value + " <color=\"green\">" + BreakEnumToString(_component.stats[i].stat) + "</color>";
                    break;
                case 1:
                    statRow2.gameObject.SetActive(true);
                    statRow2.text = "+ " + _component.stats[i].value + " <color=\"green\">" + BreakEnumToString(_component.stats[i].stat) + "</color>";
                    break;
                case 2:
                    statRow3.gameObject.SetActive(true);
                    statRow3.text = "+ " + _component.stats[i].value + " <color=\"green\">" + BreakEnumToString(_component.stats[i].stat) + "</color>";
                    break;
            }
        }
    }

    private string BreakEnumToString(Stat stat)
    {
        string statString = stat.ToString();
        string finalString = Regex.Replace(statString, "(?<!^)([A-Z])", " $1");
        return finalString;
    }



    // Implement the IPointerEnterHandler interface
    public void OnPointerEnter(PointerEventData eventData)
    {
       
        // animation
    }

    // Implement the IPointerExitHandler interface
    public void OnPointerExit(PointerEventData eventData)
    {
        
        // cancel animation
    }

    // Implement the IPointerClickHandler interface
    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryViewManager.Instance.OpenInventory();
        CraftingTable.Instance.SpawnItemComponent(_component);
        ChooseNewCardManager.Instance.CloseDisplay();
    }

}
