using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sound_manager;

    [Header("Sound Stuff")]
    //public AudioSource jump_sfx;
    //public AudioSource hit_sfx;
    //public AudioSource death_sfx;
    //public AudioSource smash_sfx;
    public AudioSource title_soundtrack;
    public AudioSource gameplay_soundtrack;
    //public bool play_hit_sfx;
    //public bool play_death_sfx;
    public bool play_soundtrack;
    public bool mute_music;
    //public bool mute_sfx;
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

}
