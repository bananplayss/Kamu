
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnShootBlank;
    public event EventHandler OnReload;
    public event EventHandler OnShootReal;

    [SerializeField] private float shakeDetectionThreshold = 3.6f;
    [SerializeField] private Animator anim;

    [SerializeField] private Button randomCardTableButton;
    [SerializeField] private string[] randomCardTableNames;
    [SerializeField] private TextMeshProUGUI tableText;
    [SerializeField] private TextMeshProUGUI restartText;
    [SerializeField] private TextMeshProUGUI restartText2;
    [SerializeField] private TextMeshProUGUI shootTimerText;
    [SerializeField] private Transform[] bullets;
    [SerializeField] private CanvasGroup deathScreen;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button restartButton2;

    [SerializeField] private Image revo;

    public float minShakeInterval;

    private float timeSinceLastShake;

    private int deathBullet;
    private int bulletsShot = 0;

    private int restartTouches = 0;

    private bool alive = true;
    private bool canShoot = true;

    private float shootCooldown;
    private float shootCooldownMax = 20;

	private void Awake() {
        Instance = this;

        randomCardTableButton.onClick.AddListener(() => {
            int random = UnityEngine.Random.Range(0,randomCardTableNames.Length);
            tableText.text = randomCardTableNames[random].ToString();

        });

        restartButton.onClick.AddListener(() => {
            RestartGame();
            
        });

		restartButton2.onClick.AddListener(() => {
			RestartGame2();

		});

        if(StartManager.Instance != null) {
			if (StartManager.Instance.role == StartManager.Role.Player) randomCardTableButton.gameObject.SetActive(false);
		}

		revo.color = RevolverCustomization.Instance.selectedColor;
	}

    private void RestartGame() {
		restartTouches++;
		restartText.text = "Biztos?";
		if (restartTouches == 2) {
			SceneManager.LoadScene(0);
		}
	}

	private void RestartGame2() {
		restartTouches++;
		restartText2.text = "Biztos?";
		if (restartTouches == 2) {
			SceneManager.LoadScene(0);
		}
	}

	private void Start() {
        deathBullet = UnityEngine.Random.Range(1, 6+1);
        Debug.Log(deathBullet);
        shootCooldown = shootCooldownMax;

        //Debug
        OnReload?.Invoke(this, EventArgs.Empty);

        canShoot = false;
	}

	private void Update() {
        if (!alive) return;
        if (canShoot) {
			if (Input.acceleration.sqrMagnitude >= shakeDetectionThreshold
			&& Time.unscaledTime >= timeSinceLastShake + minShakeInterval) {
				Shoot();
			}
        } else {
            shootTimerText.text = Mathf.Round(shootCooldown).ToString() + " mp";
            shootCooldown -= Time.deltaTime;
            if(shootCooldown <= 0) {
                canShoot = true;
                shootCooldown = shootCooldownMax;
            }
        }
	}

    

	private void Shoot() {
        canShoot = false;
        bulletsShot++;
        Debug.Log(bulletsShot);
        if(bulletsShot == deathBullet) {
            anim.Play("Shoot");
            OnShootReal?.Invoke(this, EventArgs.Empty);
            restartTouches = 0;


			StartCoroutine(GameOverCoroutine());
        } else {
            OnShootBlank?.Invoke(this, EventArgs.Empty);
        }

        DrawBullet();

	}

	private void DrawBullet() {
        bullets[bulletsShot-1].GetComponent<Animator>().Play("Draw");
	}
	

	IEnumerator GameOverCoroutine() {
        yield return new WaitForSeconds(3);
        GameOver();


    }

    private void GameOver() {
        alive = false;

        LeanTween.alphaCanvas(deathScreen, 1, 3);
    }
}
