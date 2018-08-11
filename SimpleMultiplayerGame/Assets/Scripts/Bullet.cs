using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bullet : NetworkBehaviour
{
    public GameObject explosionParticle;
    public GameObject shootingPlayer;
    public int damage = 20;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != shootingPlayer)
        {
            SpawnParticle();
            var hit = collision.gameObject;
            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(20);
            }
            Destroy(gameObject);
        }
       
    }
    
    void SpawnParticle()
    {
        if (isServer)
        {
            var muzzleFlashParticle = (GameObject)Instantiate(explosionParticle, transform.position, transform.rotation);
            NetworkServer.Spawn(muzzleFlashParticle);
        }
        
    }
}