using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewManager : MonoBehaviour
{
    [SerializeField]
    GameObject _InventoryView;
    private bool isCooldownActive = false;
    private float cooldownTime = 1.5f; // Set your desired cooldown time here

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
           PressB();
        }
    }

    private IEnumerator StartCooldown()
    {
        isCooldownActive = true;
        HUDManager.Instance.SetBagIconCooldown(cooldownTime);
        yield return new WaitForSeconds(cooldownTime);
        isCooldownActive = false;
    }

    public void PressB()
    {
        // This is to prevent player from reopening inventory while game is unpausing
        if(!_InventoryView.activeSelf && GameStateManager.Instance.IsPaused())
        {
            return;
        }

        // If cooldown is active, don't allow opening the inventory
        if (isCooldownActive)
        {
            return;
        }

        _InventoryView.SetActive(!_InventoryView.activeSelf);

        if (_InventoryView.activeSelf)
        {
            GameStateManager.Instance.PauseGame();
        }
        else
        {
            GameStateManager.Instance.ResumeGame();
            StartCoroutine(StartCooldown());
        }
    }
}
