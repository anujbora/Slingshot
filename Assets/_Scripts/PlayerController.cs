﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float vertSpeed;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire;

    private Touch touch;

    public Material[] spaceCraftMaterials;

    public int myTurn;

    //private GameObject gameManager;
    private GameController gameController;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)    //check for the first touch
            {
                //do nothing
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (gameController.getPlayerTurn() == myTurn)
                {
                    transform.Rotate(Vector3.forward * touch.deltaPosition.y * speed * Time.deltaTime);
                }

            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                //do nothing
            }

            else
            {
                if (touch.position.x > (Screen.width / 2))
                {
                    if (gameController.getPlayerTurn() == myTurn)
                    {

                        if (Time.time > nextFire)
                        {
                            nextFire = Time.time + fireRate;
                            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                            try
                            {
                                gameController.DoShotUpdate(shotSpawn.position, shotSpawn.rotation);
                            }
                            catch (Exception e) {
                                Debug.Log("Exception " + e.ToString());
                            }
                            gameController.setPlayerTurn(1 - myTurn);
                        }
                    }
                }
            }
                
        }

        if (Input.GetKey ("up")) {
			//print ("up arrow key is held down");
			transform.Rotate(Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKey ("down")) {
			//print ("down arrow key is held down");
			transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKey ("w")) {
			//print ("up arrow key is held down");
			transform.Translate(Vector3.up * vertSpeed * Time.deltaTime, Space.World);
		}

		if (Input.GetKey ("s")) {
			//print ("down arrow key is held down");
			transform.Translate(-Vector3.up * vertSpeed * Time.deltaTime, Space.World);
		}

		if (Input.GetButton("Jump") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
    }

    public void SetCarChoice(int carNum, bool isMultiplayer)
    {
        transform.FindChild("Player").GetComponent<Renderer>().material = spaceCraftMaterials[carNum - 1];
    }

    public void SetMyTurn(int i) {
        myTurn = i;
    }

}
