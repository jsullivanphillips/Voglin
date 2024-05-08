using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewManager : MonoBehaviour
{
    public static InventoryViewManager Instance { get; private set; }

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

    public void OpenInventory()
    {
        _InventoryView.SetActive(true);
        GameStateManager.Instance.PauseGame();
    }

    public void CloseInventory()
    {
        _InventoryView.SetActive(false);
    }

    public void PressB()
    {
        // This is to prevent player from reopening inventory while game is unpausing
        if(!_InventoryView.activeSelf && GameStateManager.Instance.IsPaused())
        {
            return;
        }

        // If cooldown is active, don't allow opening the inventory
        if (isCooldownActive || GameStateManager.Instance.GetGameState() == GameState.ChoosingNewAbility)
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
