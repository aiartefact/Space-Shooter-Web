using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    public Boundary boundary;
    public float speed;
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    // Bolt speed increment
    private Mover mover;
    private GameObject tempObject;
    private float shotSpeedIncrement;
    public void ShotSpeedIncrement(float incrementStep)
    {
        shotSpeedIncrement += incrementStep;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // Shot speed Increment initialization
        shotSpeedIncrement = 0;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && (Time.time > nextFire) && !PauseManager.pauseManager.gamePaused)
        {
            nextFire = Time.time + fireRate;
            //  GameObject clone = 
            //Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
            tempObject = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            // Player shot speed increment
            mover = tempObject.GetComponent<Mover>();
            mover.speed += shotSpeedIncrement;

            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
