
using UnityEngine;
using UnityEngine.UI;

public class RevolverCustomization : MonoBehaviour
{
	[SerializeField] private Button[] revos;

	public Color selectedColor;

    public static RevolverCustomization Instance { get; private set; }

	private void Awake() {
		Instance= this;

		DontDestroyOnLoad(this);

		selectedColor = Color.white;

		foreach(Button button in revos) {
			button.onClick.AddListener(() => {
				selectedColor = button.GetComponent<Image>().color;
			});
		}
	}
}
