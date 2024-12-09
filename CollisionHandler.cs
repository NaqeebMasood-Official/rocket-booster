using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float DelayInLoading = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem destroyParticle;
    // [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        destroyParticle.Stop();
    }

    void Update()
    {
        ResponndToDebugKeys();
    }

    void ResponndToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled){ return; }
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This Thing is Friendly");
                    break;
                case "Finish": 
                    StartSuccessSequence();
                    break;
                default:
                    Debug.Log("You blew Up");
                    StartCrashSequence();
                    break;
            }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        
        audioSource.PlayOneShot(deathSFX);
        destroyParticle.Play();
        Invoke("ReloadLevel", DelayInLoading);
        GetComponent<Movement>().enabled = false;
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        Invoke("LoadNextLevel", DelayInLoading);
        GetComponent<Movement>().enabled = false;
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
