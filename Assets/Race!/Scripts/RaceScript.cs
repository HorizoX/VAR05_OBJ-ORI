using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceScript : MonoBehaviour
{

    public int courseLength;
    public int regularTerrainChance = 25;
    public int muddyTerrainChance = 12;
    public int finishLine;

    private string playerRunner;
    private string AIRunner = "A";

    private int playerRunnerPos = 0;
    private int AIRunnerPos = 0;

    private bool raceFinished = false;

    void Start()
    {
        // Prompt user to choose their runner character
        Debug.Log("Choose your runner character: ");
        //playerRunner = Console.ReadLine();

        StartCoroutine(RaceCoroutine());
    }

    IEnumerator RaceCoroutine()
    {
        while (!raceFinished)
        {
            yield return new WaitForSeconds(0.5f);
            MoveRunner(ref playerRunnerPos, regularTerrainChance, muddyTerrainChance);
            MoveRunner(ref AIRunnerPos, regularTerrainChance, muddyTerrainChance);

            if (playerRunnerPos >= finishLine || AIRunnerPos >= finishLine)
            {
                raceFinished = true;
                break;
            }
        }

        // Display results
        if (playerRunnerPos >= finishLine)
        {
            Debug.Log(playerRunner + " wins!");
        }
        else
        {
            Debug.Log(AIRunner + " wins!");
        }
    }

    private void MoveRunner(ref int runnerPos, int regularTerrainChance, int muddyTerrainChance)
    {
        int randomNum = Random.Range(0, 100);
        if (runnerPos % 5 == 0)
        {
            if (randomNum <= muddyTerrainChance)
            {
                runnerPos++;
            }
        }
        else
        {
            if (randomNum <= regularTerrainChance)
            {
                runnerPos++;
            }
        }
    }
}
