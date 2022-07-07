using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualItem : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int typeId;
    [SerializeField] private float itemPrice;
    [SerializeField] private float itemWeight;
    [SerializeField] private float itemHealth;
    [SerializeField] private float itemDps;
    [SerializeField] private Image portrait;
    [SerializeField] private Sprite portraitTrash;
    [SerializeField] private GameObject priceCircle;
    [SerializeField] private GameObject weightCircle;
    [SerializeField] private GameObject healthCircle;
    [SerializeField] private GameObject dpsCircle;
    [SerializeField] private Text priceText;
    [SerializeField] private Text weightText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text dpsText;


    //Show the price
    public void SetPrice(float cost)
    {
        itemPrice = cost;
        priceCircle.SetActive(true);
        priceText.text = Mathf.RoundToInt(cost).ToString();
    }
        
    public float GetPrice()
    {
        return itemPrice;
    }

    //Show the weight
    public void SetWeight(float weight)
    {
        itemWeight = weight;
        weightCircle.SetActive(true);
        weightText.text = Mathf.RoundToInt(itemWeight).ToString();
    }

    public float GetWeight()
    {
        return itemWeight;
    }

    //Show the health
    public void SetHealth(float health)
    {
        itemHealth = health;
        healthCircle.SetActive(true);
        healthText.text = Mathf.RoundToInt(itemHealth).ToString();
    }

    public float GetHealth()
    {
        return itemHealth;
    }

    //Show the DPS
    public void SetDPS(int damage)
    {
        itemDps = damage;
        dpsCircle.SetActive(true);
        dpsText.text = Mathf.RoundToInt(damage).ToString();
    }

    public float GetDPS()
    {
        return itemDps;
    }

    public void SetImage(Sprite image)
    {
        portrait.sprite = image;
    }

    public void SetImageTrash(Sprite image)
    {
        portraitTrash = image;
    }

    public void ApplyImageTrash()
    {
        if(portraitTrash != null)
        {
            portrait.sprite = portraitTrash;
            typeId = GameManager.Instance.GetTrashType();
        }
    }

    public void SetTypeId(int type)
    {
        typeId = type;
    }

    public int GetTypeId()
    {
        return typeId;
    }

    public void SetId (int number)
    {
        id = number;
    }

    public int GetId()
    {
        return id;
    }
}
