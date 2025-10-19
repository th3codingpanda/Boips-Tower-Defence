using TMPro;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    private float _zoins;
    UIHandler uiHandler;
    public void ChangeMoney(float amount)
    {
        _zoins += amount;
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        uiHandler.ChangeUIText(moneyText,"Zoins: " + _zoins);
    }

    private void Start()
    {
        uiHandler = UIHandler.Instance;
        ChangeMoney(1000);
    }

    public bool CheckMoneyAmount(float amount)
    {
        if (_zoins - amount >= 0)
        {
            return true;
        }
        return false;
    }
}
