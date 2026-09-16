using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth = 100;
    public int CurrentHealth => currentHealth;

    public void Heal(int amount)
    {
        currentHealth += amount;
        Debug.Log($"Player healed! Current Health: {currentHealth}");
    }
}
