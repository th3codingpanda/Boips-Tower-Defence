using TMPro;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    private float _zoins;
    public void ChangeMoney(float amount)
    {
        _zoins += amount;
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        moneyText.text = "Zoins: " + _zoins;
    }

    private void Start()
    {
        ChangeMoney(1000);
    }
}
