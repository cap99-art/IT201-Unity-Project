using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("1 - Text labels")]
    [SerializeField] private TMP_Text CooldownText;

    [Header("2 - UI controls")]
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Toggle soundToggle;

    [Header("3 - Actual sound")]
    [SerializeField] private AudioSource uiAudio;

    [Header("Other")]
    public GameObject cubeToSpawn;

    // Values stored by the script (not extra GameObjects).
    private string currentState = "Idle";
    private float currentSpeed = 5f;
    private bool soundEnabled = true;
    private bool audioPaused = false;

    private void Start()
    {
        if (speedSlider != null) currentSpeed = speedSlider.value;
        if (soundToggle != null) soundEnabled = soundToggle.isOn;
        if (uiAudio != null)
        {
            uiAudio.playOnAwake = false;
            uiAudio.loop = true; // long enough to demonstrate pause/resume
            uiAudio.mute = !soundEnabled;
            uiAudio.pitch = SpeedToPitch(currentSpeed);
        }
        RefreshLabels();
        Debug.Log("Week 9 UI is ready");
    }

    
    public void StartPressed()
    {
        currentState = "Playing";
        if (uiAudio != null && uiAudio.clip != null)
        {
            if (audioPaused) uiAudio.UnPause(); // continue from pause position
            else if (!uiAudio.isPlaying) uiAudio.Play(); // start from beginning
        }
        audioPaused = false;
        RefreshLabels();
    }

    public void PausePressed()
    {
        currentState = "Paused";
        if (uiAudio != null && uiAudio.isPlaying)
        {
            uiAudio.Pause();
            audioPaused = true;
        }
        RefreshLabels();
    }

    public void ResetPressed()
    {
        currentState = "Ready";
        if (uiAudio != null) uiAudio.Stop(); // next Start plays from beginning
        audioPaused = false;
        RefreshLabels();
    }

    public void SpawnPressed()
    {
        currentState = "Spawned";
        Vector3 spawnPosition = Vector3.up * 0.5f;
        Instantiate(cubeToSpawn, spawnPosition, Quaternion.identity);
        RefreshLabels();
    }

    // Values 0..10 map to AudioSource.pitch 0.5..1.5; 5 gives normal pitch (1.0).
    public void SetSpeed(float value)
    {
        currentSpeed = value;
        if (uiAudio != null) uiAudio.pitch = SpeedToPitch(currentSpeed);
        RefreshLabels();
    }

    private float SpeedToPitch(float value)
    {
        return Mathf.Lerp(0.5f, 1.5f, Mathf.Clamp01(value / 10f));
    }

    public void SetSound(bool enabled)
    {
        soundEnabled = enabled;
        if (uiAudio != null) uiAudio.mute = !enabled;
        RefreshLabels();
    }

    private void RefreshLabels()
    {
      //  if (statusText != null) statusText.text = "Status: " + currentState;
        //if (speedText != null) speedText.text = "Speed: " + currentSpeed.ToString("0.0");
        //if (soundText != null) soundText.text = "Sound: " + (soundEnabled ? "On" : "Off");
    }
}