using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static void PlaySound(AudioClip sound)
    {

    }

    public static void StopSound(AudioClip sound)
    {
        
    }

    public static void StopAllSounds()
    {

    }

    public static void PlayMusic(AudioClip music)
    {
        
    }

    /// <summary>
    /// This function will play music on repeat.
    /// </summary>
    /// <param name="music"></param>
    /// <param name="repeatTimeStamp">If the music finishes, what timestamp should it go to to play the music again? Set to -1 to make the entire track repeat (default).</param>
    public static void PlayMusicRepeat(AudioClip music, float repeatTimeStamp = -1)
    {

    }

    public static void StopMusic(AudioClip music)
    {

    }

    public static void StopAllMusic()
    {

    }
}
