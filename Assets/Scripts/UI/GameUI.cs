using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject pauseMenu;

    private PlayerHealth playerHealth;
    private Enemy boss;

    private static GameUI instance;

    public static GameUI Get() => instance;

    private void Start()
    {
        instance = this;
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        healthSlider.value = ((float)playerHealth.CurrentHealth) / playerHealth.MaxHealth;
        EventManager.StatePause += ShowPauseMenu;
        EventManager.StateGamePlay += HidePauseMenu;
    }

    private void Update()
    {
        if (healthSlider.IsActive()) healthSlider.value = ((float)playerHealth.CurrentHealth) / playerHealth.MaxHealth;
        if(bossHealthSlider.IsActive()) bossHealthSlider.value = ((float)boss.CurrentHealth) / boss.MaxHealth;
    }

    public void ShowBossHealth(Enemy boss)
    {
        bossHealthSlider.gameObject.SetActive(true);
        bossHealthSlider.value = ((float)boss.CurrentHealth) / boss.MaxHealth;
        this.boss = boss;
    }

    public void OnLevelStart()
    {
        bossHealthSlider.gameObject.SetActive(false);
    }

    public void AudioMethod()
    {
        AudioManager.Instance.PlayUISFX("CLICK");
    }

    public void ShowPauseMenu() {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu() {
        pauseMenu.SetActive(false);
    }
}
