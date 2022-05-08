using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;

public class CoroutineWithData {
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;
    public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run() {
        while(target.MoveNext()) {
            result = target.Current;
            yield return result;
        }
    }
}
public class GetLocationWeather : MonoBehaviour
{

    public UnityEngine.UI.RawImage iconTemp;
    public UnityEngine.Experimental.Rendering.Universal.Light2D glightTemp;

    private static string userCity = "";
    private static bool daynight = false;
    
    //General Api Variables
    private const string apiKey = "879df27b8c2cba957cdbd5f98f706a1c";

    public string wetaherURL = "";
    public IEnumerator Weather(string uri)
    {
        
        using (UnityWebRequest request1 = UnityWebRequest.Get(wetaherURL))
        {
            yield return request1.SendWebRequest();

            if (request1.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request1.error);
            }
            else
            {
                string result = request1.downloadHandler.text;

                if(result.Length < 30 ){
                    Debug.Log("Algo salio mal");
                }else{
                    Debug.Log(result);
                    JObject jsonData = JObject.Parse(result);
                    string checkDay = jsonData["weather"][0]["icon"].ToString();
                    UnityEngine.Experimental.Rendering.Universal.Light2D glight = glightTemp;
                    if (checkDay.Contains("d"))
                    {
                        glight.intensity = 1f;
                    }else if (checkDay.Contains("n"))
                    {
                        glight.intensity = 0.27f;
                    }

                    string mediaURL = "http://openweathermap.org/img/wn/"+ checkDay +"@2x.png";
                    UnityWebRequest imaqge = UnityWebRequestTexture.GetTexture(mediaURL);
                    yield return imaqge.SendWebRequest();
                    if(imaqge.result == UnityWebRequest.Result.ConnectionError) {
                        Debug.Log(imaqge.error);
                    }else {

                        Texture icon = ((DownloadHandlerTexture)imaqge.downloadHandler).texture;
                        UnityEngine.UI.RawImage iconTime = iconTemp;
                        iconTime.texture = icon;
                    }
                }
            }
        }
    }

    //trabajo con api de datos del tiempo


/*
    public IEnumerator DayNight(string uri, System.Action<bool> light)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string result = request.downloadHandler.text;
                bool check = false;

                if(result.Length < 30 ){
                    Debug.Log("Algo salio mal");
                }else{
                    Debug.Log(result);
                    JObject jsonData = JObject.Parse(result);
                
                    string daytime = jsonData["dt"].ToString();

                    string sunrise = jsonData["sys"]["sunrise"].ToString();

                    Debug.Log(int.Parse(daytime));
                    if(float.Parse(daytime) - int.Parse(sunrise) < 0)
                    {
                        check = false;
                    }else if(int.Parse(daytime) - int.Parse(sunrise) >= 0)
                    {
                        check = true;
                    }
                    light(check);
                    yield return light;
                }

                
                
            }
        }
    }
    */

    private float nextActionTime = 0.0f;
    public float period = 600f;

    void Start(){
        userCity = PassData.getCity();
        wetaherURL = "http://api.openweathermap.org/data/2.5/weather?q="+ userCity +"&APPID=" + apiKey;
        StartCoroutine(Weather(wetaherURL));
    } 
}