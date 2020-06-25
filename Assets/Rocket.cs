using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource rocketAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!rocketAudio.isPlaying)
            {
                rocketAudio.Play();
            }
        }
        else
        {
            rocketAudio.Stop();
        }
    }
    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
            print("Rotate left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
            print("Rotate right");
        }
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

}
