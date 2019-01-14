using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketBody;
    AudioSource rocketSound;
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 200f;
    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Fuel":
                //do something
                break;
            default:
                //die
                break;
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!rocketSound.isPlaying)
            {
                rocketSound.Play();
            }
        }
        else
        {
            rocketSound.Stop();
        }
    }
    private void Rotate()
    {
        rocketBody.freezeRotation = true;
        float rotationSpeed = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.left * rotationSpeed);
        }else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.right * rotationSpeed);
        }
        rocketBody.freezeRotation = false;
    }
}
