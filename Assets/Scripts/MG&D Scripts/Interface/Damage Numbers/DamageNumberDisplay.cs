using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberDisplay : MonoBehaviour
{
    public static DamageNumberDisplay Instance { get; private set; }

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
    GameObject damageNumberPrefab;
    [SerializeField]
    Transform _WorldSpaceCanvas;

    public void DisplayDamageNumber(int damage, Vector3 position)
    {
        // Instantiate the damage number as a child of the world space Canvas
        Vector3 spawnPositionOffset = new Vector3(0f, 0.5f, 0f);
        GameObject damageNumber = Instantiate(damageNumberPrefab, position + spawnPositionOffset, Quaternion.identity, _WorldSpaceCanvas);

        // Set the damage
        damageNumber.GetComponent<DamageNumber>().SetDamage(damage);
    }
}
