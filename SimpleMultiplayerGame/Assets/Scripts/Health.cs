using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {
    public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthBar;
    public bool destroyOnDeath;
    private GameObject[] spawnPoints;

    void Start () {
        if (isLocalPlayer)
        {
            spawnPoints =  GameObject.FindGameObjectsWithTag("NetworkStartPosition");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth = maxHealth;
            RpcRespawn();
        }
	}
    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;
                RpcRespawn();
            }
           
        }
    }
    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
    [ClientRpc]
    void RpcRespawn()
    {
        
       if (isLocalPlayer)
       {
         transform.position = new Vector3(0, 1000, 0);
         Vector3 spawnPoint  = Vector3.zero;
           
          if (spawnPoints != null && spawnPoints.Length > 0)
          {
 
               spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                // This line is for preventing players from spawning on each other
               spawnPoint = new Vector3(spawnPoint.x + Random.Range(-5,5), spawnPoint.y, spawnPoint.z + Random.Range(-5, 5));
              

           } 
            transform.position = spawnPoint;
            GetComponent<PlayerController>().cameraFollow.ResetCamera();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 2);
    }
}
