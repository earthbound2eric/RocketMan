using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketBody;
    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }
    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.left);
        }else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.right);
        }
    }
}
