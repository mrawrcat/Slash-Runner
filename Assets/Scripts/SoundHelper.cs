using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundHelper : MonoBehaviour
{

    public Slider music_volume;
    public Button music_mute_onoff;
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


    public void slideAudio()
    {
        SoundManager.sound_manager.title_soundtrack.volume = music_volume.value;
        SoundManager.sound_manager.gameplay_soundtrack.volume = music_volume.value;
    }
}
