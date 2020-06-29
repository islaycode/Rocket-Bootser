using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // It won't let you add more than one script on a single object.
public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 MovementVector = new Vector3(10F,10F,10F); //Vector 3 is position in Transform
    [SerializeField] float period = 2f;
    [Range(0, 1)] [SerializeField] float movementFactor; // 0 for not moved , 1 for moved.

    // Start is called before the first frame update
    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;  //For Storing position
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) // Protect against period is zero
        {
            return;
        }
        float cycles = Time.time / period; // grows contiually from 0

        const float  tau = Mathf.PI * 2;
        float rawSinwave = Mathf.Sin(cycles * tau);
        print(rawSinwave);

        movementFactor = rawSinwave / 2f + 0.5f;
        Vector3 offset = movementFactor * MovementVector;
        transform.position = startingPos + offset;
    }
}
