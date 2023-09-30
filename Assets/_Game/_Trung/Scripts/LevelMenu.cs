using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
	[SerializeField] private GameObject[] levels;
	[SerializeField] private Button[] buttons;

	private void Awake() {
		int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].interactable = false;
		}
		for (int i = 0; i < unlockedLevel; i++) {
			buttons[i].interactable = true;
		}
	}
	public void openLevel(int index) {
		this.gameObject.SetActive(false);
		levels[index].SetActive(true);
	}
}
