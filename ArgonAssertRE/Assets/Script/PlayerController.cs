using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Setting")]
    [Tooltip ("비행기 객체의 상우좌하 이동속도 조절")][SerializeField] float speed = 5f;
    [SerializeField] float minxRange = -14f;
    [SerializeField] float maxXRange = 16f;
    [SerializeField] float minyRange = -5f;
    [SerializeField] float maxyRange = 20f;

    float xThrow;
    float yThrow;
    
    [Header("Screen Position based turning")]
    [SerializeField] float positionpitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = -5f;
    [SerializeField] float controlRollFactor = 20f;

    [Header("laser Array")]
    [SerializeField] GameObject[] lasers;
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetActiveLasers(true);
        }
        else
        {
            SetActiveLasers(false);
        }
    }

    void SetActiveLasers(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

     /*void ActiveLasers()
     {
        foreach(GameObject laser in lasers) 
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = true;
        }
     }

    void DeactiveLasers()
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = false;
        }
    }*/

    void ProcessRotation() 
    {
        float pitchDueToPositon = transform.localPosition.y * positionpitchFactor;
        float pitchDueToContolThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPositon + pitchDueToContolThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");

        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * speed;
        float yOffest = yThrow * Time.deltaTime * speed;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffest;

        float clampedXpos = Mathf.Clamp(rawXPos, minxRange, maxXRange);
        float clampedYpos = Mathf.Clamp(rawYPos, minyRange, maxyRange);

        transform.localPosition = new Vector3
            (clampedXpos, clampedYpos, transform.localPosition.z);
    }
}
