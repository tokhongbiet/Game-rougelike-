using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement; // Bắt buộc có để chuyển Scene

public class MainMenuManager : MonoBehaviour
{
    // Điền chính xác tên Scene chơi game của bạn ở đây
   // [SerializeField] private string playSceneName = "Current Scene";

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Đã thoát Game!");
    }

    public void OptionGame()
    {
        Debug.Log("Đã bấm nút Tùy Chọn của Game!");
        //Dùng Để thêm lựa chọn v..v
    }
}