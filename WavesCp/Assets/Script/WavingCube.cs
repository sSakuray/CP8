using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavingCube : MonoBehaviour
{
    private Rigidbody rb;
    private BlockWave wavePlane;
    private Vector3 originalPosition;
    private float waveSpeed = 2f;  
    private float waveHeight = 2f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wavePlane = FindObjectOfType<BlockWave>();
        originalPosition = transform.position;
    }

    void Update()
    {
        float surfaceHeight = wavePlane.GetSurfaceHeight(transform.position);
        Vector3 newPosition = new Vector3(originalPosition.x, surfaceHeight, originalPosition.z);
        rb.MovePosition(Vector3.Lerp(transform.position, newPosition, Time.deltaTime * waveSpeed));
        float sway = Mathf.Sin(Time.time * waveSpeed) * 0.1f;
        rb.MovePosition(new Vector3(newPosition.x + sway, newPosition.y, newPosition.z));
    }
}