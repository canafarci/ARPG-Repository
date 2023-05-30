using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Inventories;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/RPG.UI.InventorySystem/Healing Action Item"))]
public class HealthActionItem : ActionItem
{
    [SerializeField] float healingAmount;
    public override void Use(GameObject user)
    {
        base.Use(user);
        user.GetComponent<PlayerHealth>().Heal(healingAmount);
    }
}
