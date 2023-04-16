using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject ExplosiontVFXe;
    [SerializeField] GameObject HitVFXe;
    GameObject parents;

    ScoreBoard scoreBoard;
    [SerializeField] int scorePerHit = 150;

    [SerializeField] int hitpoints = 2;

    Rigidbody rb;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parents = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    void AddRigidBody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("hit Particle1");
        ProcessHit();
        Debug.Log("hit Particle2");
        if (hitpoints <= 0)
        {
            Debug.Log("kill");
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitpoints--;
        GameObject vfx = Instantiate(HitVFXe, transform.position, Quaternion.identity);
        vfx.transform.parent = parents.transform; 
       
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(ExplosiontVFXe, transform.position, Quaternion.identity);
        vfx.transform.parent = parents.transform;
        scoreBoard.IncreaseScore(scorePerHit);
        Destroy(gameObject);
    }
}
