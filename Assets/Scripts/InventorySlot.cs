//This class is meant to be a middle man between the inventoryManager and the ingredients

using System;

[Serializable]
public class InventorySlot
{
    /// <summary>
    /// how much of the Ingredient this slot holds
    /// </summary>
    public int Amount;

    /// <summary>
    /// the ingredient that the slot holds
    /// </summary>
    public Ingredient Ingredient;

    /// <summary>
    /// Sets the slots amount to the given number
    /// </summary>
    /// <param name="newAmount">the number the amount will be set to</param>
    public void SetAmount(int newAmount)
    {
        Amount = newAmount;
    }

    /// <summary>
    /// adds this number to the current amount
    /// </summary>
    /// <param name="amountChange">number to add to the amount</param>
    public void AddAmount(int amountChange)
    {
        Amount += amountChange;
    }

}
