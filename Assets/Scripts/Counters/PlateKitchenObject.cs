using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> validKithenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;
    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenOnjectSO) {
        if (!validKithenObjectSOList.Contains(kitchenOnjectSO)) {
            //Not a valid ingredient
            return false;
        } else {
            if (kitchenObjectSOList.Contains(kitchenOnjectSO)) {
                //Already has this type
                return false;
            } else {
                kitchenObjectSOList.Add(kitchenOnjectSO);
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                    kitchenObjectSO = kitchenOnjectSO
                });
                return true;
            }
        }

    }
    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }
}
