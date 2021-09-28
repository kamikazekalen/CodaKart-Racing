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

        CharacterRotator.objectToRotate = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
