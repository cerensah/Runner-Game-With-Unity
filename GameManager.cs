using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameData gameData;

    [HideInInspector]
    public int starScore, score_Count, selected_Index;

    [HideInInspector]
    public bool[] chars;

    [HideInInspector]
    public bool playSound = true;

    private string data_Path = "GameData.dat";

    void Awake()
    {
        MakeSingleton();
        InitializeGameData();
    }

    void Start()
    {

    }

    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }else if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameData()
    {
        LoadGameData();

        
        if(gameData == null){
            //means: we are running the game for the first time

            //starScore = 0;

            starScore = 9000;

            score_Count = 0;
            selected_Index = 0;

            chars = new bool[9];
            chars[0] = true;

            for(int i = 1; i < chars.Length; i++)
            {
                chars[i] = false;
            }

            gameData = new GameData();
            gameData.StarScore = starScore;
            gameData.Chars = chars;
            gameData.ScoreCount = score_Count;
            gameData.SelectedIndex = selected_Index;

            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            //a directory to store our data
            file = File.Create(Application.persistentDataPath + data_Path);

            if(gameData != null)
            {
                gameData.Chars = chars;
                gameData.StarScore = starScore;
                gameData.ScoreCount = score_Count;
                gameData.SelectedIndex = selected_Index;

                bf.Serialize(file, gameData);
            }

        }catch(Exception e)
        {

        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
        }
    }

    void LoadGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + data_Path, FileMode.Open);

            //deserialize will catch the data inside the file as an object. We are saying it that the object is from GameData class
            gameData = (GameData)bf.Deserialize(file);

            if(gameData != null)
            {
                starScore = gameData.StarScore;
                score_Count = gameData.ScoreCount;
                chars = gameData.Chars;
                selected_Index = gameData.SelectedIndex;
            }
        }
        catch(Exception e)
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

} //class

/*
 * Having a singleton - intance != null part - will allow us to not have multiples of this game object, it will 
 * destroy the duplicate.
 * 
 * DontDestroyOnLoad part will make this gameobject not destroyable. So, when we go from
 * one scene to the other this object won't be destroyed.
 */



/*
 * what is try&catch? When we are trying to save data on the computer, we should use these.
 * Because we are trying to create or open a file. If there is a problem (for ex: we cant open the file or the file
 * does not exist) we will catch that exception so that program does not crash.
 * 
 * 
 * Finally clause will be executed even if we have an exception or not
 */



















