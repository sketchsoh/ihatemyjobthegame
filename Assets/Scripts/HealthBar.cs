using System.Collections;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    [SerializeField]
    private float healthDrainRate;
    private Coroutine healthBarCoroutine;
    private Coroutine redFlashCoroutine;
    [SerializeField]
    private float animDuration;
    
    [SerializeField]
    private GameObject redFlashOverlay;

    private bool passiveDrain;
    
    public AudioClip[] goodFoodSFX;
    public AudioClip[] badFoodSFX;
    
    void Start()
    {
        currentHealth = maxHealth;
        passiveDrain = true;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.transform.localScale = new Vector3(0, 1, 1);
            return;
        }
        if (!passiveDrain) return;
        float newHealth = currentHealth - healthDrainRate;
        UpdateHealthBar(newHealth);
    }

    public void TakeDamage(float damage)
    {
        float health  = currentHealth - damage;
        UpdateHealthBar(health);
        SoundManager.Instance.PlayRandomSFXClip(badFoodSFX, transform, true, 0.5f);
        LMotion.Create(1.0f, 0.0f, 0.5f).WithEase(Ease.InOutQuad).BindToColorA(redFlashOverlay.GetComponent<SpriteRenderer>());
    }

    public void Heal(float heal)
    {
        float  health = currentHealth + heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthBar(health);
        SoundManager.Instance.PlayRandomSFXClip(goodFoodSFX, transform, true, 1f);

    }

    private void UpdateHealthBar(float newHealth)
    {
        if (healthBarCoroutine != null)
        {
            StopCoroutine(healthBarCoroutine);
        }
        float healthPercentage = newHealth / maxHealth;
        healthBarCoroutine = StartCoroutine(HealthBarLerp(healthPercentage, animDuration, newHealth));
    }

    private IEnumerator HealthBarLerp(float newHealthPercentage, float animationDuration, float finalHealth)
    {
        float currHealthPercentage = currentHealth / maxHealth;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float percentageComplete = elapsedTime / animationDuration;
            float healthBarFill =  Mathf.Lerp(currHealthPercentage, newHealthPercentage, percentageComplete);
            float newHealthBarFill = healthBarFill * 16.0f;
            if (newHealthBarFill <= 0)
            {
                gameObject.transform.localScale = new Vector3(0, 1, 1);
                currentHealth = 0;
                //TODO: Add Lose Screen
                Debug.Log("LOSE by game");
                StopCoroutine(healthBarCoroutine);
                SoundManager.Instance.TransitionMusicClip(MusicType.Lose, 0.5f);
                SceneManager.LoadScene("Lose3");
            }
            gameObject.transform.localScale = new Vector3(newHealthBarFill, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            passiveDrain = false;
            currentHealth = healthBarFill * maxHealth;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        passiveDrain = true;
        currentHealth = finalHealth;
        gameObject.transform.localScale = new Vector3(newHealthPercentage * 16.0f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        healthBarCoroutine = null;
    }
}
