using UnityEngine;
using UnityEngine.Networking;
using System;

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn, MuzzleFlashSpawn;
    Rigidbody rb;
    public GameObject canon;
    [NonSerialized]public CameraFollow cameraFollow;
    private Movement playerMovement;
    public float bulletSpeed = 6;
    public GameObject MuzzleFlash;
    void Update()
    {
        if (!isLocalPlayer)
            return;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime *-3;

        playerMovement.Move(x,z);
        
        // Shooting
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdFire(this.gameObject);
        }
        playerMovement.RotateCanon(Input.mousePosition);
    }


    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer)
        {
            cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
            cameraFollow.SetPlayer(this.gameObject);
        }

        //GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
        playerMovement = GetComponent<Movement>();
        playerMovement.rb = GetComponent<Rigidbody>();
        playerMovement.canon = canon;
    }
    [Command]
    void CmdFire(GameObject shootingPlayer)
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        bullet.GetComponent<Bullet>().shootingPlayer = shootingPlayer;
        var muzzleFlashParticle = (GameObject)Instantiate(MuzzleFlash, MuzzleFlashSpawn.position, MuzzleFlashSpawn.rotation);
        NetworkServer.Spawn(muzzleFlashParticle);
        NetworkServer.Spawn(bullet);
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}