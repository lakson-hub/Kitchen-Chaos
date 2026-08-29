using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
    
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectsTextMesh;
    [SerializeField] private TextMeshProUGUI musicTextMesh;
    [SerializeField] private TextMeshProUGUI moveUpTextMesh;
    [SerializeField] private TextMeshProUGUI moveDownTextMesh;
    [SerializeField] private TextMeshProUGUI moveLeftTextMesh;
    [SerializeField] private TextMeshProUGUI moveRightTextMesh;
    [SerializeField] private TextMeshProUGUI interactTextMesh;
    [SerializeField] private TextMeshProUGUI interactAlternateTextMesh;
    [SerializeField] private TextMeshProUGUI pauseTextMesh;
    [SerializeField] private TextMeshProUGUI gamepadInteractTextMesh;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateTextMesh;
    [SerializeField] private TextMeshProUGUI gamepadPauseTextMesh;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake() {
        Instance = this;
        
        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });
        
        moveUpButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Move_Up);
        });        
        moveDownButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Move_Down);
        });        
        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Move_Left);
        });        
        moveRightButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Move_Right);
        });        
        interactButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Interact);
        });        
        interactAlternateButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.InteractAlternate);
        });        
        pauseButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Pause);
        });
        gamepadInteractButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Gamepad_Interact);
        });        
        gamepadInteractAlternateButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Gamepad_InteractAlternate);
        });        
        gamepadPauseButton.onClick.AddListener(() => {
            RebindBinding(PlayerInput.Binding.Gamepad_Pause);
        });
    }

    private void Start() {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        
        UpdateVisual();
        
        HidePressToRebindKey();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectsTextMesh.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicTextMesh.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Up);
        moveDownTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Down);
        moveLeftTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Left);
        moveRightTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Right);
        interactTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Interact);
        interactAlternateTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.InteractAlternate);
        pauseTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Pause);
        gamepadInteractTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseTextMesh.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;
        
        gameObject.SetActive(true);
        
        soundEffectsButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    
    private void HidePressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(PlayerInput.Binding binding) {
        ShowPressToRebindKey();
        
        PlayerInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}