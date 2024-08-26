using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameData
{
    private int star_Score;
    private int score_Count;

    private bool[] chars;

    private int selected_Index;

    public int StarScore
    {
        get
        {
            return star_Score;
        }
        set
        {
            star_Score = value;
        }
    }

    public int ScoreCount
    {
        get
        {
            return score_Count;
        }
        set
        {
            score_Count = value;
        }
    }

    public bool[] Chars
    {
        get {
            return chars;
        }
        set
        {
            chars = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return selected_Index;
        }
        set
        {
            selected_Index = value;

        }
    }
} //class















