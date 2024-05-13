using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUpgradeManager : MonoBehaviour
{
    public static AbilityUpgradeManager Instance { get; private set; }

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
    private NewAbilityManager _newAbilityManager;
    [SerializeField]
    private AbilityHUDManager _AbilityHUDManager;
    [SerializeField]
    private HUDManager _HUDManager;

    Dictionary<int, AbilitySO> abilityDictionary = new Dictionary<int, AbilitySO>();

    private int _availableSkillPoints = 0;

    public int GetAvailableSkillPoints()
    {
        return _availableSkillPoints;
    }

    public void LevelUp()
    {
        _availableSkillPoints++;
        _AbilityHUDManager.SetSkillPoints(_availableSkillPoints);
    }

    private void SkillPointSpent()
    {
        _availableSkillPoints--;
        _AbilityHUDManager.SetSkillPoints(_availableSkillPoints);

        if(!InventoryViewManager.Instance.IsInventoryOpen())
        {
            _HUDManager.SetDarkenedBackgroundForHUDHighLight(false);
        }
        
        _HUDManager.SetSpendAbilityPointsText(false);
    }

    public void ChooseAbility(AbilitySO ability, int abilitySlot)
    {
        AbilitySO chosenAbility = Instantiate(ability);
        chosenAbility.abilitySlot = abilitySlot;
        
        if(abilityDictionary.ContainsKey(abilitySlot))
        {
            abilityDictionary[abilitySlot] = chosenAbility;
        }
        else
        {
            abilityDictionary.Add(abilitySlot, chosenAbility);
        }
        _AbilityHUDManager.SetAbilitySlot(abilitySlot, chosenAbility);
        PlayerAbilityManager.Instance.AddAbility(chosenAbility);
        _newAbilityManager.RemoveAbility(ability);
        _newAbilityManager.HideAbilityOptions();
        if(!InventoryViewManager.Instance.IsInventoryOpen())
        {
            GameStateManager.Instance.ResumeGame();
        }
    }

    public void UpgradeAbilityInSlot(int abilitySlot)
    {
        if(abilityDictionary.ContainsKey(abilitySlot))
        {
            AbilitySO ability = abilityDictionary[abilitySlot];   

            if(ability.upgrade != null)
            {
                ability.UpgradeAbility(ability.upgrade);
            }
            else
            {
                Debug.Log("No upgrade available for " + ability.name);
            }
            
            _AbilityHUDManager.SetAbilitySlot(abilitySlot, ability);  
        }
    }

    public void UpgradeAbilitySlot1()
    {
        SkillPointSpent();
        if(abilityDictionary.ContainsKey(1))
        {
            UpgradeAbilityInSlot(1);
        }
        else
        {
            _newAbilityManager.StartDisplayAbilityOptions(1);
        }
        
    }

    public void UpgradeAbilitySlot2()
    {
        SkillPointSpent();
        if(abilityDictionary.ContainsKey(2))
        {
            UpgradeAbilityInSlot(2);
        }
        else
        {
            _newAbilityManager.StartDisplayAbilityOptions(2);
        }
    }

    public void UpgradeAbilitySlot3()
    {
        SkillPointSpent();
        if(abilityDictionary.ContainsKey(3))
        {
            UpgradeAbilityInSlot(3);
        }
        else
        {
            _newAbilityManager.StartDisplayAbilityOptions(3);
        }
    }

    public void UpgradeAbilitySlot4()
    {
        SkillPointSpent();
        if(abilityDictionary.ContainsKey(4))
        {
            UpgradeAbilityInSlot(4);
        }
        else
        {
            _newAbilityManager.StartDisplayAbilityOptions(4);
        }
    }

    public void UpgradeAbilitySlot5()
    {
        SkillPointSpent();
        if(abilityDictionary.ContainsKey(5))
        {
            UpgradeAbilityInSlot(5);
        }
        else
        {
            _newAbilityManager.StartDisplayAbilityOptions(5);
        }
    }
   
}
