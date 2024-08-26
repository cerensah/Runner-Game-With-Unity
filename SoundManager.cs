using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource
        move_Audio_source,
        jump_Audio_source,
        powerUp_Die_Audio_source,
        background_Audio_source;

    public AudioClip
        power_Up_Clip,
        die_Clip,
        coin_Clip,
        game_Over_Clip;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        //TEST IF WE SHOULD PLAY BG SOUND
        if (GameManager.instance.playSound)
        {
            background_Audio_source.Play();
        }
        else
        {
            background_Audio_source.Stop();
        }
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void PlayMoveLineSound()
    {
        move_Audio_source.Play();
    }

    public void PlayJumpSound()
    {
        jump_Audio_source.Play();
    }

    public void PlayDeadSound()
    {
        powerUp_Die_Audio_source.clip = die_Clip;
        powerUp_Die_Audio_source.Play();
    }

    public void PlayPowerUpSound()
    {
        powerUp_Die_Audio_source.clip = power_Up_Clip;
        powerUp_Die_Audio_source.Play();
    }


    public void PlayStarSound()
    {
        powerUp_Die_Audio_source.clip = coin_Clip;
        powerUp_Die_Audio_source.Play();
    }


    public void PlayGameOverClip()
    {
        background_Audio_source.Stop();
        background_Audio_source.clip = game_Over_Clip;
        background_Audio_source.loop = false;
        background_Audio_source.Play();
    }

} //class


























