using UnityEngine;
using UnityEngine.InputSystem; // ⚠️ BẮT BUỘC phải có dòng này

public class WeaponSwitcher : MonoBehaviour
{
    public enum WeaponType { Gun, Sword }

    [Header("Vũ khí")]
    [SerializeField] private GameObject gunObject;
    [SerializeField] private GameObject swordObject;

    private WeaponType currentWeapon;

    void Start()
    {
        SelectWeapon(WeaponType.Gun);
    }

    void Update()
    {
        // Kiểm tra null để tránh lỗi khi không có bàn phím
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SelectWeapon(WeaponType.Gun);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SelectWeapon(WeaponType.Sword);
        }
    }

    void SelectWeapon(WeaponType weapon)
    {
        currentWeapon = weapon;

        if (gunObject != null)
            gunObject.SetActive(weapon == WeaponType.Gun);

        if (swordObject != null)
            swordObject.SetActive(weapon == WeaponType.Sword);

        Debug.Log($"Đã chuyển sang: {weapon}");
    }

    public WeaponType GetCurrentWeapon() => currentWeapon;
}