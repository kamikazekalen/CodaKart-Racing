using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwapper : MonoBehaviour
{
    public GameObject player;

    public Dropdown characterDropdown, kartDropdown, hairDropdown, skinDropdown;

    public GameObject catCharacter, boyCharacter, girlCharacter, catKart, boyKart, girlKart;
    private int charInUse = 0;

    // Start is called before the first frame update
    void Start()
    {
        AnimController.character = player.transform.Find("Char").gameObject;
        AnimController.kart = player.transform.Find("Kart").gameObject;

        characterDropdown.onValueChanged.AddListener(delegate { CharacterChanged(); });
        kartDropdown.onValueChanged.AddListener(delegate { KartChanged(); });

        CharacterRotator.objectToRotate = player.transform;
    }

   void CreateNewCharacter(GameObject character, Vector3 position, Quaternion rotation)      
   {
        GameObject newChar = Instantiate(character, position, rotation);

        newChar.SetActive(true);
        newChar.name = "Char";
        newChar.transform.parent = player.transform;

        hairDropdown.value = 0;
        skinDropdown.value = 0;

        AnimController.characterAnim = newChar.GetComponent<Animator>();
   }
    
    void CharacterChanged()
    {
        GameObject previousChar = player.transform.Find("Char").gameObject;
        Vector3 previousPosition = previousChar.transform.position;
        Quaternion previousRotation = previousChar.transform.rotation;

        Destroy(previousChar);

        switch(characterDropdown.value)
        {
            case 0:
                //Cat Charater
                CreateNewCharacter(catCharacter, previousPosition, previousRotation);
                charInUse = 0;
                break;
            case 1:
                //Boy Charater
                CreateNewCharacter(boyCharacter, previousPosition, previousRotation);
                charInUse = 1;
                break;
            case 2:
                //Girl Charater
                CreateNewCharacter(girlCharacter, previousPosition, previousRotation);
                charInUse = 2;
                break;
        }
        CharacterRotator.objectToRotate = player.transform;
    }

    void CreateNewKart(GameObject kart, Vector3 position, Quaternion rotation)
    {
        GameObject newKart = Instantiate(kart, position, rotation);

        newKart.SetActive(true);
        newKart.name = "Kart";
        newKart.transform.parent = player.transform;

        AnimController.kartAnim = newKart.GetComponent<Animator>();
    }
    
    void KartChanged()
    {
        GameObject previousKart = player.transform.Find("Kart").gameObject;
        Vector3 previousPosition = previousKart.transform.position;
        Quaternion previousRotation = previousKart.transform.rotation;

        Destroy(previousKart);

        switch (kartDropdown.value)
        {
            case 0:
                //Cat Kart
                CreateNewKart(catKart, previousPosition, previousRotation);
                break;
            case 1:
                //Boy Kart
                CreateNewKart(boyKart, previousPosition, previousRotation);
                break;
            case 2:
                //Girl Kart
                CreateNewKart(girlKart, previousPosition, previousRotation);
                break;
        }
        CharacterRotator.objectToRotate = player.transform;
    }
}