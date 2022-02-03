using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

    // Bolt speed increment
    private Mover mover;
    private GameObject tempObject;
    private float shotSpeedIncrement;
    public void ShotSpeedIncrement(float incrementStep)
    {
        shotSpeedIncrement += incrementStep;
    }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);		
	}

    void Fire ()
    {
        //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        tempObject = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

        // Enemy's shot speed increment
        mover = tempObject.GetComponent<Mover>();
        mover.speed += shotSpeedIncrement;

        audioSource.Play();
    }
}
