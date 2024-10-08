using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float DelayTime = 2f;
    [SerializeField] AudioClip Explosion;
    [SerializeField] AudioClip Finish;
    [SerializeField] ParticleSystem ExplosionParticle;
    [SerializeField] ParticleSystem FinishParticle;

    AudioSource Audio;


    bool IsTransitioning = false;
    bool CollisionDisabled = false;
    void Start() 
    {
        Audio = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        cheats();
    }
    void OnCollisionEnter(Collision other) 
    {
        if(IsTransitioning || CollisionDisabled){return;}
        
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                case "Finish":
                    StartFinishSequence();
                    break;
                case "Fuel":
                    Debug.Log("You picked up fuel");
                    break;
                default:
                    StartCrashSequence();
                    
                    break;

            }
        
    }

    void StartCrashSequence()
    {
        IsTransitioning = true;
        Audio.Stop();
        GetComponent<Movement>().enabled = false;
        Audio.PlayOneShot(Explosion);
        ExplosionParticle.Play();
        Invoke ("ReloadScene", DelayTime);
    }

    void StartFinishSequence()
    {
        IsTransitioning = true;
        Audio.Stop();
        GetComponent<Movement>().enabled = false;
        Audio.PlayOneShot(Finish);
        FinishParticle.Play();
        Invoke ("LoadNextScene", DelayTime);
    }




    void ReloadScene()
    {
        int Currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(Currentsceneindex);
    }

    void LoadNextScene()
    {
        int Currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        int Nextsceneindex = Currentsceneindex + 1 ;
        if (Nextsceneindex == SceneManager.sceneCountInBuildSettings)
        {
            Nextsceneindex = 0;
        }
        SceneManager.LoadScene(Nextsceneindex);
    }

    void cheats()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextScene();
        }

        if(Input.GetKey(KeyCode.C))
        {
            CollisionDisabled = !CollisionDisabled; //toggle collision
        }


    }
}

