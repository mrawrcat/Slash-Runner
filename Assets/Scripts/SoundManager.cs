using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sound_manager;

    [Header("Sound Stuff")]
    public AudioSource jump_sfx;
    public AudioSource hit_sfx;
    public AudioSource death_sfx;
    public AudioSource smash_sfx;
    public AudioSource title_soundtrack;
    public AudioSource gameplay_soundtrack;
    public bool play_hit_sfx;
    public bool play_death_sfx;
    public bool play_soundtrack;
    public bool mute_music;
    public bool mute_sfx;
    private void Awake()
    {
        if (sound_manager == null)
        {
            sound_manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (sound_manager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (mute_music)
        {
            title_soundtrack.mute = true;
            gameplay_soundtrack.mute = true;
        }
        else
        {
            title_soundtrack.mute = false;
            gameplay_soundtrack.mute = false;
        }

        if (mute_sfx)
        {
            jump_sfx.mute = true;
            hit_sfx.mute = true;
            death_sfx.mute = true;
            smash_sfx.mute = true;
        }
        else
        {
            jump_sfx.mute = false;
            hit_sfx.mute = false;
            death_sfx.mute = false;
            smash_sfx.mute = false;
        }
    }

}
