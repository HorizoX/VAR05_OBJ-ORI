using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceScript : MonoBehaviour
{
    // GameObject references for the player and AI runners, as well as the tiles that make up the course
    public GameObject playerRunner;
    public GameObject AIRunner;
    public GameObject[] courseTiles = new GameObject[12];

    // Chances for the runners to successfully move forward on regular terrain and muddy terrain, respectively
    public int regularTerrainChance = 25;
    public int muddyTerrainChance = 12;

    // Flag to determine if the race is finished
    private bool raceFinished = false;
    public bool isPlayer;

    // Start function, called when the scene starts
    //void Start()
    //{
    //    // Start the RaceCoroutine as soon as the scene starts
    //    StartCoroutine(RaceCoroutine());
    //}

    public void StartRace(bool player)
    {
        isPlayer = player;
        StartCoroutine(RaceCoroutine());
    }
    // Coroutine to handle the logic for the race
    public IEnumerator RaceCoroutine()
    {
        // Integers to keep track of the current tile for both the player and AI runners
        int AITile;
        int playerTile;

        // While the race is not finished
        while (!raceFinished && isPlayer)
        {
            // Wait for half a second
            yield return new WaitForSeconds(0.5f);

            // Move the player and AI runners
            MoveRunner(playerRunner, regularTerrainChance, muddyTerrainChance);
            MoveRunner(AIRunner, regularTerrainChance, muddyTerrainChance);

            // Determine the current tile for both the player and AI runners
            playerTile = Mathf.FloorToInt(playerRunner.transform.position.x);
            AITile = Mathf.FloorToInt(AIRunner.transform.position.x);

            // If either the player or AI runner reaches the finish line
            if (courseTiles[playerTile].tag == "FinishLine" || courseTiles[AITile].tag == "FinishLine")
            {
                // Set raceFinished to true to stop the loop
                raceFinished = true;
                break;
            }
        }

        // Determine the final tile for both the player and AI runners
        playerTile = Mathf.FloorToInt(playerRunner.transform.position.x);
        AITile = Mathf.FloorToInt(AIRunner.transform.position.x);

        // If the player is on the finish line tile, the player wins
        if (courseTiles[playerTile].tag == "FinishLine")
        {
            Debug.Log("Player wins!");
        }
        // Otherwise, the AI wins
        else
        {
            Debug.Log("AI wins!");
        }
    }

    private void MoveRunner(GameObject runner, int regularTerrainChance, int muddyTerrainChance)
    {
        Debug.Log("This is a debug log message.");
        // Generate a random number between 0 and 100
        int randomNum = Random.Range(0, 100);

        // Get the current tile number of the runner
        int currentTile = Mathf.FloorToInt(runner.transform.position.x);

        // Get the current tile object of the runner
        GameObject currentTileObject = courseTiles[currentTile];

        // If the current tile is muddy terrain
        if (currentTileObject.tag == "MuddyTerrain")
        {
            // If the random number is less than or equal to the muddy terrain chance
            if (randomNum <= muddyTerrainChance)
            {
                // Move the runner one tile ahead
                runner.transform.position = new Vector3(runner.transform.position.x + 1, runner.transform.position.y, runner.transform.position.z);
            }
        }
        // If the current tile is regular terrain
        else if (currentTileObject.tag == "GrassyTerrain")
        {
            // If the random number is less than or equal to the regular terrain chance
            if (randomNum <= regularTerrainChance)
            {
                // Move the runner one tile ahead
                runner.transform.position = new Vector3(runner.transform.position.x + 1, runner.transform.position.y, runner.transform.position.z);
            }
        }
    }
}
