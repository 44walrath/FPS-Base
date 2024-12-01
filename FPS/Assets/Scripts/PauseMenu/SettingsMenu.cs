using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject fpsToggle;
    public GameObject settingsMenuUI;
    void Start()
    {
        print (fpsToggle.GetComponent<Toggle>().isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Return()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
}


