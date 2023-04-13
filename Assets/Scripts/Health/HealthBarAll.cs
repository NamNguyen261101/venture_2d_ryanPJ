using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAll : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Color low;
    private Color high;
    private Vector3 offset;


    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth );
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponent<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

    }
}
