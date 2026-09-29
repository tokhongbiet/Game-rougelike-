using UnityEngine;
using UnityEngine.SceneManagement; // Bắt buộc có để dùng SceneManager

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Reference")]
    public GameObject gameOverPanel;

    private void Awake()
    {
        // Singleton đơn giản
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Đảm bảo ẩn Panel khi vào game
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Trả lại time scale bình thường
        Time.timeScale = 1f;
    }

    // Hàm gọi khi Game Over
    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Đóng băng thời gian trong game (tùy chọn)
        Time.timeScale = 0f;
    }

    // Hàm gắn vào nút Restart
    public void RestartGame()
    {
        // Trả lại tốc độ thời gian
        Time.timeScale = 1f;

        // Tải lại Scene hiện tại
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}