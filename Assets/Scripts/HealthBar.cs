using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    [SerializeField]
    private float healthDrainRate;
    private Coroutine healthBarCoroutine;
    [SerializeField]
    private float animDuration;

    private bool passiveDrain;
    void Start()
    {
        currentHealth = maxHealth;
        passiveDrain = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            Heal(10);
        }

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
    }

    public void Heal(float heal)
    {
        float  health = currentHealth + heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthBar(health);
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
                StopCoroutine(healthBarCoroutine);
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
