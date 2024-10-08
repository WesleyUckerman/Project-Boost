using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS - for tuning, tipicaly set in for editor
    //CACHE - e.g. reference for readability or speed
    //STATE - private intance (member) variables
    [SerializeField]float MainTrust = 0f;
    [SerializeField]float RotationTrust =0f;
    [SerializeField] AudioClip MainEngine ;
    [SerializeField] ParticleSystem MainEngineParticle ;
    Rigidbody Rb;
    AudioSource AS;
   
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       ProcessTrust(); 
       ProcessRotation();
    }

    void ProcessTrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }

        else
        {
            StopThrusting();
        }


    }

    private void StopThrusting()
    {
        AS.Stop();
        MainEngineParticle.Stop();
    }

    private void StartThrusting()
    {
        Rb.AddRelativeForce(Vector3.up * MainTrust * Time.deltaTime);
        if (!AS.isPlaying)
        {
            AS.PlayOneShot(MainEngine);
        }
        if (!MainEngineParticle.isPlaying)
        {
            MainEngineParticle.Play();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(RotationTrust);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-RotationTrust);
        }
    }

    private void ApplyRotation(float RotateThisFrame)
    {
        Rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotateThisFrame * Time.deltaTime);
        Rb.freezeRotation = false;
    }
}
