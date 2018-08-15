using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class MachListpanel : MonoBehaviour {
    [SerializeField]
    private JoinButton joinButtonPrefab;
	// Use this for initialization
	void Start () {
        AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
	}

    private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches)
    {
        ClaerExistingButtons();
        CreateNewJoingameButtons(matches);
    }

    private void CreateNewJoingameButtons(List<MatchInfoSnapshot> matches)
    {
        foreach(var match in matches)
        {
            var button = Instantiate(joinButtonPrefab);
            button.Initialize(match, transform);
        }
    }

    private void ClaerExistingButtons()
    {
        var buttons = GetComponentsInChildren<JoinButton>();
        foreach (var button in buttons)
            Destroy(button.gameObject);
    }
}
