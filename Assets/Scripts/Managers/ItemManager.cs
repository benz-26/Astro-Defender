using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public int itemAmount;

    public TextMeshProUGUI txtItemAmount;

    private void Start()
    {
/*        itemAmount = 0;*/
        txtItemAmount.text = "X" + itemAmount.ToString();
    }

    private void Update()
    {
        txtItemAmount.text = "X" + itemAmount.ToString();
    }
}
