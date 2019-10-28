using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject shipPrefab;
    public Transform shipStartPosition; 
    public GameObject currentShip { get; private set; }
    public GameObject spaceStationPrefab;
    public Transform spaceStationStartPosition;
    public GameObject currentSpaceStation { get; private set; }
    public SmoothFollow cameraFollow;
    public Boundary boundary;

    public GameObject inGameUI;
    public GameObject pausedUI;
    public GameObject gameOverUI;
    public GameObject mainMenuUI;
    public GameObject warningUI;
    public bool gameIsPlaying { get; private set; }
    public AsteroidSpawner asteroidSpawner;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }
    void ShowUI(GameObject newUI)
    {
        GameObject[] allUI = { inGameUI, pausedUI, gameOverUI, mainMenuUI };
        foreach (GameObject UIToHide in allUI)
        {
            UIToHide.SetActive(false);
        }
        newUI.SetActive(true);
    }
    public void ShowMainMenu()
    {
        ShowUI(mainMenuUI);
        gameIsPlaying = false;
        asteroidSpawner.spawnAsteroids = false;
    }

    public void StartGame()
    {
        ShowUI(inGameUI);
        gameIsPlaying = true;
        if (currentShip != null)
        {
            Destroy(currentShip);
        }
        if (currentSpaceStation != null)
        { 
            Destroy(currentSpaceStation);
        }
        currentShip = Instantiate(shipPrefab);
        currentShip.transform.position = shipStartPosition.position;
        currentShip.transform.rotation = shipStartPosition.rotation;
        currentSpaceStation = Instantiate(spaceStationPrefab);
        currentSpaceStation.transform.position = spaceStationStartPosition.position;
        currentSpaceStation.transform.rotation = spaceStationStartPosition.rotation;
        cameraFollow.target = currentShip.transform;
        asteroidSpawner.spawnAsteroids = true;
        asteroidSpawner.target = currentSpaceStation.transform;
    }
    public void GameOver()
    {
        ShowUI(gameOverUI);
        gameIsPlaying = false;
        if (currentShip != null)
            Destroy(currentShip);
        if (currentSpaceStation != null)
            Destroy(currentSpaceStation);
        warningUI.SetActive(false);
        asteroidSpawner.spawnAsteroids = false;
        asteroidSpawner.DestroyAllAsteroids();
    }
    public void SetPaused(bool paused)
    {
        inGameUI.SetActive(!paused);
        pausedUI.SetActive(paused);
        if (paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Update()
    {
        if (currentShip == null)
            return;
        float distance = (currentShip.transform.position - boundary.transform.position).magnitude;
        if (distance > boundary.destroyRadius)
        {
            GameOver();
        }
        else if (distance > boundary.warningRadius)
        {
            warningUI.SetActive(true);
        }
        else
        {
            warningUI.SetActive(false);
        }
    }
}
