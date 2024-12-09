using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float RotationThrust = 100f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem rocketJetParticle;
    [SerializeField] ParticleSystem rocketJetLeftParticle;
    [SerializeField] ParticleSystem rocketJetRightParticle;
    Rigidbody rb;
    AudioSource audioSource;
     void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rocketJetParticle.Stop();
        rocketJetRightParticle.Stop();
        rocketJetLeftParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        processRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                
            }
             if(!rocketJetParticle.isPlaying)
            {
                rocketJetParticle.Play();                
            }
           
        }
        else
        {
            audioSource.Stop();
            rocketJetParticle.Stop();
        }
    }

    void processRotation()
    {
    if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(RotationThrust);
            if(!rocketJetLeftParticle.isPlaying)
            {
                rocketJetLeftParticle.Play();
            }
            
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-RotationThrust);
            if(!rocketJetRightParticle.isPlaying)
            {
                rocketJetRightParticle.Play();
            }
        }
        else
        {
            rocketJetRightParticle.Stop();
            rocketJetLeftParticle.Stop();
        }
    }

    void ApplyRotation(float RotationthisThrust)
    {
         transform.Rotate(Vector3.forward * Time.deltaTime * RotationthisThrust);
     }
}
