using UnityEngine;
using UnityEngine.InputSystem;
public class movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 750f;
    [SerializeField] AudioClip engine;
    [SerializeField] ParticleSystem MainThrustParticles;
    [SerializeField] ParticleSystem LeftThrustParticles;
    [SerializeField] ParticleSystem RightThrustParticles;

    AudioSource SoundMovement;
    Rigidbody rb;
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();  
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody>();  
        SoundMovement = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        processThrust();
        processRotation();
    }

    private void processThrust()
    {
        if (thrust.IsPressed())
        {                 

            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!SoundMovement.isPlaying)
            {
                SoundMovement.PlayOneShot(engine);
            }
            MainThrustParticles.Play();
           
        }
        if (!thrust.IsPressed())
        {
            SoundMovement.Stop();
            MainThrustParticles.Stop();
        }

    }

    private void processRotation()
    {
        float rotationDirection = rotation.ReadValue<float>();
        if (rotationDirection > 0)
        {
            applyRotation(rotationDirection);
            LeftThrustParticles.Play();
        }
        else if (rotationDirection < 0)
        {
            applyRotation(rotationDirection);
            RightThrustParticles.Play();
        }

        if (rotationDirection == 0)
        {
            LeftThrustParticles.Stop();
            RightThrustParticles.Stop();
        }
    }

    private void applyRotation(float rotationDirection)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationStrength * rotationDirection *  -1);
        rb.freezeRotation = false;
    }
}
