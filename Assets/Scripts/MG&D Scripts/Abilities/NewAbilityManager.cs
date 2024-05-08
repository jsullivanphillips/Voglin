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
    private List<AbilitySO> _TEMP_Abilities = new List<AbilitySO>();

    public void StartDisplayAbilityOptions(int abilitySlot)
    {
        GameStateManager.Instance.PauseGame();
        _AbilityOptionsLayoutGroup.transform.parent.gameObject.SetActive(true);

        StartCoroutine(DisplayAbilityOptions(abilitySlot));
    }

    private IEnumerator DisplayAbilityOptions(int abilitySlot)
    {
        yield return new WaitForSeconds(0.25f);

        foreach (AbilitySO ability in _TEMP_Abilities)
        {
            GameObject abilityOption = Instantiate(_AbilityCardPrefab, _AbilityOptionsLayoutGroup);
            abilityOption.GetComponent<AbilityCard>().SetAbility(ability, abilitySlot);
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


}
