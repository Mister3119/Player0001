using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject MenuSettings;
    public GameObject Menu;
    public GameObject Blur;

    public Player player;

    void Start()
    {
        Menu.SetActive(false);
        MenuSettings.SetActive(false);
        SetCursorState(CursorLockMode.Locked);
    }

    void SetCursorState(CursorLockMode wantedMode)
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking

        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }

    private void Update()
    {
        //quand la touche echape est préssé
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if MenuSettings est afiché allors le caché
            if (MenuSettings.activeSelf)
            {
                MenuSettings.SetActive(false);
                Menu.SetActive(true);
            }
            else if (Menu.activeSelf)//if MenuSttings est caché est que le menu est afiché allors le caché
            {
                player.enabled = true;
                Menu.SetActive(false);
                Blur.SetActive(false);
                SetCursorState(CursorLockMode.Locked);
            }

            if (!Menu.activeSelf)
            {
                player.enabled = false;
                Menu.SetActive(true);
                Blur.SetActive(true);
                SetCursorState(CursorLockMode.None);
            }
        }
    }

    public void Continue()
    {
        if (Menu.activeSelf)
        {
            player.enabled = true;
            Menu.SetActive(false);
            Blur.SetActive(false);
            SetCursorState(CursorLockMode.Locked);
        }
    }

    public void OpenSettings()
    {
        Menu.SetActive(false);
        MenuSettings.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        MenuSettings.SetActive(false);
        Menu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
