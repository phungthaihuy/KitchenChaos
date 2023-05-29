using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter
{
    
    [SerializeField] private CuttingKitchenObjectSO[] CuttingKitchenObjectSOArray;
    private float cuttingProgress;
    public static event EventHandler OnAnyCut;
    public event EventHandler OnCut;
    public event EventHandler<OnProgressBarChangedEventArgs> OnProgressBarChanged;
    public class OnProgressBarChangedEventArgs : EventArgs
    {
        public float progressBarNomalized;
    }

    public override void Interact(Playerr playerr)
    {
        if (!HasKitchenObject())
        {
            //Counter khong co kitchenObject
            if (playerr.HasKitchenObject())
            {
                if (HasKitchenObjectSOCanSlices(playerr.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player dang giu kitchenObject co the cat dc
                    cuttingProgress = 0;
                    playerr.GetKitchenObject().SetKitchenObjectParent(this);

                    CuttingKitchenObjectSO cuttingKitchenObjectSO = GetCuttingKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressBarChanged?.Invoke(this, new OnProgressBarChangedEventArgs
                    {
                        progressBarNomalized = (float)cuttingProgress / cuttingKitchenObjectSO.maxCuttingProgress
                    });
                }
                else
                {
                    Debug.Log("This kitchenObject can't slices!");
                }
            }
            else
            {
                //player khong giu kitchenObject
            }
        }
        else
        {
            //Counter co kitchenObject
            if (!playerr.HasKitchenObject())
            {
                //player khong co kitchenObject
                GetKitchenObject().SetKitchenObjectParent(playerr);
            }
            else
            {
                if (playerr.GetKitchenObject().TryGetPlate(out PlatesKitchenObject platesKitchenObject)) //player handle a plate
                {
                    if (platesKitchenObject.TryAddIngredient(this.GetKitchenObject().GetKitchenObjectSO()))//add kitchenObject on counter to plate which player is handle
                    {
                        GetKitchenObject().DestroyKitchenObject(); //destroy visual on counter
                    }
                }
                
            }
        }
    }

    public override void InteractAlternate(Playerr playerr)
    {
        if (HasKitchenObject() && HasKitchenObjectSOCanSlices(GetKitchenObject().GetKitchenObjectSO())) //tren cutting counter co kitchen object co the cat dc 
        {
            
            cuttingProgress ++;

            CuttingKitchenObjectSO cuttingKitchenObjectSO = GetCuttingKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            OnProgressBarChanged?.Invoke(this, new OnProgressBarChangedEventArgs
            {
                progressBarNomalized = (float)cuttingProgress / cuttingKitchenObjectSO.maxCuttingProgress

            });

            

            if (cuttingProgress >= cuttingKitchenObjectSO.maxCuttingProgress)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputCuttingKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO()); //khai bao

                GetKitchenObject().DestroyKitchenObject(); // huy kitchenObjectSO input

                Transform kitchenObjectTransform = Instantiate(outputKitchenObjectSO.prefab);// spawn kitchenObjectSO output
                kitchenObjectTransform.localPosition = Vector3.zero;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this); //setParent cho kitchenObjectSO output 
            }
            
        }
    }

    private bool HasKitchenObjectSOCanSlices(KitchenObjectSO kitchenObjectSO)
    {
        CuttingKitchenObjectSO cuttingKitchenObjectSO = GetCuttingKitchenObjectSO(kitchenObjectSO);
        return cuttingKitchenObjectSO != null;
    }

    private KitchenObjectSO GetOutputCuttingKitchenObjectSO(KitchenObjectSO kitchenObjectSO) // lay cuttingKitchenObjectSO.output
    {
        CuttingKitchenObjectSO cuttingKitchenObjectSO = GetCuttingKitchenObjectSO(kitchenObjectSO);
        if (cuttingKitchenObjectSO != null)
        {
            return cuttingKitchenObjectSO.output;
        }
        return null;
    }

    private CuttingKitchenObjectSO GetCuttingKitchenObjectSO(KitchenObjectSO kitchenObjectSO) // lay cuttingKitchenObjectSO can slices
    {
        foreach (CuttingKitchenObjectSO cuttingKitchenObjectSO in CuttingKitchenObjectSOArray)
        {
            if (cuttingKitchenObjectSO.input == kitchenObjectSO)
            {
                return cuttingKitchenObjectSO;
            }
        }
        return null;
    }
}
