using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Axle class that defines what gose into our axles
[System.Serializable]
public class Axle
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
//Controllor class that moves the kart wheels
public class Controller : MonoBehaviour
{
    //List of axles and motor/steering values for kart
    public List<Axle> axles;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    // Water
    private float waterSlowdown = 1.0f;
    private const float onWater = 0.1f;

    // SpeedBooster
    private float speedBooster = 1.0f;
    private const float onSpeedBooster = 20.0f;

    //Fixed update that runs the Physics and input
    public void FixedUpdate()
    {
        //Get the motor and steering input WASD
        float motor = GetSpeed(Input.GetAxis("Vertical"));//W-Forwards, S-Backwords
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal"); //A-Left, D-Right
        //Foreach loop that runs through all the axles in the list
        foreach (Axle axle in axles)
        {
            //If motor is valid, move the wheels
            if (axle.motor)
            {
                axle.leftWheel.motorTorque = motor;
                axle.rightWheel.motorTorque = motor;
            }
            //If steering is valid, turn the wheels
            if (axle.steering)
            {
                axle.leftWheel.steerAngle = steering;
                axle.rightWheel.steerAngle = steering;
            }
        }
    }

    private float GetSpeed(float verticalAxis)
    {
        return maxMotorTorque * verticalAxis * waterSlowdown * speedBooster;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Water")
        {
            waterSlowdown = onWater;
        }
        if (collision.gameObject.name == "Terrain") {
            waterSlowdown = 1.0f;
            speedBooster = 1.0f;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        // Doesn't work with wheel colliders?
    }

    private void OnCollisionStay(Collision collision)
    {
        waterSlowdown = collision.collider.tag == "Water" ? onWater : 1.0f;
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedBooster")
        {
            speedBooster = onSpeedBooster;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SpeedBooster")
        {
            speedBooster = 1.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        speedBooster = other.tag == "SpeedBooster" ? onSpeedBooster : 1.0f;
    }
}