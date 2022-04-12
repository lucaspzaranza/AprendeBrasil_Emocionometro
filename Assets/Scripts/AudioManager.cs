using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<ScreenAudioList> _audioLists;
    public List<ScreenAudioList> AudioLists => _audioLists;

    public Dictionary<string, List<AudioClip>> AudioDictionary { get; set; } = new Dictionary<string, List<AudioClip>>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        PopulateAudioDictionary();
    }

    private void PopulateAudioDictionary()
    {
        AudioDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (var screenAudioList in _audioLists)
        {
            AudioDictionary.Add(screenAudioList.name, screenAudioList.audios);
        }
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// Plays an audio with an interval to start.
    /// </summary>
    /// <param name="timeToStart">Time in seconds to start to play the audio.</param>
    /// <param name="screenNumber">The number of the audio screen.</param>
    /// <param name="audioNumber">The number of the audio of the chosen screen.</param>
    public void PlayAudio(float timeToStart, int screenNumber, int audioNumber)
    {
        StartCoroutine(PlayAudioCoroutine(timeToStart, screenNumber, audioNumber));
    }

    private IEnumerator PlayAudioCoroutine(float timeToStart, int screenNumber, int audioNumber)
    {
        yield return new WaitForSeconds(timeToStart);
        audioSource.PlayOneShot(AudioDictionary[$"Screen {screenNumber}"][audioNumber - 1]);
    }
}
