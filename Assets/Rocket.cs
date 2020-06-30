using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;
    
    //Sounds
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Finish;

    //Particles
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem FinishParticle;
    
    //Memeber Variable
    Rigidbody rigidBody;
    AudioSource rocketAudio;

    bool collisionDisabled = false;

    enum State {Alive,Dying,Transcend }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            Rotate();
            Thrust();
        }
        if (Debug.isDebugBuild)
        {
            respondToDebugKeys();
        }

    }

    private void respondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextlevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionDisabled)
       {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;

            case "Finish":
                LoadNextLevel();
                break;

            default:
                RestartGame();
                break;
        }
    }

    private void RestartGame()
    {
        print("DEAD");
        state = State.Dying;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(Death);
        DeathParticle.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        print("Finish");
        state = State.Transcend;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(Finish);
        FinishParticle.Play();
        Invoke("LoadNextlevel",levelLoadDelay);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextlevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            rocketAudio.Stop();
            mainEngineParticle.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!rocketAudio.isPlaying)
        {
            rocketAudio.PlayOneShot(mainEngine);
        }
        mainEngineParticle.Play();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

}
