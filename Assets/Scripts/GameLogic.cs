using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Transform[] checkpointArray;
    public static Transform[] checkpointA;

    public static int currentCheckpoint = 0;
    public static int currentLap = 0;

    public int Lap;
    public int maxLaps = 3;

    public Vector3 startPos;
    public Transform player;

    public int playerPosition = 1;
    public int playerCount = 1;

    public Text lapText;

    public Text lapTimerText;
    public Text totalTimerText;

    public Text startTimerText;

    public Text positionTextUpper;
    public Text positionTextLower;

    public float[] lapTime = { 0, 0, 0 };
    public float[] totalTime = { 0, 0, 0 };

    public static float lapTimeCount;
    public static float totalTimeCount;

    private bool startTiming;

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = 0;
        currentLap = 0;

        player.position = startPos;
        
        positionTextUpper.text = playerPosition.ToString();
        positionTextLower.text = playerCount.ToString();

        StartCoroutine(SetOff());
    }

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

    // Update is called once per frame 
    void Update()
    {
       if (startTiming)
        {
            lapTimeCount += Time.deltaTime;
            totalTimeCount += Time.deltaTime;

            lapTime[0] = Mathf.Floor(lapTimeCount / 60.0f);
            lapTime[1] = Mathf.Floor(lapTimeCount) % 60;
            lapTime[2] = Mathf.Floor(lapTimeCount * 1000.0f) % 1000;

            totalTime[0] = Mathf.Floor(totalTimeCount / 60.0f);
            totalTime[1] = Mathf.Floor(totalTimeCount) % 60;
            totalTime[2] = Mathf.Floor(totalTimeCount * 1000.0f) % 1000;
        }

        lapTimerText.text = string.Format("{0:00}:{1:00}.{2:000}", lapTime[0], lapTime[1], lapTime[2]);
        totalTimerText.text = string.Format("{0:00}:{1:00}.{2:000}", totalTime[0], totalTime[1], totalTime[2]);

        if (currentLap != Lap)
        {
            if (Lap != 0)
            {
                lapTimeCount = 0.0f;
            }
        }
        Lap = currentLap;

        if(Lap > maxLaps)
        {
            this.enabled = false;
        }

        if (Lap == 0)
        {
            lapText.text = "Lap " + (Lap + 1) + " of " + maxLaps;
        }
        else if (Lap <= maxLaps)
        {
            lapText.text = "Lap " + Lap + " of " + maxLaps;
        }

        positionTextUpper.text = playerPosition.ToString();
        checkpointA = checkpointArray;
    }
}
