using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvoiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    Button arrivedButton;
    public GameObject deliveryPointsScreen;
    public GameObject deliveryPointDetailsScreen;
    public List<GameObject> invoiceBills = new List<GameObject>();
    void Start()
    {
        arrivedButton = GetComponent<Button>();
        arrivedButton.onClick.AddListener(arrivedClicked);

    }

    void arrivedClicked()
    {
        WindowNavigation.navigation.NavigationSwapper(deliveryPointDetailsScreen, deliveryPointsScreen);
        WindowNavigation.navigation.RoutesButtonClicked(deliveryPointsScreen, deliveryPointDetailsScreen);
    }
}
