using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandling : MonoBehaviour
{
    [SerializeField] float LevelDelay = 3f;
    [SerializeField] AudioClip finishSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource SoundCollision;

    bool isMoving = true;

    private void Start()
    {
        SoundCollision = GetComponent<AudioSource>();

    }

    private void Update()
    {
      RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNewLevel();
        }
    }

    private void OnCollisionEnter(Collision otherObject)
    {
        string objType = otherObject.gameObject.tag;

        if (!isMoving)
        { 
            return;
        }

        switch (objType)
        {
            case "Finish":
                Progress();
                break;
            case "Respawn":
                break;
            default:
                Crash();
                break;
        }
    }

    void Progress()
    {    
        isMoving = false;
        GetComponent<movement>().enabled = false;
        Invoke(nameof(LoadNewLevel), LevelDelay);
        SoundCollision.Stop();
        SoundCollision.PlayOneShot(finishSound);
        finishParticles.Play();
    }

     void Crash()
    {
        
        isMoving = false;
        GetComponent<movement>().enabled = false;
        Invoke(nameof(ReloadLevel), LevelDelay);
        SoundCollision.Stop();
        SoundCollision.PlayOneShot(crashSound);      
        crashParticles.Play();
    }

    void LoadNewLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
      
        int newLevel = level + 1;

        if (newLevel == SceneManager.sceneCountInBuildSettings)
        {
            newLevel = 0;
        }
        
        SceneManager.LoadScene(newLevel);
        isMoving = true;
    }

    void ReloadLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
        isMoving = true;
    }
}
