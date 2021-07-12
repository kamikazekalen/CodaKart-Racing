using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Require a rigidbody to run the code
[RequireComponent(typeof(Rigidbody))]
public class JumpScript : MonoBehaviour
{
    //Variables for grounded, force values, and rigidbody
    public bool isGrounded = false;
    public float jumpForce = 1500.0f;
    public Vector3 jumpValue = new Vector3(0.0f, 2.0f, 0.0f);
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        //Set the rigidbody to the componet rigidbody
        rigidBody = GetComponent<Rigidbody>();
    }

    //Function that is called when colliders hit another collider
    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    // Fixed Update is called once per frame. Best for physics
    void FixedUpdate()
    {
        // If space bar is pressed and is grounded is true
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Add a force to the car that ignores mass
            rigidBody.AddForce(jumpValue * jumpForce, ForceMode.Impulse);
        }
        isGrounded = false;
    }
}
