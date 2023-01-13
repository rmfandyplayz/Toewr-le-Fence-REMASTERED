using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeusUnite.Audio;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Plays music WITHOUT repeat. To play music with repeat, use PlayMusicRepeat().
    /// </summary>
    /// <param name="music"></param>
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
