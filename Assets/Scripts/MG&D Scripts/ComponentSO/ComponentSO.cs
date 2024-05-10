using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Stat
{
    AttackSpeed,
    CooldownReduction,
    PhysicalPower,
    MagicPower,
    CriticalChance,
    MoveSpeed,
    LifeSteal,
    Health
}

[System.Serializable]
public class StatFloatPair
{
    public Stat stat;
    public float value;
}

[CreateAssetMenu(fileName = "New Component", menuName = "Components/Component")]
public class ComponentSO : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public string summary;
    public List<StatFloatPair> stats;

    public string uniquePassive;
    public float uniquePassiveValue;

    public int id;
}
