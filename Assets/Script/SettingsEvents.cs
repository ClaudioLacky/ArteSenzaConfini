using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsEvents : MonoBehaviour
{
    public static SettingsEvents instance;

    private UIDocument document;

    private DropdownField dropdownResolution;

    private DropdownField dropdownQuality;

    private Slider sliderVolume;

    private Button buttonApply;

    private Button buttonReturn;

    private string sceneLoaded;

    [SerializeField]
    private AudioMixer audioMixer;

    private void Awake()
    {
        instance = this;

        float volumeInit;

        document = GetComponent<UIDocument>();

        dropdownResolution = document.rootVisualElement.Q("DropdownResolution") as DropdownField;

        dropdownQuality = document.rootVisualElement.Q("DropdownQuality") as DropdownField;

        sliderVolume = document.rootVisualElement.Q("SliderVolume") as Slider;

        InitDisplayResolution();
        InitQualitySettings();

        buttonApply = document.rootVisualElement.Q("ButtonApply") as Button;
        buttonApply.RegisterCallback<ClickEvent>(OnButtonApply);


        buttonReturn = document.rootVisualElement.Q("ButtonReturn") as Button;
        buttonReturn.RegisterCallback<ClickEvent>(OnButtonReturn);

        audioMixer.GetFloat("volume", out volumeInit);

        volumeInit += 80;

        sliderVolume.value = volumeInit;

        sliderVolume.RegisterCallback<ChangeEvent<float>>(OnValueChanged);
    }

    private void OnValueChanged(ChangeEvent<float> changeEvent)
    {
        SetVolume(changeEvent.newValue);
    }
    public void SetVolume(float volume)
    {
        volume = volume - 80;
        audioMixer.SetFloat("volume", volume);
    }

    private void InitDisplayResolution()
    {
        dropdownResolution.choices = Screen.resolutions.Select(resolution => $"{resolution.width}x{resolution.height}").ToList();
        dropdownResolution.index = Screen.resolutions
            .Select((resolution, index) => (resolution, index))
            .First((value) => value.resolution.width == Screen.currentResolution.width && value.resolution.height == Screen.currentResolution.height)
            .index;
    }

    private void InitQualitySettings()
    {
        dropdownQuality.choices = QualitySettings.names.ToList();
        dropdownQuality.index = QualitySettings.GetQualityLevel();
    }

    private void OnButtonApply(ClickEvent evt)
    {
        var resolution = Screen.resolutions[dropdownResolution.index];
        Screen.SetResolution(resolution.width, resolution.height, true);
        QualitySettings.SetQualityLevel(dropdownQuality.index, true);
    }

    private void OnButtonReturn(ClickEvent evt)
    {
        SceneManager.UnloadSceneAsync("Settings");
    }

    private void OnDisable()
    {
        buttonApply.UnregisterCallback<ClickEvent>(OnButtonApply);
        buttonReturn.UnregisterCallback<ClickEvent>(OnButtonReturn);
    }

    public void SceneLoaded(string scene)
    {
        sceneLoaded = scene;
    }
}