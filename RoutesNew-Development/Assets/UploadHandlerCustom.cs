using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadHandlerCustom : MonoBehaviour
{
    DatabaseReference mDatabaseRef;
    [SerializeField] InputField fuelQuantity;
    [SerializeField] InputField fuelPrice;
    [SerializeField] InputField fueltotalAmount;
    string selectedFuel;
    [SerializeField] Image petrolButton;
    [SerializeField] Image dieselButton;
    [SerializeField] Sprite highlighter;
    [SerializeField] Sprite ordinary;
    [SerializeField] Image loadingButton;
    [SerializeField] Image unloadingButton;
    string labourType;
    [SerializeField] InputField ChargesPerPerson;
    [SerializeField] InputField countPeople;
    [SerializeField] InputField totalAmount;




    // Start is called before the first frame update
    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void fuelExpenseUploader()
    {
        print("fuel quantity..." + fuelQuantity.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("FuelExpenses").Child("FuelQuantity").SetValueAsync(fuelQuantity.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("FuelExpenses").Child("FuelPrice").SetValueAsync(fuelPrice.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("FuelExpenses").Child("FuelTotalAmount").SetValueAsync(fueltotalAmount.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("FuelExpenses").Child("Fuel").SetValueAsync(selectedFuel);

    }

    public void fuelSetter(string fuel)
    {
        selectedFuel = fuel;
        if (fuel == "Petrol")
        {
            petrolButton.sprite = highlighter;
            dieselButton.sprite = ordinary;
        }
        else
        {
            dieselButton.sprite = highlighter;
            petrolButton.sprite = ordinary;

        }
    }

   
    public void labourSettings(string labour)
    {
        labourType = labour;
        if (labour == "Loading")
        {
            loadingButton.sprite = highlighter;
            unloadingButton.sprite = ordinary;
        }
        else
        {
            unloadingButton.sprite = highlighter;
            loadingButton.sprite = ordinary;

        }
    }


    public void LabourChargesUploader()
    {
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("LabourCharges").Child("ChargesPerPerson").SetValueAsync(ChargesPerPerson.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("LabourCharges").Child("NoOfPeople").SetValueAsync(countPeople.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("LabourCharges").Child("TotalAmount").SetValueAsync(totalAmount.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("LabourCharges").Child("LabourType").SetValueAsync(labourType);

    }







}
