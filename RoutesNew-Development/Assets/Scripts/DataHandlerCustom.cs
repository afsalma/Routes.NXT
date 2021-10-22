using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
public class DataHandlerCustom : MonoBehaviour
{
    List<GameObject> routesDeliveryPoints = new List<GameObject>();
    [SerializeField] GameObject deliveryPointsScreen;
    [SerializeField] GameObject deliveryPointsDetailsScreen;
    [SerializeField] GameObject routesTodayScreen;
    [SerializeField] TMP_InputField userName;
    [SerializeField] TMP_InputField passWord;
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject loginPanelNew;
    public static DataHandlerCustom dataHandler;
    DatabaseReference mDatabaseRef;
    string ID;
    public List<LocationInfo> routesToday = new List<LocationInfo>();
    FirebaseStorage storage;
    StorageReference storage_ref;
    public string date;
    [SerializeField] GameObject routesPrefab;
    [SerializeField] GameObject routesParent;
    [SerializeField] GameObject DeliveryPointsPrefab;
    [SerializeField] GameObject DeliveryPointsParent;
    [SerializeField] GameObject invoiceParent;
    [SerializeField] GameObject invoicePrefab;
    DataSnapshot snapshot;

    /*
    [SerializeField] InputField fuelQuantity;
    [SerializeField] InputField fuelPrice;
    [SerializeField] InputField fueltotalAmount;
   */
    /*
    [SerializeField] TMP_InputField fueltotalAmount;
    [SerializeField] TMP_InputField fueltotalAmount;
    [SerializeField] TMP_InputField fueltotalAmount;
    */

    private void Awake()
    {
       
      
        dataHandler = this;
        date = GetDate();
        loginPanelNew.SetActive(false);
        AuthenticationManager("","");
     //   OnInitialize();
       
     
    }
    public void AuthenticationManager(string username, string password)
    {
        loginPanel.SetActive(false);
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        string u = PlayerPrefs.GetString("username").ToString();
        string p = PlayerPrefs.GetString("password").ToString();


        auth.SignInWithEmailAndPasswordAsync("ansiyahan555@gmail.com", "8138080003").ContinueWith(task =>
        //  auth.SignInWithEmailAndPasswordAsync(username, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            //  writeNewUser(newUser.UserId, "ansu", "ansiyahan");
            
        });
       
        FirebaseDatabase.DefaultInstance.GetReference("users").Child("15-08-2021").Child("Hyzappan").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print("error"); // Handle the error...
            }
            else if (task.IsCompleted)
            {
                snapshot = task.Result;
                print(snapshot.Value.ToString());
            }
            

        });

        StartCoroutine(GetSnapShotDelay());

    }

    IEnumerator GetSnapShotDelay()
    {
        yield return new WaitForSeconds(3f);
        if (snapshot == null)
        {
            StartCoroutine(GetSnapShotDelay());
        }
        else
            DataRetrieved();
    }
      
    private void DataRetrieved()
      {
        
        long routesCount = snapshot.Child("routesToday").ChildrenCount;
        print(routesCount);
        for (int i = 0; i < routesCount; i++)
        {
           
            GameObject game = Instantiate(routesPrefab, routesParent.transform);
            RoutesPrefabButton routesButtonScript= game.AddComponent<RoutesPrefabButton>();
            routesButtonScript.routesToday = routesTodayScreen;
            routesButtonScript.Deliverypoints = deliveryPointsScreen;
            string gdgd = game.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
            game.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = snapshot.Child("routesToday").Child(i.ToString()).Child("route").Value.ToString();
         ///   string text = snapshot.Child("routesToday").Child(i.ToString()).Child("route").Value.ToString();
            print(snapshot.Child("routesToday").Child(i.ToString()).Child("route").Value.ToString());
            /* long deliveryPointsCount = snapshot.Child("routesToday").Child(i.ToString()).Child("deliveryPoints").ChildrenCount;
             for (int k=0;k< deliveryPointsCount;k++)
             {
                 GameObject deliveryPointsGameObject = Instantiate(DeliveryPointsPrefab, DeliveryPointsParent.transform);
                 deliveryPointsGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = snapshot.Child("routesToday").Child(i.ToString()).Child("deliveryPoints").Child(k.ToString()).Value.ToString();
                 routesButtonScript.routesDeliveryPoints.Add(deliveryPointsGameObject);
                 routesDeliveryPoints.Add(deliveryPointsGameObject);
                 deliveryPointsGameObject.SetActive(false);
                 MapRedirection mapScript = deliveryPointsGameObject.AddComponent<MapRedirection>();
                 mapScript.url= snapshot.Child("routesToday").Child(i.ToString()).Child("urls").Child(k.ToString()).Value.ToString();
             }
            */
            long deliveryPointsCount = snapshot.Child("routesToday").Child(i.ToString()).Child("deliveryPoints").ChildrenCount;
            for (int k = 0; k < deliveryPointsCount; k++)
            {
                GameObject deliveryPointsGameObject = Instantiate(DeliveryPointsPrefab, DeliveryPointsParent.transform);
                deliveryPointsGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = snapshot.Child("routesToday").Child(i.ToString()).Child("deliveryPoints").Child(k.ToString()).Child("deliveryPoint").Value.ToString();
                routesButtonScript.routesDeliveryPoints.Add(deliveryPointsGameObject);
                routesDeliveryPoints.Add(deliveryPointsGameObject);
                deliveryPointsGameObject.SetActive(false);
                MapRedirection mapScript = deliveryPointsGameObject.AddComponent<MapRedirection>();
                mapScript.url = snapshot.Child("routesToday").Child(i.ToString()).Child("urls").Child(k.ToString()).Value.ToString();
                InvoiceManager currInvoiceManager = deliveryPointsGameObject.transform.GetChild(3).gameObject.AddComponent<InvoiceManager>();
                currInvoiceManager.deliveryPointsScreen = deliveryPointsScreen;
                currInvoiceManager.deliveryPointDetailsScreen = deliveryPointsDetailsScreen;
                GameObject Invoicespawned = Instantiate(invoicePrefab, invoiceParent.transform);

            }
        }
        // Debug.Log(JsonUtility.ToJson(user));
        //  Debug.Log(user.ToString());
        print("data retrieved");

      }

    public void DeactivateAllDeliveryPoints()
    {
        for (int k = 0; k < routesDeliveryPoints.Count; k++)
        {
            routesDeliveryPoints[k].SetActive(false);
        }


    }



    private string GetDate()
    {
       return  DateTime.Now.ToString("dd-MM-yyyy");
    }

   

    private void OnInitialize()
    {
        var i = PlayerPrefs.GetInt("logined");
        if (i == 1)
        {
            print(PlayerPrefs.GetString("username"));
            print(PlayerPrefs.GetString("password"));
            if (userName.text.ToString() == "ansiyahan555@gmail.com")
            {
                AuthenticationManager(userName.text.ToString(), passWord.text.ToString());
                Debug.Log("equalllll");
            }
        }
        else
        {
            loginPanel.SetActive(true);
        }
    }
    public void SetLoginCredentials()
    {
        PlayerPrefs.SetString("username", userName.text);
        PlayerPrefs.SetString("password", passWord.text);
        PlayerPrefs.SetInt("logined",1);
        OnInitialize();
    }

    /*
    public void fuelExpenseUploader()
    {
      //  print("fuel quantity..."+fuelQuantity.text);
        
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("FuelQuantity").SetValueAsync(fuelQuantity.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("FuelPrice").SetValueAsync(fuelPrice.text);
        mDatabaseRef.Child("users").Child("15-08-2021").Child("Hyzappan").Child("FuelTotalAmount").SetValueAsync(fueltotalAmount.text);


    }
     */
   
   
    /*
    private void writeNewUser(string userId, string name, string email)
        {


   //  User user = new User(name, email);
   //     user.routesToday = routesToday;
   //     string json = JsonUtility.ToJson(user);
   //   date = GetDate();

   // mDatabaseRef.Child("users").Child(date).Child("Hyzappan").SetRawJsonValueAsync(json);
   /*
    FirebaseDatabase.DefaultInstance.GetReference("users").Child("15-08-2021").Child("Hyzappan").GetValueAsync().ContinueWith(task =>
    {
        DataSnapshot snapshot = task.Result;
        DataRetrieved(snapshot);

    });
   */
}



public class User
{
    public string username;
    public string email;
    public float attandance;
    public float age;
    public float mailid;
    public float PhoneNumber;
    public float Name;
    public enum Fuel{petrol,diesel};
    public float FuelQuantity;
    public float FuelPrice;
    public float FuelTotalAmount;
    public enum loading { loading,unloading};
    public float ChargePerPerson;
    public float NumberOfPeople;
    public float TotalAmount;







    public List<string> routesToday = new List<string>() { "route1", "route2" };


    public List<string> routesCovered = new List<string>() { "route1", "route2" };
    public List<string> deliveryPoints = new List<string>() { "route1", "route2" };
    public List<string> customername = new List<string>() { "route1", "route2" };
    public List<string> customernumber = new List<string>() { "route1", "route2" };
    public List<string> Invoice = new List<string>() { "route1", "route2" };
 //   public List<bool> paymentStatus = new List<bool>() { "route1", "route2" };
    public List<string> tollading = new List<string>() { "route1", "route2" };
    



    public User(string username, string email)
    {
        this.username = username;
        this.email = email;
       
    }
}
public class routes
{
    public List<string> deliveryPoints = new List<string>() { "route1", "route2" };
    public List<string> customername = new List<string>() { "route1", "route2" };
    public List<string> customernumber = new List<string>() { "route1", "route2" };
    public List<string> Invoice = new List<string>() { "route1", "route2" };
}

