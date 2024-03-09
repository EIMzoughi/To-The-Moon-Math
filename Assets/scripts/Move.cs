using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audiosource;
    [SerializeField] float speedUp;
    [SerializeField] float Rot;
    [SerializeField] AudioClip mainEng;

    [SerializeField] ParticleSystem mainEngParticals;
    [SerializeField] ParticleSystem leftSideParticals;
    [SerializeField] ParticleSystem rightSideParticals;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        
    }
    
    
    void ProcessThrust()
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



    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * speedUp * Time.deltaTime);
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(mainEng);
        }
        if (!mainEngParticals.isPlaying)
        {
            mainEngParticals.Play();
        }
    }
    private void StopThrusting()
    {
        audiosource.Stop();
        mainEngParticals.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }

    }

    private void AppRot(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }



    private void RotateLeft()
    {
        AppRot(Rot);
        if (!rightSideParticals.isPlaying)
        {
            rightSideParticals.Play();
        }
    }
    
    private void RotateRight()
    {
        AppRot(-Rot);
        if (!leftSideParticals.isPlaying)
        {
            leftSideParticals.Play();
        }
    }

    
    private void StopRotating()
    {
        rightSideParticals.Stop();
        leftSideParticals.Stop();
    }

 
}
