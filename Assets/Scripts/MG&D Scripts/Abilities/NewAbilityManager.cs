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
        _AbilityOptionsLayoutGroup.transform.parent.gameObject.SetActive(true);

        StartCoroutine(DisplayAbilityOptions(abilitySlot));
    }

    private IEnumerator DisplayAbilityOptions(int abilitySlot)
    {
        yield return new WaitForSeconds(0.25f);

        List<AbilitySO> availableAbilities = new List<AbilitySO>();
        availableAbilities.AddRange(_Abilities);

        for(int i = 0; i < 3; i++)
        {
            GameObject abilityOption = Instantiate(_AbilityCardPrefab, _AbilityOptionsLayoutGroup);

            AbilitySO tempAbility = availableAbilities[Random.Range(0, availableAbilities.Count)];
            availableAbilities.Remove(tempAbility);

            abilityOption.GetComponent<AbilityCard>().SetAbility(tempAbility, abilitySlot);
            if(availableAbilities.Count == 0)
            {
                break;
            }
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
