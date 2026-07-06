using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedVisualGameObjectArray;
    
    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == baseCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject selectedVisualGameObject in selectedVisualGameObjectArray) {
            selectedVisualGameObject.SetActive(true);
        }
    }
    
    private void Hide() {
        foreach (GameObject selectedVisualGameObject in selectedVisualGameObjectArray) {
            selectedVisualGameObject.SetActive(false);
        }
    }
}