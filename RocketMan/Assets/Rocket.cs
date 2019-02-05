using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{
    Rigidbody rocketBody;
    AudioSource rocketSound;
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip levelLoad;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem levelLoadParticle;
    enum State {Alive, Dying, Transcending};
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            // TODO: stop sound on death
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        { return; } // dead
            switch (collision.gameObject.tag)
            {
                case "Finish":
                    state = State.Transcending;
                    Invoke("LoadNextScene", 1f); // paramterise time
                    rocketSound.PlayOneShot(levelLoad);
                    levelLoadParticle.Play();
                    break;
                case "Friendly":
                    //do nothing
                    break;
                default:
                    rocketSound.Stop();
                    state = State.Dying;  
                    Invoke("dying", 1f);
                    rocketSound.PlayOneShot(death);
                    deathParticle.Play();
                    break;
            }
        
    }
    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            rocketSound.Stop();
            mainEngineParticle.Stop();
        }
    }

    private void ApplyThrust()
    {
        if (state == State.Alive)
        {
            rocketBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!rocketSound.isPlaying)
            {
                mainEngineParticle.Play();
                rocketSound.PlayOneShot(mainEngine);
            }
        }
    }

    private void dying()
    {
        SceneManager.LoadScene(0);
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
    private void RespondToRotateInput()
    {
        rocketBody.freezeRotation = true;
        float rotationSpeed = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.left * rotationSpeed);
        }else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.right * rotationSpeed);
        }
        rocketBody.freezeRotation = false;
    }
}
