
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class JoinButton : MonoBehaviour {
    private Text buttonText;
    MatchInfoSnapshot match;
    Transform panelTransform;
    // Use this for initialization
    void Start () {
        buttonText = GetComponentInChildren<Text>();
        GetComponent<Button>().onClick.AddListener(JoinMatch);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void IntializeInvoke()
    {
        buttonText.text = match.name + "("+match.currentSize+"/"+match.maxSize+") Players";
        transform.SetParent(panelTransform);
        transform.localRotation = Quaternion.identity;
        transform.position = Vector3.zero;
    }
    public void Initialize(MatchInfoSnapshot inMatch, Transform inPanelTransform)
    {
        match = inMatch;
        panelTransform = inPanelTransform;
        Invoke("IntializeInvoke", 1f);
    }
    public void JoinMatch()
    {
        
        MyNetworkManager.Instance.JoinMatch(match);
    }
}
