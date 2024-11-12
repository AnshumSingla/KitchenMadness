using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a script for interfaces
//In interfaces we don't actually include any fuction implementation it is upto the class that implents this interface
public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTranform();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
