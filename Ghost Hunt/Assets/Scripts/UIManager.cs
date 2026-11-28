using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("1 - Text labels")]
    [SerializeField] private TMP_Text stats;
    [SerializeField] private TMP_Text AbilityText;

    [Header("2 - UI controls")]
    [SerializeField] private Toggle soundToggle;

    [Header("3 - Actual sound")]
    [SerializeField] private AudioSource uiAudio;

    // Values stored by the script (not extra GameObjects).
    private float currentSpeed = 5f;
    private float cooldown = 0;
    private bool soundEnabled = true;
    private bool audioPaused = false;

    private void Start()
{
    if (soundToggle != null)
    {
        soundToggle.isOn = soundEnabled;
    }

    if (uiAudio != null)
    {
        uiAudio.playOnAwake = false;
        uiAudio.loop = true;
        uiAudio.mute = !soundEnabled;

        if (soundEnabled)
        {
            uiAudio.Play();
        }
    }
}

public void SetSound(bool enabled)
{
    soundEnabled = enabled;

    if (uiAudio != null)
    {
        uiAudio.mute = !soundEnabled;
    }
}

    void Update()
    {
        stats.text = "Level " + ClickScript.level + " [Round " + ClickScript.round + "]" +
            "\nPoints: " + ClickScript.score + 
            "\nKills: " + ClickScript.ghostsDestroyed;

        //creates a cooldown that counts from 5 to 0, including miliseconds, displayed as text UI.
        //once the cooldown is over, the speedMultiplier goes back to normal speed.
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            AbilityText.text = cooldown.ToString("F2");

            if (cooldown <= 0)
            {
                Movement.speedMultiplier = 1f;
                AbilityText.text = "";
            }
        }
    }

    //when the ability is clicked, it checks the cost, and makes
    //the cooldown 5f so that the Update function can count it down,
    //and lowers the speedMultiplier so that ghosts are slower.
    public void AbilityPressed()
    {
        if (ClickScript.score >= 5)
        {
            ClickScript.score -= 5;
            cooldown = 5f;
            Movement.speedMultiplier = 0.1f;
        }
    }

    //public void StartPressed()
    //{
    //    currentState = "Playing";
    //    if (uiAudio != null && uiAudio.clip != null)
    //    {
    //        if (audioPaused) uiAudio.UnPause(); // continue from pause position
    //        else if (!uiAudio.isPlaying) uiAudio.Play(); // start from beginning
    //    }
    //    audioPaused = false;
    //}

    //private float SpeedToPitch(float value)
    //{
    //    return Mathf.Lerp(0.5f, 1.5f, Mathf.Clamp01(value / 10f));
    //}

    //public void SetSound(bool enabled)
    //{
    //    soundEnabled = enabled;
    //    if (uiAudio != null) uiAudio.mute = !enabled;
    //}
}