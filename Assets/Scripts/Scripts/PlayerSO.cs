using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerSO", menuName = "PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public new string name;
    public string bio;
    public string tag;
    public CustomColor primaryColor;
    public CustomColor secondaryColor;
}
