using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider Slider;
    public GameObject HealthSlider;
    public Stats Card;
    public Image Fill;

    bool IsOn = false;

    void Start()
    {
        //if(Death) Death.SetActive(false);
        Slider.maxValue = Card.Health;
        Slider.minValue = 0;

        if(Fill) Fill.color = Color.red;
    }

    void Update()
    {
        Slider.value = Card.CurrentHealth;
        //Slider3D.value = Slider2D.value;

        if(Card.CurrentHealth < 1 || Card == null) 
        {
            //if(Death) Death.SetActive(true);
            HealthSlider.SetActive(false);
        }
    }

    public void TurnOn()
    {
        IsOn = true;
    }

    public void TurnOff()
    {
        IsOn = false;
    }
}