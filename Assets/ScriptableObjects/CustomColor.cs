using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CustomColor", menuName = "CustomColor")]
public class CustomColor : ScriptableObject
{
    public new string name;
    public string description;
    public Color color;
}
