using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    public enum FoodType
    {
        G1,
        G2,
        G3,
        G4,
        G5,
        B1,
        B2
    };
    public FoodType foodType;
    private Rigidbody2D rb2d;
    private float speed = 100;
    private HealthBar healthBar;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void InitializeFood(float foodSpeed, GameObject healthBar)
    {
        speed = foodSpeed;
        this.healthBar = healthBar.GetComponent<HealthBar>();
    }

    void Update()
    {
        rb2d.linearVelocity = new Vector3(0, -speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (foodType)
            {
                case FoodType.G1:
                {
                    healthBar.Heal(10);
                    break;
                }
                case FoodType.G2:
                {
                    healthBar.Heal(10);
                    break;
                }
                case FoodType.G3:
                {
                    healthBar.Heal(20);
                    break;
                }
                case FoodType.G4:
                {
                    healthBar.Heal(20);
                    break;
                }
                case FoodType.G5:
                {
                    healthBar.Heal(30);
                    break;
                }
                case FoodType.B1:
                {
                    healthBar.TakeDamage(10);
                    break;
                }
                case FoodType.B2:
                {
                    healthBar.TakeDamage(20);
                    break;
                }
            }
        }

        if (!other.gameObject.CompareTag("Food"))
        {
            Destroy(gameObject);
        }
    }
}
