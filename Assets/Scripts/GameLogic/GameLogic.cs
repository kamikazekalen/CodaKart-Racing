using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    //Arays for the checkpoints
    public Transform[] checkpointArray;
    public static Transform[] checkpointA;

    //Current lap and checkpoint variables
    public static int currentCheckpoint, currentLap;

    public GameObject finishPanel;

    //Lap used for text display and max lap
    public int Lap;
    public int maxLaps = 3;

    //Starting position and the player
    public Vector3 startPos;
    public Quaternion startRotation;
    public static Transform player;

    //Total platers and the current player position as an int
    public int playerPosition = 1;
    public int playerCount = 1;

    //UI Text for the Timer Panel
    public Text lapText, lapTimerText, totalTimerText, startTimerText, positionTextUpper, positionTextLower;
    //UI Text for the Finish Panel
    public Text winnerNameText, returnTimerText;

    //Lap timer arrays for Minutes : Seconds . Milliseconds
    public float[] lapTime = { 0, 0, 0 };
    public float[] totalTime = { 0, 0, 0 };

    //Counting numbers for the lap and total timers
    public static float lapTimeCount, totalTimeCount;

    //Private Booleans
    private bool startTiming;
    private bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        //Set the current lap and checkpoint to 0 at start
        currentCheckpoint = 0;
        currentLap = 0;

        //Teleport player
        player.position = startPos;
        player.rotation = startRotation;
        
        //Set the playerposition and total players to the current player count
        positionTextUpper.text = playerPosition.ToString();
        positionTextLower.text = playerCount.ToString();

        //Start the setOff countdown coroutine
        StartCoroutine(SetOff());
    }

    //Steps for counting down from 3 and then unfreezing the player
    IEnumerator SetOff()
    {
        Controller kartScript = player.GetComponent<Controller>();
        kartScript.Freeze();
        yield return new WaitForSeconds(1.0f);
        startTimerText.text = "2";
        startTimerText.color = Color.red;
        yield return new WaitForSeconds(1.0f);
        startTimerText.text = "1";
        startTimerText.color = Color.yellow;
        yield return new WaitForSeconds(1.0f);
        startTiming = true;
        kartScript.Unfreeze();
        startTimerText.text = "GO!";
        startTimerText.color = Color.green;
        yield return new WaitForSeconds(1.0f);
        startTimerText.text = "";
    }

    IEnumerator FinishGame()
    {
        finish = true;
        totalTimeCount = 0.0f;

        startTimerText.text = "Finished!";
        yield return new WaitForSeconds(2.0f);

        startTimerText.text = "";
        finishPanel.SetActive(true);

        returnTimerText.text = "3";
        yield return new WaitForSeconds(1.0f);
        returnTimerText.text = "2";
        yield return new WaitForSeconds(1.0f);
        returnTimerText.text = "1";
        yield return new WaitForSeconds(1.0f);
        returnTimerText.text = "0";

        Destroy(player.gameObject);
        SceneManager.LoadScene("main menu");
    }

    // Update is called once per frame 
    void Update()
    {
        if (!finish)
        {
            TimerCount();
            LapLogic();

            //Update the player position text and the checkpointA array
            positionTextUpper.text = playerPosition.ToString();
            checkpointA = checkpointArray;
        }
    }

    private void LapLogic()
    {
        //If the current lap is not equal to the lap, We want to reset lapTimer
        if (currentLap != Lap)
        {
            //Set lap = currentlap so we can run checkpoints again
            Lap = currentLap;
            lapTimeCount = 0.0f;

            //If the lap is not the very first 0 lap, Then reset the lapcount

        }


        //If the lap is greater than maxLaps we want to stop the code
        if (Lap > maxLaps)
        {
            StartCoroutine(FinishGame());
        }

        //If the lap is the very first one, display it as 1 rather than 0. Else display it as lap
        if (Lap == 0)
        {
            lapText.text = "Lap " + (Lap + 1) + " of " + maxLaps;
        }
        else if (Lap <= maxLaps)
        {
            lapText.text = "Lap " + (Lap + 1) + " of " + maxLaps;
        }
    }

    private void TimerCount()
    {
        //If start timing is true do all the time counting
        if (startTiming)
        {
            //Increment both of the timers
            lapTimeCount += Time.deltaTime;
            totalTimeCount += Time.deltaTime;

            //Calculate the Minutes : Seconds . Milliseconds
            lapTime[0] = Mathf.Floor(lapTimeCount / 60.0f);
            lapTime[1] = Mathf.Floor(lapTimeCount) % 60;
            lapTime[2] = Mathf.Floor(lapTimeCount * 1000.0f) % 1000;

            //Calculate the Minutes : Seconds . Milliseconds
            totalTime[0] = Mathf.Floor(totalTimeCount / 60.0f);
            totalTime[1] = Mathf.Floor(totalTimeCount) % 60;
            totalTime[2] = Mathf.Floor(totalTimeCount * 1000.0f) % 1000;
        }

        //Display the Minutes : Seconds . Milliseconds for both timers
        lapTimerText.text = string.Format("{0:00}:{1:00}.{2:000}", lapTime[0], lapTime[1], lapTime[2]);
        totalTimerText.text = string.Format("{0:00}:{1:00}.{2:000}", totalTime[0], totalTime[1], totalTime[2]);
    }
}
