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

    //Fixed update that runs the Physics and input
    public void FixedUpdate()
    {
        //Get the motor and steering input WASD
        float motor = maxMotorTorque * Input.GetAxis("Vertical");//W-Forwards, S-Backwords
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
}