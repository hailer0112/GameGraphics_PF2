using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Collider))]
public class HealZone : MonoBehaviour
{
    [Header("Heal Settings")]
    [SerializeField] private int healAmount = 10;
    [SerializeField] private float healInterval = 1.0f;

    [Header("Ambient Effect")]
    [Tooltip("플레이어가 물 위에 있는 동안 계속 재생되는 파티클")]
    [SerializeField] private VisualEffect ambientParticle;
    [SerializeField] private ParticleSystem magicCircle;

    private bool isPlayerInside = false;
    private float healTimer = 0f;

    private void Start()
    {
        ambientParticle.Stop();
        magicCircle.Stop();
    }

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInside = true;
        healTimer = 0f;

        
        if (ambientParticle != null)
        {
            ambientParticle.Play();
        }

        if (magicCircle != null)
        {
            magicCircle.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || !isPlayerInside) return;

        healTimer += Time.deltaTime;

        
        if (healTimer >= healInterval)
        {
            healTimer = 0f;
            ApplyHeal(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInside = false;
        healTimer = 0f;

       
        if (ambientParticle != null)
        {
            ambientParticle.Stop();
        }

        if (magicCircle != null)
        {
            magicCircle.Stop();
        }
    }

    private void ApplyHeal(GameObject player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.Heal(healAmount);
        }
    }
}
