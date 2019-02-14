using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armour item", menuName = "Inventory/Armour")]
public class ArmourItem : Equipment
{
    public int armourRating;
    public bool isEnchanted;
    public ArmourEnchant currentEnchantment;

    public void Enchantment()
    {
        switch (currentEnchantment)
        {
            case ArmourEnchant.FireResistance:

                break;
        }
    }

    public override void UseItem()
    {
        base.UseItem();
        if (isEnchanted)
        {
            Enchantment();
        }
    }

    public enum ArmourEnchant
    {
        FireResistance,
        ColdResistance,
        FireImmunity,
        ColdImmunity,
        Durable,
    }
}
