// MyNetworkManager
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MyNetworkManager : MonoBehaviour
{
	private static MyNetworkManager _instance;
    public static MyNetworkManager Instance
    {
        get
        {
            if ((UnityEngine.Object)_instance == (UnityEngine.Object)null)
            {
                GameObject gameObject = new GameObject("My Network Manager");
                gameObject.AddComponent<MyNetworkManager>();
            }
            return _instance;
        }
    }
    [NonSerialized]
	public bool isAtStartup = true;

	private NetworkClient myClient;

	private float nextRefreshTime = 0f;

	public InputField matchNameInput;

	public InputField roomSizeInput;

	public GameObject errortext;

	public GameObject matchMakerPanel;

	

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void StartHosting()
	{
		if (matchNameInput.text != "" && roomSizeInput.text != "")
		{
			string text = matchNameInput.text;
			uint matchSize = uint.Parse(roomSizeInput.text);
            if (matchSize > 10) matchSize = 10;
			NetworkManager.singleton.StartMatchMaker();
			NetworkManager.singleton.matchMaker.CreateMatch(text, matchSize, true, "", "", "", 0, 0, OnMatchCreated);
			errortext.SetActive(false);
			matchMakerPanel.SetActive(false);
		}
		else
		{
			errortext.SetActive(true);
		}
	}

	private void OnMatchCreated(bool success, string extendedInfo, MatchInfo responseData)
	{
		NetworkManager.singleton.StartHost(responseData);
	}

	private void Update()
	{
		if (Time.time >= nextRefreshTime)
		{
			RefreshMatches();
		}
	}

	public void RefreshMatches()
	{
		nextRefreshTime = Time.time + 60f;
		if (NetworkManager.singleton.matchMaker == null)
		{
			NetworkManager.singleton.StartMatchMaker();
		}
		NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", false, 0, 0, HandleListMatchesCompelete);
	}

	private void HandleListMatchesCompelete(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
	{
		Debug.Log(success);
		AvailableMatchesList.HandleNewMatchList(responseData);
	}

	public void JoinMatch(MatchInfoSnapshot match)
	{
		if ((UnityEngine.Object)NetworkManager.singleton.matchMaker == (UnityEngine.Object)null)
		{
			NetworkManager.singleton.StartMatchMaker();
		}
		NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
	}

	private void OnMatchJoined(bool success, string extendedInfo, MatchInfo responseData)
	{
        
		if (success)
		{
            Debug.Log("Joined the match successfuly");
            matchMakerPanel.SetActive(false);
            NetworkManager.singleton.StartClient(responseData);
        }
        else
        {
            Debug.Log("could not join the match");
        }
	}
}
