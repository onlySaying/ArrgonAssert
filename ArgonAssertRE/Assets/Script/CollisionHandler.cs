using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    private void OnTriggerEnter(Collider other)
    {
        StartCarshSequence();
    }

    void StartCarshSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled= false;
        GetComponent<PlayerController>().enabled= false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel",loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIdx);
    }
}
