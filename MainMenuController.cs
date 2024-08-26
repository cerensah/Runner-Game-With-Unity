using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject char_Menu;
    public Text starScoreText;

    public Image music_Img;
    public Sprite music_Off,musicOn;

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void CharMenu()
    {
        char_Menu.SetActive(true);

        //display the star score
        starScoreText.text = "" + GameManager.instance.starScore;
    }

    public void HomeButton()
    {
        char_Menu.SetActive(false);
    }

    public void MusicButton()
    {
        if (GameManager.instance.playSound)
        {
            music_Img.sprite = music_Off;
            GameManager.instance.playSound = false;
        }
        else
        {
            music_Img.sprite = musicOn;
            GameManager.instance.playSound = true;
        }
    }

} //class






















