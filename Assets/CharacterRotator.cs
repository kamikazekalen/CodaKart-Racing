using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    public float secondsPerRotation = 6.0f;
    public static Transform objectToRotate;
    // Start is called before the first frame update
    void Start()
    {
        objectToRotate.Rotate(0, 6.0f * (2.5f * secondsPerRotation) * Time.deltaTime, 0);
    }
}
