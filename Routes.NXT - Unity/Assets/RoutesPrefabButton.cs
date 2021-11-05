using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoutesPrefabButton : MonoBehaviour
{
   public Button routesButton;
   public GameObject Deliverypoints;
   public GameObject routesToday;
   public List<GameObject> routesDeliveryPoints = new List<GameObject>();
   // [SerializeField] GameObject routesToday;
    // Start is called before the first frame update
    void Start()
    {
        routesButton = GetComponentInChildren<Button>();
        routesButton.onClick.AddListener(RoutesButtonClicked);
    }

    void RoutesButtonClicked()
    {
        WindowNavigation.navigation.NavigationSwapper(Deliverypoints, routesToday);
        WindowNavigation.navigation.RoutesButtonClicked(routesToday, Deliverypoints);
        DataHandlerCustom.dataHandler.DeactivateAllDeliveryPoints();
        for (int k = 0; k < routesDeliveryPoints.Count; k++)
        {
            routesDeliveryPoints[k].SetActive(true);
        }
            // routesToday.SetActive(false);
            //  Deliverypoints.SetActive(true);

    }





    }
