using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float movementFactor;
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
  Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period != 0f)
        {
            float cycles = Time.time / period;
            const float tau = Mathf.PI * 2f;
            float rawSinWave = Mathf.Sin(cycles * tau);
            movementFactor = rawSinWave;
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
    }
}
