using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Canvas pause;
    private void Start()
    {
        musicSlider.value = 0.5f;
    }
    private void Update()
    {
        music.volume = musicSlider.value;
    }
}
