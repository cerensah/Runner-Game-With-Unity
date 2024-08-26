using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour
{
    public GameObject[] available_Chars;

    private int currentIndex;

    public Text selectedText;
    public GameObject starIcon;
    public Image selectBtn_Image;
    public Sprite button_Green, button_Blue;

    private bool[] chars;

    public Text starScoreText;

    void Start()
    {
        InitializeCharacters();
    }

    void InitializeCharacters()
    {
        currentIndex = GameManager.instance.selected_Index;

        for(int i = 0; i <available_Chars.Length; i++)
        {
            available_Chars[i].SetActive(false);
        }

        available_Chars[currentIndex].SetActive(true);

        chars = GameManager.instance.chars;
    }

    public void NextChar()
    {
        available_Chars[currentIndex].SetActive(false);

        if(currentIndex + 1 == available_Chars.Length)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }

        available_Chars[currentIndex].SetActive(true);

        CheckIfCharacterIsUnlocked();
    }

    public void PreviousChar()
    {
        available_Chars[currentIndex].SetActive(false);

        if (currentIndex - 1 == -1)
        {
            currentIndex = available_Chars.Length - 1;
        }
        else
        {
            currentIndex--;
        }

        available_Chars[currentIndex].SetActive(true);
    }

    void CheckIfCharacterIsUnlocked()
    {
        if (chars[currentIndex])
        { //if the hero is unlocked
            starIcon.SetActive(false);

            if(currentIndex == GameManager.instance.selected_Index)
            {
                selectBtn_Image.sprite = button_Green;
                selectedText.text = "Selected";
            }
            else
            {
                selectBtn_Image.sprite = button_Blue;
                selectedText.text = "Select?";
            }
        }
        else
        { //if the hero is locked, we will prompt user to buy
            selectBtn_Image.sprite = button_Blue;
            starIcon.SetActive(true);
            selectedText.text = "1000";
        }
    }

    public void SelectCharacter()
    {
        //if the character in the index is locked. If you have enough points, buy it
        if (!chars[currentIndex])
        {
            if (currentIndex != GameManager.instance.selected_Index)
            {
                if (GameManager.instance.starScore >= 1000)
                {
                    GameManager.instance.starScore -= 1000;
                    selectBtn_Image.sprite = button_Green;
                    selectedText.text = "Selected";
                    starIcon.SetActive(false);
                    chars[currentIndex] = true;

                    starScoreText.text = GameManager.instance.starScore.ToString();

                    GameManager.instance.selected_Index = currentIndex;
                    GameManager.instance.chars = chars;

                    GameManager.instance.SaveGameData();

                }
                else
                {
                    print("NOT ENOUGH STARS");
                }
            }
        }
        else //if the character in the index is unlocked. which means it is selectable
        {
            selectBtn_Image.sprite = button_Green;
            selectedText.text = "Selected";
            GameManager.instance.selected_Index = currentIndex;

            GameManager.instance.SaveGameData();
        }
    }

} //class




























