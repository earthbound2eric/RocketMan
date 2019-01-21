using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{
    Rigidbody rocketBody;
    AudioSource rocketSound;
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 200f;
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
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f); // paramterise time
                break;
            default:
                SceneManager.LoadScene(0);
                break;
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!rocketSound.isPlaying)
            {
                rocketSound.Play();
            }
        }
        else
        {
            rocketSound.Stop();
        }
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
    private void Rotate()
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
