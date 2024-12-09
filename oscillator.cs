using UnityEngine;

public class oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 MovementVector;

    float movementFactor;
    [SerializeField] float period = 2f;
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon  ){return;}
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor =  (rawSinWave + 1f) / 2f;

        Vector3 offset = MovementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
