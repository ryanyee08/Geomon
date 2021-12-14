using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AudioManager : MonoBehaviour
{
    // AudioManager should handle all audio playback in the game.
    // It is accessible to yarnspinner so that audio cues can be provided by the yarn dialogue script
    // Please see https://yarnspinner.dev/docs/unity/working-with-commands/ for documentation on linking script to monobehaviors

    public AudioSource ambienceSource;
    public AudioSource musicSource;
    public AudioSource fxSource;
    public AudioSource fxInterfaceSource;

    // Dictionary is neccesary because YarnSpinner commands can only pass strings
    Dictionary<string, AudioClip> AudioDictionary = new Dictionary<string, AudioClip>();
    
    // Audio Clips
    public AudioClip ambienceRain;

    public AudioClip fxInterfaceInput;
    public AudioClip fxTakeDamage;
    public AudioClip fxAttackTackle;
    public AudioClip fxAttackBlast;

    public AudioClip musicBattle;
    public AudioClip musicVictory;
    public AudioClip musicRival;
    public AudioClip musicMainMenu;
    public AudioClip musicEuclidTown;
    public AudioClip musicProfShrubLab;


    // Start is called before the first frame update
    void Start()
    {
        //Load Audio Dictionary
        AudioDictionary.Add("mBattle", musicBattle);
        AudioDictionary.Add("mVictory", musicVictory);
        AudioDictionary.Add("mRival", musicRival);
        AudioDictionary.Add("mMainMenu", musicMainMenu);
        AudioDictionary.Add("mEuclidTown", musicEuclidTown);
        AudioDictionary.Add("mProfShrubLab", musicProfShrubLab);

        AudioDictionary.Add("fInterfaceInput", fxInterfaceInput);
        AudioDictionary.Add("fTakeDamage", fxTakeDamage);

        AudioDictionary.Add("fAttackTackle", fxAttackTackle);
        AudioDictionary.Add("fAttackBlast", fxAttackBlast);

    }

    // Loads an  Audioclip to the music source and plays it
    // Note that music source is configured in editor to loop
    [YarnCommand("playMusic")]
    public void playMusic(string audiofile)
    {
        musicSource.clip = AudioDictionary[audiofile];
        musicSource.Play();
        Debug.Log("music audio should now be: " + audiofile );

    }

    // Loads an  Audioclip to the Ambience source and plays it
    // Note that ambience source is configured in editor to loop
    [YarnCommand("playAmbience")]
    public void playAmbience(string audiofile)
    {
        ambienceSource.clip = AudioDictionary[audiofile];
        ambienceSource.Play();
        Debug.Log("ambience audio should now be: " + audiofile);

    }

    // Loads an  Audioclip to the Fx Battle source and plays it
    [YarnCommand("playFx")]
    public void playFx(string audiofile)
    {
        fxSource.clip = AudioDictionary[audiofile];
        fxSource.Play();
        Debug.Log("playfx audio should now be: " + audiofile);

    }

    // Loads an Audioclip to the Fx Interface source and plays it
    public void playInterfaceFx(string audiofile)
    {
        fxInterfaceSource.clip = AudioDictionary[audiofile];
        fxInterfaceSource.Play();
        Debug.Log("playfxinterface audio should now be: " + audiofile);

    }

    // Need a function that 

    // Stops playback of music source
    [YarnCommand("stopMusic")]
    public void stopMusic()
    {
        musicSource.Stop();
    }

    // Stops playback of Ambience
    [YarnCommand("stopAmbience")]
    public void stopAmbience()
    {
        ambienceSource.Stop();
    }
}