using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Coll : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float LeavelLoadDelay = 2f;
    [SerializeField] AudioClip mainCrash;
    [SerializeField] AudioClip mainSucc;

    [SerializeField] ParticleSystem mainCrashParticles;
    [SerializeField] ParticleSystem mainSuccParticles;
    AudioSource audioSource;

    [SerializeField] GameObject portal;
    [SerializeField] GameObject portal2;

    bool isTrans;
    bool collDisable = false;


    public string question;
    public string correctAnswer;

    public GameObject questionPanel;
    public Text questionText;
    public InputField answerInput;
    Move move;
    void Start()
    {
        move = player.GetComponent<Move>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        NextLevel();
    }

    void NextLevel()
    {
        if (Input.GetKey(KeyCode.L))
        {
            StartCoroutine(LoadNextLevel());
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collDisable = !collDisable;
        }
    }
    



     void OnCollisionEnter(Collision other)
    {
        if (isTrans || collDisable) { return; };
        
        switch (other.gameObject.tag) 
        {
            case "friendly":
                Debug.Log("friendly");
                break;   
            case "Finish":
                Debug.Log("wp");
                StartWinSequence();
                break;
            case "portal":
                Debug.Log("teleporting");
                AskQuestion();
                break;
            case "portal2":
                Debug.Log("teleported");
                break;
            default:
                StartCrashSequence();
                break;
        }

     }

    private void AskQuestion()
    {
        move.enabled = false;
        questionPanel.SetActive(true);
        questionText.text = question;
        answerInput.text = "";
        
        answerInput.ActivateInputField();
        answerInput.Select();
    }
    public void CheckAnswer()
    {
        string playerAnswer = answerInput.text.Trim();

        if (playerAnswer.ToLower() == correctAnswer.ToLower())
        {
            StartTeleporting();
        }
        else
        {
            // Incorrect answer handling
            ResetQuestion();
        }
    }
    void StartTeleporting()
    {

        Vector3 newPosition = portal2.transform.position;
        newPosition.y += 3f;
        player.position = newPosition;
        ResetQuestion();
    }

    private void ResetQuestion()
    {
        move.enabled = true;
        questionPanel.SetActive(false);
        
    }
    void StartCrashSequence()
        {
            audioSource.Stop();
            isTrans = true;
            
            GetComponent<Move>().enabled = false;
            
            StartCoroutine(ReloadLevel());
            audioSource.PlayOneShot(mainCrash);
            mainCrashParticles.Play();
        }
        
        void StartWinSequence()
        {
            audioSource.Stop();
            isTrans = true;
            
            GetComponent<Move>().enabled = false;
            
            StartCoroutine(LoadNextLevel());
            audioSource.PlayOneShot(mainSucc);
            mainSuccParticles.Play();
        }




        IEnumerator ReloadLevel() 
        { 
            yield return new WaitForSeconds(LeavelLoadDelay);
            
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }



        
    
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(LeavelLoadDelay);
        int NextLevel = SceneManager.GetActiveScene().buildIndex;
        int NextSecene = NextLevel + 1;
        if (NextSecene == SceneManager.sceneCountInBuildSettings)
        {
            NextSecene = 0;
        }
        SceneManager.LoadScene(NextSecene);


    }


}
