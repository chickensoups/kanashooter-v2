﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BulletTypeScript : MonoBehaviour
{

    public GameObject BulletTypePrefabs;
    public List<string> answers = new List<string>();

    private static BulletTypeScript _instance;

    public static BulletTypeScript instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        answers = LevelUtils.currentLevel.GetAnswers();
	    for (int i = 0; i < answers.Count; i++)
	    {
	        GameObject BulletType = Instantiate(BulletTypePrefabs, Vector3.zero, Quaternion.identity) as GameObject;
            BulletType.transform.SetParent(transform);
            BulletType.transform.localScale = new Vector3(1, 1);
	        BulletType.name = answers[i];
	        BulletType.GetComponentInChildren<Text>().text = answers[i];
	    }
	    GameObject[] BulletTypeButtons = GameObject.FindGameObjectsWithTag("BulletType");
	    for (int i = 0; i < BulletTypeButtons.Length; i++)
	    {
	        Button btn = BulletTypeButtons[i].GetComponent<Button>();
            btn.onClick.AddListener(ChooseBulletType);

	    }
	}

    private void ChooseBulletType()
    {
        string bulletTypeString = EventSystem.current.currentSelectedGameObject.name;
        Done_PlayerController.currentBulletType = bulletTypeString;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
