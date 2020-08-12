using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond;
    public float highDegreesPerSecond = 8f;
    public float lowDegreesPerSecond = 5f;

    public float amplitude;
    private float highAmplitude = 0.1f;
    private float lowAmplitude = 0.05f;

    public float frequency;
    private float highFrequency = 1.3f;
    private float lowFrequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        degreesPerSecond = Random.Range(lowDegreesPerSecond, highDegreesPerSecond);
        frequency = Random.Range(lowFrequency, highFrequency);
        amplitude = Random.Range(lowAmplitude, highAmplitude);
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;


        transform.position = tempPos;

    }
}
