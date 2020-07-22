using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Slider timeSlider;
    public Button pauseButton;

    public Sprite PauseSprite;
    public Sprite StartSprite;

    public float CurrentTimeValue
    {
        get => Time.timeScale;
        set
        {
            Time.timeScale = value;
            timeSlider.value = value;

            pauseButton.GetComponent<Image>().sprite = value > 0f ? PauseSprite : StartSprite;
        }
    }

    public void Pause()
    {
        CurrentTimeValue = CurrentTimeValue == 0f ? 0.5f : 0f;
    }
}