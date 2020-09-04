using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundHelper : MonoBehaviour
{

    public Slider music_volume;
    public Slider sfx_volume;
    public Button music_mute_onoff;

    public Text muteMusicTxt;
    public Text muteSfxTxt;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Title")
        {
            if (!SoundManager.sound_manager.title_soundtrack.isPlaying)
            {
                music_volume.value = SoundManager.sound_manager.title_soundtrack.volume;
                SoundManager.sound_manager.title_soundtrack.Play();
                SoundManager.sound_manager.gameplay_soundtrack.Stop();
            }
            
        }
        else if (scene.name == "Main")
        {
            music_volume.value = SoundManager.sound_manager.gameplay_soundtrack.volume;
            SoundManager.sound_manager.title_soundtrack.Stop();
            SoundManager.sound_manager.gameplay_soundtrack.Play();
        }
    }

    private void Update()
    {
        if (SoundManager.sound_manager.mute_music)
        {
            muteMusicTxt.text = "music muted";
        }
        else
        {
            muteMusicTxt.text = "music on";
        }

        if (SoundManager.sound_manager.mute_sfx)
        {
            muteSfxTxt.text = "sfx muted";
        }
        else
        {
            muteSfxTxt.text = "sfx on";
        }
    }

    public void slideAudio()
    {
        SoundManager.sound_manager.title_soundtrack.volume = music_volume.value;
        SoundManager.sound_manager.gameplay_soundtrack.volume = music_volume.value;
    }

    public void slideSFXAudio()
    {
        SoundManager.sound_manager.hit_sfx.volume = sfx_volume.value;
        SoundManager.sound_manager.jump_sfx.volume = sfx_volume.value;
        SoundManager.sound_manager.death_sfx.volume = sfx_volume.value;
        SoundManager.sound_manager.smash_sfx.volume = sfx_volume.value;
    }

    public void Mute_Music()
    {
        SoundManager.sound_manager.mute_music = !SoundManager.sound_manager.mute_music;
    }

    public void Mute_Sfx()
    {
        SoundManager.sound_manager.mute_sfx = !SoundManager.sound_manager.mute_sfx;
    }
}
