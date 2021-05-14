using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    //Help
    public GameObject helpPanel;
    public Toggle helpToggle;

    //Audio
    public AudioSource themeMusic;
    public Toggle audioToggle;

    private void Start()
    {
        helpPanel.SetActive(helpToggle.isOn);
    }

    private void Update()
    {
        //Help
        helpPanel.SetActive(helpToggle.isOn);
        //Audio
        themeMusic.mute = !audioToggle.isOn;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
