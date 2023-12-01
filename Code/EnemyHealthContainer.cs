using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthContainer : MonoBehaviour
{
    // grab the image for the fillAmountImage for the health bar
    [SerializeField] private Image fillAmountImage;
    public Image FillAmountImage => fillAmountImage;
}
