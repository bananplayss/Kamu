
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public static StartManager Instance { get; private set; }

    public enum Role {
        Player,
        Admin,
    }

    public Role role;

    [SerializeField] private Button startButton;
    [SerializeField] private Button playAsButton;
    [SerializeField] private TextMeshProUGUI playAsText;


	private void Awake() {
        Instance = this;

        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });

        playAsButton.onClick.AddListener(() => {
            if (role == Role.Player) {
                playAsText.text = "Játékvezetõ";
                role = Role.Admin;
            } else {
                playAsText.text = "Játékos";
                role = Role.Player;
            }
		});
	}

	private void Start() {
		DontDestroyOnLoad(this);
	}
}
