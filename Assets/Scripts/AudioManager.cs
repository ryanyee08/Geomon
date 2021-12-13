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


    // Start is called before the first frame update
    void Start()
    {
        //Load Audio Dictionary
        AudioDictionary.Add("mBattle", musicBattle);
        AudioDictionary.Add("mVictory", musicVictory);


        AudioDictionary.Add("fInterfaceInput", fxInterfaceInput);
        AudioDictionary.Add("fTakeDamage", fxTakeDamage);

        AudioDictionary.Add("fAttackTackle", fxAttackTackle);
        AudioDictionary.Add("fAttackBlast", fxAttackBlast);

    }

    // Update is called once per frame
    void Update()
    {
        
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

    // Loads an  Audioclip to the Fx source and plays it
    [YarnCommand("playFx")]
    public void playFx(string audiofile)
    {
        fxSource.clip = AudioDictionary[audiofile];
        fxSource.Play();
        Debug.Log("playfx audio should now be: " + audiofile);

    }

    public void playInterfaceFx(string audiofile)
    {
        fxInterfaceSource.clip = AudioDictionary[audiofile];
        fxInterfaceSource.Play();
        Debug.Log("playfxinterface audio should now be: " + audiofile);

    }

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

    // Stops playback of Fx

}

//Okay so i think what i need to do to change tracks is to set the new clip as AudioSource.clip = the clip I want and then do Audio.Play