using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private GameObject player;
    [SerializeField]private Vector3 offset = new Vector3(0, 50, 200);
    private Vector3 targetPos;
    private float screenWidth, screenHeight, journeyLength, movementStartTime = -1;
    public float speed = 2;
    private bool cameraMoving = false;
    void Start () {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
	}

	void LateUpdate () {
        if (player != null)
        {
            
            
            Vector3 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);
            if (screenPos.x<100 || screenWidth - screenPos.x < 100
                || screenPos.y< 100 || screenHeight - screenPos.y < 100)
            {
                    cameraMoving = true;
                    targetPos = new Vector3(player.transform.position.x, player.transform.position.y + offset.y,
                    player.transform.position.z + offset.z);
                    movementStartTime = Time.time;
                    journeyLength = Vector3.Distance(transform.position, targetPos);   
            }
        }
	}
    void Update()
    {
        if (cameraMoving)
        {
            float distCovered = (Time.time - movementStartTime) * speed;
            float journeyPercent = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetPos, journeyPercent);
            if (journeyPercent >= 1)
            {
                cameraMoving = false;
            }
        }
        
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
        ResetCamera();
    }
    public void ResetCamera()
    {
        targetPos = new Vector3(player.transform.position.x, player.transform.position.y + offset.y,
                   player.transform.position.z);
        transform.position = targetPos;
    }
}
