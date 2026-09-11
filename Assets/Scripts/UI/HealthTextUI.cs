using UnityEngine;
using TMPro;

public class HealthTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private CharacterScript playerScript;

    private void Start()
    {
        if (playerScript != null)
        {
 
            playerScript.OnHealthChanged += UpdateHealthUI;

        }
    }

    private void OnDestroy()
    {
        if (playerScript != null)
        {
            playerScript.OnHealthChanged -= UpdateHealthUI;
        }
    }

    private void UpdateHealthUI(float healthRatio)
    {
        if (healthText != null && playerScript != null)
        {
            healthText.text = playerScript.HP.ToString();
        }
    }
}