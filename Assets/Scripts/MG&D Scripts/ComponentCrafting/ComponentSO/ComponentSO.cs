using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Component", menuName = "Components/Component")]
public class ComponentSO : ScriptableObject
{
    public Sprite sprite;
    public new string name;
    public int id;
}
