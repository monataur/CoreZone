using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;

public class CharacterInitialization : MonoBehaviour
{
    public string selectedFirstName; // First and last names are used for referring to the character, probably won't be used in multiplayer
    public string selectedLastName;
    public string selectedNickname; // Nickname is what will be used to refer to the character in battles, and in menus
    public string selectedGender; // Selected gender. Has little implementation 
    public Color selectedColor; // Player's selected color. This will be used for the GUI stuff mainly.

    public GameObject firstNameInput;
    public GameObject lastNameInput;
    public GameObject nickNameInput;
    public Text firstNameInputText;
    public Text lastNameInputText;
    public Text nickNameInputText;
    public int genderDropdownInt;
    public Dropdown genderDropdown;
    public bool canProceedGender;
    public bool canProceedName;
    public bool canProceedGeneral;
    public Text canProceedTextObject;
    public int maxFirstName = 12;
    public int maxLastName = 16;
    public int maxNickname = 8;
    public bool firstNameValid;
    public bool lastNameValid;
    public bool nickNameValid;
    public string playerGuid;

    void FirstNameMirror()
    {
        selectedFirstName = firstNameInputText.text; 
    }

    void LastNameMirror()
    {
        selectedLastName = lastNameInputText.text;
    }

    void NickNameMirror()
    {
        selectedNickname = nickNameInputText.text;
    }

    void GenderMirror()
    {
        genderDropdownInt = genderDropdown.value;

        switch (genderDropdownInt)
        {
            case 0:
                selectedGender = "Null";
                canProceedGender = false;
                break;

            case 1:
                selectedGender = "Male";
                canProceedGender = true;
                break;

            case 2:
                selectedGender = "Female";
                canProceedGender = true;
                break;

            case 3:
                selectedGender = "Other";
                canProceedGender = true;
                break;
        }
    }

    public void CannotProceedRedText()
    {
        switch (canProceedGeneral)
        {
            case true:

                break;
        }
    }

    void Start()
    {
        GenerateGUID();
    }

    public void GenerateGUID()
    {
        var newGuid = System.Guid.NewGuid();
        var newGuidString = newGuid.ToString();
    }

    public void GeneralCheck()
    {
        if (canProceedGender == true && canProceedName == true)
        {
            canProceedGeneral = true;
        }

        else if (canProceedGender == false || canProceedName == false)
        {
            canProceedGeneral = false;
        }
    }

    public void NameCheck()
    {
        var firstNameLength = selectedFirstName.Length;
        var lastNameLength = selectedLastName.Length;
        var NicknameLength = selectedNickname.Length;

        if (firstNameLength == maxFirstName)
        {
            firstNameValid = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        FirstNameMirror();
        LastNameMirror();
        NickNameMirror();
        GenderMirror();
        GeneralCheck();
        
    }
}
