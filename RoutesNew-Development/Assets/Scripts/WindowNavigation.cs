using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowNavigation : MonoBehaviour
{
    public static WindowNavigation navigation;
    [Header("Buttons")]
    #region 

    [SerializeField]Button BackButton;
    [SerializeField]Button fuelExpenseButton;
    [SerializeField]Button foodExpenseButton;
    [SerializeField]Button worshopButton;
    [SerializeField]Button tollChargesButton;
    [SerializeField]Button LabourChargesButton;
    [SerializeField]Button startTripButton;
    [SerializeField]Button NextCustomerButton;
    [SerializeField]Button routesTodayButton;
    [SerializeField]Button routesCoveredButton;
    [SerializeField]Button attendanceLogButton;
    [SerializeField]Button profileUpdationButton;
    [SerializeField]Button ExpenseManagerButton;
    [SerializeField] Button VerifyButton;
    [SerializeField] Button paymentProceed;
    [SerializeField] Button updateButton;
    #endregion

    #region 
    [Header("Next-Windows")]
    [SerializeField]GameObject fuelExpenseClicked;
    [SerializeField]GameObject foodExpenseClicked;
    [SerializeField]GameObject worshopButtonClicked;
    [SerializeField]GameObject tollChargesButtonClicked;
    [SerializeField]GameObject LabourChargesButtonClicked;
    [SerializeField]GameObject startTripButtonClicked;
    [SerializeField]GameObject NextCustomerButtonClicked;
    [SerializeField]GameObject routesTodayButtonClicked;
    [SerializeField]GameObject routesCoveredButtonClicked;
    [SerializeField]GameObject attendanceLogButtonClicked;
    [SerializeField]GameObject profileUpdationButtonClicked;
    [SerializeField]GameObject ExpenseManagerButtonClicked;
    [SerializeField]GameObject VerifyButtonClicked;
    [SerializeField]GameObject paymentPrceedButtonClicked;
    [SerializeField] GameObject paidButtonObject;
    [SerializeField] GameObject updateButtonObject;
    #endregion

    #region 
    [Header("current Windows")]
    [SerializeField]GameObject ExpenseManagerWindow;
    [SerializeField]GameObject StartTripWindow;
    [SerializeField]GameObject orderPlacingWindow;
    [SerializeField]GameObject WelcomeScreen;
    [SerializeField]GameObject driverVerification;
    [SerializeField] GameObject InvoiceScreen;
    // [SerializeField]GameObject ;

    #endregion

    [SerializeField] List<GameObject> LastWindow = new List<GameObject>();
    [SerializeField] GameObject presentWindow;
    [SerializeField] int lastWindowIndex=-1;
    [SerializeField] GameObject AddTollPanel;
    [SerializeField] TMP_Dropdown tollDropDown;
    [SerializeField] TMP_InputField tollAddition;


    private void Awake()
    {
        if(navigation==null)
        navigation = this;
        
    }
    private void Start()
    {
        fuelExpenseButton.onClick.AddListener(() => NextWindowGenerator(fuelExpenseClicked, ExpenseManagerWindow));
        foodExpenseButton.onClick.AddListener(() => NextWindowGenerator(foodExpenseClicked, ExpenseManagerWindow));
        worshopButton.onClick.AddListener(() => NextWindowGenerator(worshopButtonClicked, ExpenseManagerWindow));
        tollChargesButton.onClick.AddListener(() => NextWindowGenerator(tollChargesButtonClicked, ExpenseManagerWindow));
        LabourChargesButton.onClick.AddListener(() => NextWindowGenerator(LabourChargesButtonClicked, ExpenseManagerWindow));
        startTripButton.onClick.AddListener(() => NextWindowGenerator(startTripButtonClicked, StartTripWindow));
        NextCustomerButton.onClick.AddListener(() => NextWindowGenerator(NextCustomerButtonClicked, orderPlacingWindow));
        routesTodayButton.onClick.AddListener(() => NextWindowGenerator(routesTodayButtonClicked, WelcomeScreen));
      //  routesTodayButton.onClick.AddListener(DataHandlerCustom.dataHandler.DeactivateAllDeliveryPoints);
        routesCoveredButton.onClick.AddListener(() => NextWindowGenerator(routesCoveredButtonClicked, WelcomeScreen));
        attendanceLogButton.onClick.AddListener(() => NextWindowGenerator(attendanceLogButtonClicked, WelcomeScreen));
        profileUpdationButton.onClick.AddListener(() => NextWindowGenerator(profileUpdationButtonClicked, WelcomeScreen));
        ExpenseManagerButton.onClick.AddListener(() => NextWindowGenerator(ExpenseManagerButtonClicked, WelcomeScreen));
        VerifyButton.onClick.AddListener(() => NextWindowGenerator(VerifyButtonClicked, driverVerification));
        paymentProceed.onClick.AddListener(() => NextWindowGenerator(paymentPrceedButtonClicked, InvoiceScreen));
        BackButton.onClick.AddListener(BackButtonClicked);
        updateButton.onClick.AddListener(PaidUpdateButtonClicked);
      //  arrivedButton.onClick.AddListener(() => NextWindowGenerator(arrivedButtonClicked, driverVerification));
        //        QRCodeScanner.onClick.AddListener(QRScanner);
    }

    void NextWindowGenerator(GameObject nextWindow, GameObject currentWindow)
    {
        currentWindow.SetActive(false);
        lastWindowIndex += 1;
        LastWindow.Add(currentWindow);
        nextWindow.SetActive(true);
        presentWindow = nextWindow;
    }

    void QRScanner()
    {


    }
        void BackButtonClicked()
    {
        if (lastWindowIndex > -1)
        {
            presentWindow.SetActive(false);

            LastWindow[lastWindowIndex].SetActive(true);
            if (lastWindowIndex != -1)
            {
                presentWindow = LastWindow[lastWindowIndex];
            }
            LastWindow.RemoveAt(lastWindowIndex);
            lastWindowIndex -= 1;
        }
    }


    public void NavigationSwapper(GameObject nextWindow, GameObject currentWindow)
    {
        LastWindow.Add(currentWindow);
        presentWindow = nextWindow;
        lastWindowIndex += 1;
    }

    public void RoutesButtonClicked(GameObject nextWindow, GameObject currentWindow)
    {
        nextWindow.SetActive(false);
        currentWindow.SetActive(true);
    }


    public void AddTollButtonClicked()
    {
        AddTollPanel.SetActive(true);

    }

    List<string> tollOptions = new List<string>();
    
    public void tollAdEndEdit()
    {
        AddTollPanel.SetActive(false);
        tollOptions.Add(tollAddition.text);
        tollDropDown.AddOptions(tollOptions);
    }

    public void PaidUpdateButtonClicked()
    {
        
        
        updateButtonObject.SetActive(false);
        paidButtonObject.SetActive(true);
        StartCoroutine(BacRedirectionDelay());
    }

    IEnumerator BacRedirectionDelay()
    {
        yield return new WaitForSeconds(2f);
        BackButtonClicked();
        BackButtonClicked();
        BackButtonClicked();

    }
}