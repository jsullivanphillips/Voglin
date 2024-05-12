using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAbilityManager : MonoBehaviour
{
    [SerializeField]
    private Transform _AbilityOptionsLayoutGroup;

    [SerializeField]
    private GameObject _AbilityCardPrefab;
    

    // TEMP
    [SerializeField]
    private List<AbilitySO> _Abilities = new List<AbilitySO>();

    public void StartDisplayAbilityOptions(int abilitySlot)
    {
        GameStateManager.Instance.PauseGame();

        

        DisplayAbilityOptions(abilitySlot);
    }

    private void DisplayAbilityOptions(int abilitySlot)
    {
        _AbilityOptionsLayoutGroup.transform.parent.gameObject.SetActive(true);

        List<AbilitySO> availableAbilities = new List<AbilitySO>();
        availableAbilities.AddRange(_Abilities);

        for(int i = 0; i < 3; i++)
        {
            if(availableAbilities.Count == 0)
            {
                break;
            }
            GameObject abilityOption = Instantiate(_AbilityCardPrefab, _AbilityOptionsLayoutGroup);

            AbilitySO tempAbility = availableAbilities[Random.Range(0, availableAbilities.Count)];
            availableAbilities.Remove(tempAbility);

            abilityOption.GetComponent<AbilityCard>().SetAbility(tempAbility, abilitySlot);
            
        }
    }

    public void HideAbilityOptions()
    {
        foreach (Transform child in _AbilityOptionsLayoutGroup)
        {
            Destroy(child.gameObject);
        }
        _AbilityOptionsLayoutGroup.transform.parent.gameObject.SetActive(false);
    }

    public void RemoveAbility(AbilitySO abilityToRemove)
    {
        _Abilities.RemoveAll(ability => ability.name == abilityToRemove.name);
    }


}
