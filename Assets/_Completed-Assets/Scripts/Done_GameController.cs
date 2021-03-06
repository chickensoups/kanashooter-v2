﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float startWait;
    public GameObject LevelUpPanel, GameoverPanel;
	
    public int index;
    private string name;
    private string welcomeMessage;
    public List<string> questions;
    public List<string> answers;
    private int enemyEachWaveCount;
    private float waveWait;
    private float spawnWait;
    private bool isRotate;
    private bool isFaster;
    public bool isStop = false;

    public static int levelNumber;

    private static Done_GameController _instance;

    public GameObject progressbar, healthbar;

    public Level levelData;


    public static Done_GameController instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }

    public void Start ()
    {
        levelData = LevelUtils.currentLevel;
        index = levelData.GetIndex();
        name = levelData.GetName();
        welcomeMessage = levelData.GetWelcomeMessage();
        answers = levelData.GetAnswers();
        questions = levelData.GetQuestions();
        enemyEachWaveCount = levelData.GetEnemyEachWaveCount();
        waveWait = levelData.GetWaveWait();
        spawnWait = levelData.GetSpawnWait();
        isRotate = levelData.IsRotate();
        isFaster = levelData.IsFaster();

        StartCoroutine (SpawnWaves ());
        StartCoroutine(RegenHealthbar());
    }

    void Update ()
	{
	    if (isStop)
	    {
            return;
	    }
	    if (healthbar.GetComponent<HealthbarController>().GetCurrentHealth() <= 0)
	    {
            GameoverPanel.SetActive(true);
            GameoverPanel.GetComponent<GameoverDialog>().ShowData();
            isStop = true;
            StopCoroutine(SpawnWaves());
            StopCoroutine(RegenHealthbar());
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
        }
	    if (progressbar.GetComponent<ProgressbarController>().GetCurrentPoint() >= progressbar.GetComponent<ProgressbarController>().GetWinPoint3())
	    {
            isStop = true;
	        if (PlayerDataUtils.playerData.highestLevelUnlocked <= LevelUtils.currentLevel.GetIndex())
	        {
	            PlayerDataUtils.playerData.highestLevelUnlocked = LevelUtils.currentLevel.GetIndex() + 1;

	        }
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemy.Length; i++)
            {
                Destroy(enemy[i]);
            }
            PlayerDataUtils.saveData();
            LevelUpPanel.SetActive(true);
            LevelUpPanel.GetComponent<LevelUpDialog>().ShowData();
            StopCoroutine(SpawnWaves());
            StopCoroutine(RegenHealthbar());
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
        }
    }

    public IEnumerator RegenHealthbar()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            healthbar.GetComponent<HealthbarController>().regenHealth();
            yield return new WaitForSeconds(1);
            if (isStop)
            {
                break;
            }

        }
    }
	
	public IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject enemy = Instantiate (hazard, spawnPosition, spawnRotation) as GameObject;
			    int randomIndex = Random.Range(0, questions.Count);
                enemy.GetComponentInChildren<TextMesh>().text = questions[randomIndex];
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

		    if (isStop)
		    {
		        break;
		    }
		}
	}
	

    public void OnApplicationQuit()
    {
        PlayerDataUtils.saveData();
    }

    public void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            PlayerDataUtils.saveData();
        }
    }
}