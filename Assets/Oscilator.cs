using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // It won't let you add more than one script on a single object.
public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 MovementVector; //Vector 3 is position in Transform

    [Range(0, 1)] [SerializeField] float movementFactor; // 0 for not moved , 1 for moved.

    // Start is called before the first frame update
    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementFactor * MovementVector;
        transform.position = startingPos + offset;
    }
}
