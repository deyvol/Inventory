using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualItem : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int typeId;
    [SerializeField] private float price;
    [SerializeField] private Image portrait;
    [SerializeField] private Sprite portraitTrash;
    [SerializeField] private GameObject priceCircle;
    [SerializeField] private Text priceText;

    public void SetPrice(float cost)
    {
        price = cost;
        priceCircle.SetActive(true);
        priceText.text = Mathf.RoundToInt(cost).ToString();
    }

    public float GetPrice()
    {
        return price;
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
