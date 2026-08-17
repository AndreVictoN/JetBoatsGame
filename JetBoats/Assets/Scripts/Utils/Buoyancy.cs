using System;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float waterLevel;
    [SerializeField] private float buoyancyForce;
    [SerializeField] private Transform[] buoyancyPoints;

    [SerializeField] private float waveAmplitude;
    [SerializeField] private float waveFrequency;
    [SerializeField] private float waveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach (Transform point in buoyancyPoints)
        {
            //float depth = waterLevel - point.position.y;

            float depth = GetCurrentWaveLevel(point) - point.position.y;

            if(depth > 0f)
            {
                float force = buoyancyForce * depth;
                rb.AddForceAtPosition(Vector3.up * force, point.position);
            }
        }
    }

    private float GetCurrentWaveLevel(Transform pos)
    {
        float y = pos.position.y;
        float x = pos.position.x;

        float waveSin = Mathf.Sin(x * waveFrequency - Time.time * waveSpeed);
        float level = waveSin * waveAmplitude - y;

        return level;
    }
}
