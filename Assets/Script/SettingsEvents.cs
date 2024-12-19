using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce l'interfaccia delle impostazioni
/// </summary>
public class SettingsEvents : MonoBehaviour
{
    public static SettingsEvents instance;  // Istanza della classe

    private UIDocument documentSettings;    // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private UIDocument documentExternal;    // Componente documento di un altro oggetto

    private DropdownField dropdownResolution;   // Componente dropdown del documento UXML per scegliere la risoluzione dello schermo

    private DropdownField dropdownQuality;      // Componente dropdown del documento UXML per scegliere la qualità del gioco

    private Slider sliderVolume;                // Componente slider del documento UXML per sistemare il volume del gioco

    private Button buttonApply;                 // Componente bottone del documento UXML per applicare le modifiche

    private Button buttonReturn;                // Componente bottone del documento UXML per tornare indietro

    [SerializeField]
    private AudioMixer audioMixer;              // Componente AudioMixer per regolare il volume del gioco

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        instance = this;

        float volumeInit;           // Volume iniziale

        documentSettings = GetComponent<UIDocument>();      // Per prendere il documento UXML assegnato all'oggetto

        // Per rendere non visibile all'inizio l'interfaccia dei Settings
        documentSettings.rootVisualElement.style.display = DisplayStyle.None;

        // Inizializzazione del dropdownField da assegnare allo specifico dropdownField del documento UXML
        // di cui si sta facendo riferimento
        dropdownResolution = documentSettings.rootVisualElement.Q("DropdownResolution") as DropdownField;

        // Inizializzazione del dropdownField da assegnare allo specifico dropdownField del documento UXML
        // di cui si sta facendo riferimento
        dropdownQuality = documentSettings.rootVisualElement.Q("DropdownQuality") as DropdownField;

        // Inizializzazione dello slider da assegnare allo specifico slider del documento UXML di cui si sta facendo riferimento
        sliderVolume = documentSettings.rootVisualElement.Q("SliderVolume") as Slider;

        InitDisplayResolution();    // Metodo che inizializza il menù a tendina della risoluzione
        InitQualitySettings();      // Metodo che inizializza il menù a tendina della qualità

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonApply = documentSettings.rootVisualElement.Q("ButtonApply") as Button;
        buttonApply.RegisterCallback<ClickEvent>(OnButtonApply);        // Aggiunge un gestore di eventi in questo caso
                                                                        // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonReturn = documentSettings.rootVisualElement.Q("ButtonReturn") as Button;
        buttonReturn.RegisterCallback<ClickEvent>(OnButtonReturn);      // Aggiunge un gestore di eventi in questo caso
                                                                        // se si clicca il bottone genera un evento

        audioMixer.GetFloat("volume", out volumeInit);  // Si prende il valore iniziale del volume dell'AudioMixer e salvato in volumeInit

        volumeInit += 80;   // Si sistema nella scala dei numeri interi da 0 a 100

        sliderVolume.value = volumeInit;    // Si porta il cursore slider al volume inziziale

        // Aggiunge un gestore di eventi in questo caso se si sposta il cursore slide genera un evento
        sliderVolume.RegisterCallback<ChangeEvent<float>>(OnValueChanged);
    }

    /// <summary>
    /// Imposta il documento di un altro oggetto
    /// </summary>
    /// <param name="document">Documento dell'altro oggetto</param>
    public void SetDocument(UIDocument document)
    {
        documentExternal = document;
    }

    /// <summary>
    /// Metodo che gestisce l'evento dello spostamento del cursore
    /// </summary>
    /// <param name="changeEvent">Registra l'evento del cambio di valore del cursore</param>
    private void OnValueChanged(ChangeEvent<float> changeEvent)
    {
        // Cambiando valore il cursore imposta il volume dell'AudioMixer al nuovo valore corrispondente del cursore
        SetVolume(changeEvent.newValue);
    }

    /// <summary>
    /// Metodo che imposta il volume dell'AudioMixer
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        volume = volume - 80;       // Si sistema nella scala Decibel da -80 a 1
        audioMixer.SetFloat("volume", volume);      // Si aggiorna il valore del volume sull'AudioMixer
    }

    /// <summary>
    /// Metodo che inizializza il menù a tendina della risoluzione dello schermo
    /// </summary>
    private void InitDisplayResolution()
    {
        // Prende le possibilità di risoluzione dalle impostazioni di Unity e le inscerisce nel menù a tendina
        dropdownResolution.choices = Screen.resolutions.Select(resolution => $"{resolution.width}x{resolution.height}").ToList();
        dropdownResolution.index = Screen.resolutions
            .Select((resolution, index) => (resolution, index))
            .First((value) => value.resolution.width == Screen.currentResolution.width && value.resolution.height == Screen.currentResolution.height)
            .index;
    }

    /// <summary>
    /// Metodo che inizializza il menù a tendina della qualità del gioco
    /// </summary>
    private void InitQualitySettings()
    {
        // Prende le possibilità di qualità dalle impostazioni di Unity e le inserisce nel menù a tendina
        dropdownQuality.choices = QualitySettings.names.ToList();
        dropdownQuality.index = QualitySettings.GetQualityLevel();
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone che conferma le modifiche apportate alle impostazioni del gioco
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonApply(ClickEvent evt)
    {
        // Conferma le modifiche apportate
        var resolution = Screen.resolutions[dropdownResolution.index];
        Screen.SetResolution(resolution.width, resolution.height, true);
        QualitySettings.SetQualityLevel(dropdownQuality.index, true);
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone che disattiva l'interfaccia delle impostazioni
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonReturn(ClickEvent evt)
    {
        // Disattiva l'interfaccia dei Settings e attiva l'interfaccia precedente
        documentSettings.rootVisualElement.style.display = DisplayStyle.None;
        documentExternal.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Metodo che disabilita gli eventi sui bottoni
    /// </summary>
    private void OnDisable()
    {
        buttonApply.UnregisterCallback<ClickEvent>(OnButtonApply);
        buttonReturn.UnregisterCallback<ClickEvent>(OnButtonReturn);
    }
}