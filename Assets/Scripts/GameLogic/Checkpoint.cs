using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //Code that runs when something enters the Trigger checkpoint collider
    void OnTriggerEnter(Collider other)
    {
        //If it is not the player that triggers it, Skip the code
        if (!other.CompareTag("Player"))
        {
            return;
        }

        //If the position of this checkpoint is the currentCheckpoint for player, Then run increment code
        if(transform == GameLogic.checkpointA[GameLogic.currentCheckpoint].transform)
        {
            //Check if it is the last checpoint in the list or not
            if (GameLogic.currentCheckpoint + 1 < GameLogic.checkpointA.Length)
            {
                //Increase checkpoint if it is not last checkpoint
                GameLogic.currentCheckpoint++;
            }
            else
            {
                //Reset checkpoint to 0 if last check point is reached
                GameLogic.currentCheckpoint = 0;
                GameLogic.currentLap++;
            }
        }
    }
}
