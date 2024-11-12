using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We can extend only one base class but as for interfaces we can extend as many according to the need
//We used instances in this one so that both counter and player won't become the parent of the kitchen object
public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;


    private KitchenObject kitchenObject;

    //We added override keyword as we don't want to hide the base function and override it instead
    public override void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            //Give the obeject to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }


    public Transform GetKitchenObjectFollowTranform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}