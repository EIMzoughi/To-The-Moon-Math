using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    
    Vector3 StartingPos;
    float MovementFac;
    [SerializeField] Vector3 MovementVec;
    [SerializeField] float period = 2f;



    // Start is called before the first frame update
    void Start()
    {
        StartingPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period <=Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float RawSinWave = Mathf.Sin(cycles*tau);

        MovementFac = (RawSinWave + 1f) / 2f;



        Vector3 offset = MovementVec * MovementFac;
        transform.position = StartingPos + offset;
    }
}
