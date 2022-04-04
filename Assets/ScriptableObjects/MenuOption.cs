using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New MenuOption", menuName = "MenuOption")]
public class MenuOption : ScriptableObject
{
    public new string name;
    public string description;
    public Enums.MenuType menuType;
    public Image thumb;
    public CustomColor customColor;
}
