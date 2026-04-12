using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    [SerializeField] private static int coinAmount = 0;
    [SerializeField] private GameObject storePanel;

    public void AddCoins(int amount)
    {
        coinAmount += amount;
    }

    public void SubtractCoins(int amount)
    {
        coinAmount -= amount;
    }

    public void OpenStore()
    {
        storePanel.SetActive(true);
    }

    public bool BuyPowerup(int price)
    {
        if (coinAmount >= price)
        {
            //confirm button here?
            SubtractCoins(price);
            return true;
        }
        //not enough coins text here?
        return false;
    }
}
