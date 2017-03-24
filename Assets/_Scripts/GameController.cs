﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameController : MonoBehaviour {


    public GameObject myCarPrefab;

    public GameObject opponentPrefab;

    private bool _multiplayerReady;
    private string _myParticipantId;
    private Vector2 _startingPoint = new Vector2(-7.28f, 0f);
    private Dictionary<string, OpponentController> _opponentScripts;

    void Start() {
        SetupMultiplayerGame();
    }

    void SetupMultiplayerGame()
    {
        // TODO: Fill this out!
        // 1
        _myParticipantId = MultiplayerController.Instance.GetMyParticipantId();
        // 2
        List<Participant> allPlayers = MultiplayerController.Instance.GetAllPlayers();
        _opponentScripts = new Dictionary<string, OpponentController>(allPlayers.Count - 1);
        for (int i = 0; i < allPlayers.Count; i++)
        {
            string nextParticipantId = allPlayers[i].ParticipantId;
            Debug.Log("Setting up car for " + nextParticipantId);
            // 3
            Vector3 carStartPoint = Vector3.zero;
            if (i == 0) carStartPoint = new Vector3(_startingPoint.x, _startingPoint.y, 0);
            else if (i == 1) carStartPoint = new Vector3(_startingPoint.x * -1, _startingPoint.y, 0);

            if (nextParticipantId == _myParticipantId)
            {
                // 4
                GameObject myCar = (Instantiate(myCarPrefab, carStartPoint, Quaternion.identity) as GameObject);
                myCar.GetComponent<PlayerController>().SetCarChoice(i + 1, true);
                //myCar.transform.position = carStartPoint;
            }
            else
            {
                // 5
                GameObject opponentCar = (Instantiate(opponentPrefab, carStartPoint, Quaternion.identity) as GameObject);
                OpponentController opponentScript = opponentCar.GetComponent<OpponentController>();
                opponentScript.SetCarNumber(i + 1);
                // 6
                _opponentScripts[nextParticipantId] = opponentScript;
            }
        }
        // 7
        //_lapsRemaining = 3;
        //_timePlayed = 0;
        //guiObject.SetLaps(_lapsRemaining);
        //guiObject.SetTime(_timePlayed);
        //_multiplayerReady = true;
    }
}