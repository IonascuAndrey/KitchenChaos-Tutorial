using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            //There is no a kitchen object
            if(player.HasKitchenObject())
            {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
            {
                //Player not carrying anything
            }
        } else
        {
            //There is a kitchen object here
            if (player.HasKitchenObject())
            {
                //the player is carrying something
            }
            else
            {
                //the player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
                
    }
}