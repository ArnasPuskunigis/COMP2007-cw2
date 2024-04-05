using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider HealthSlider;

    [Header("Health Settings")]
    public float CurrentHealth;
    [Tooltip("Press D to take damage")]
    public float DamageAmount;
    [Tooltip("Press A to heal")]
    public float HealAmount;

    [Header("Damage and Healing animation curves")]
    public AnimationCurve AddingHealthCurve;
    public AnimationCurve TakingDamageCurve;

    [Header("Speed of Animating the healing and damaging effects")]
    [Range(0,5)]
    public float AddHealthSpeed;
    [Range(0, 5)]
    public float TakeDamageSpeed;


    [Header("Debug data")]
    public float NewHealth;
    public bool UpdatingHealth = false;
    public bool Healing = false;
    public float AnimationTime;
    public float NormalizedValue;

    void Start()
    {
        NewHealth = CurrentHealth;
    }

    void Update()
    {
        //Get user input, A takes damage, D adds health
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Ensure that health does not go below 0
            NewHealth -= DamageAmount;
            if (NewHealth < 0)
            {
                NewHealth = 0;
            }
            //Set updating health to true and healing to false, as the player is taking damage
            UpdatingHealth = true;
            Healing = false;
            AnimationTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //Ensure that health does not go above 100
            NewHealth += HealAmount;
            if (NewHealth > 100)
            {
                NewHealth = 100;
            }
            //Set updating health to true and healing to true, as the player is being healed
            UpdatingHealth = true;
            Healing = true;
            AnimationTime = 0;
        }

        //Esentially if its time to take damage (false on healing) then run this code otherwise there is the else if code for healing
        if (UpdatingHealth == true && Healing == false)
        {
            // Normalise current and target health (These are reversed as you target health is below the current health when taking damage, this is because the graph is the same rather than reversed)
            float normalizedCurrentHealth = NewHealth / 100f;
            float normalizedNewHealth = CurrentHealth / 100f;

            // Ensure that the graph is perfectly evaluated from 0 to 1 and how quickly this should happen
            AnimationTime += Time.deltaTime * TakeDamageSpeed;
            float graphPosition = TakingDamageCurve.Evaluate(AnimationTime);

            // Take the graph position at this specific time and evaluate it between the current and wanted health
            // For example, if you have 50 health and want 70, if the graph is perfectly LINEAR, then 0.5 on the graph would equate to 60 health. (Here it is also reversed also due to the graph being the same but needing the health to go down instead of up)
            CurrentHealth = ((graphPosition * (normalizedCurrentHealth - normalizedNewHealth)) + normalizedNewHealth) * 100;
            
            // Check if the graph has been evaluated completely
            if (graphPosition >= 1)
            {
                // Reset variables to stop this function from running endlessly
                UpdatingHealth = false;
                AnimationTime = 0;
            }
            HealthSlider.value = CurrentHealth;
        }
        else if (UpdatingHealth == true && Healing == true)
        {
            
            float normalizedCurrentHealth = CurrentHealth / 100f;
            float normalizedNewHealth = NewHealth / 100f;

            AnimationTime += Time.deltaTime * AddHealthSpeed;
            float graphPosition = AddingHealthCurve.Evaluate(AnimationTime);

            float currentValue = (graphPosition * (normalizedNewHealth - normalizedCurrentHealth)) + normalizedCurrentHealth;
            if (graphPosition >= 1)
            {
                print("Healing");
                UpdatingHealth = false;
                CurrentHealth = currentValue * 100;
                AnimationTime = 0;
            }
            HealthSlider.value = currentValue * 100f;
        }

    }

}
