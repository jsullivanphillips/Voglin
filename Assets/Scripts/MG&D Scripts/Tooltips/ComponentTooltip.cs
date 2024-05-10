using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class ComponentTooltip : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text nameText, summaryText, statRow1, statRow2, statRow3, uniquePassiveText;

    private ComponentSO currentComponent;

    public void SetComponent(ComponentSO component)
    {
        currentComponent = component;
        icon.sprite = component.icon;
        nameText.text = component.name;
        summaryText.text = component.summary;
        FillStatRows();
        uniquePassiveText.text = "";


        // need to do some string parsing for formating the title and values for uniquepassivetext
    }

    private void FillStatRows()
    {
        statRow1.gameObject.SetActive(false);
        statRow2.gameObject.SetActive(false);
        statRow3.gameObject.SetActive(false);

        for(int i = 0; i < currentComponent.stats.Count; i++)
        {
            switch(i)
            {
                case 0:
                    statRow1.gameObject.SetActive(true);
                    statRow1.text = "+ " + currentComponent.stats[i].value + " <color=\"green\">" + BreakEnumToString(currentComponent.stats[i].stat) + "</color>";
                    break;
                case 1:
                    statRow2.gameObject.SetActive(true);
                    statRow2.text = currentComponent.stats[i].stat.ToString() + ": " + currentComponent.stats[i].value;
                    break;
                case 2:
                    statRow3.gameObject.SetActive(true);
                    statRow3.text = currentComponent.stats[i].stat.ToString() + ": " + currentComponent.stats[i].value;
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
}
