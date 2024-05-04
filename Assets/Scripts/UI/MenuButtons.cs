using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private const float delay = 0.3f;

    AudioSource buttonPressSound;

    private void Start()
    {
        // Load audio component
        buttonPressSound = GetComponent<AudioSource>();
    }

    public void StartAgainPress()
    {
        StartCoroutine(StartAgain());
    }

    public void QuitGamePress()
    {
        StartCoroutine(QuitGame());
    }

    private void PlayButtonPressSound()
    {
        // Play the sount if exists
        if (buttonPressSound != null)
        {
            buttonPressSound.Play();
        }        
    }

    IEnumerator StartAgain()
    {
        // Play the sound before reloading the scene
        PlayButtonPressSound();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("1stLevel");
    }

    IEnumerator QuitGame()
    {
        // Play the sound before exit the game
        PlayButtonPressSound();
        yield return new WaitForSeconds(delay);
        QuitGameQuit();
    }

    private void QuitGameQuit()
    {
        // Exit the game
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
