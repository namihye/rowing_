using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Distance : MonoBehaviour
{
    public GameObject _Boat;
    public GameObject FinishMenu;
    public GameObject Ranking;
    public GameObject StartUI;
    public GameObject _Timer;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI curTimeText;
    public TextMeshProUGUI curDistanceText;

    float curTime;
    float BoatDistance;
    Vector3 FirstDistance = new Vector3(0,0,0);
    bool isFinishMenu = true;

    IEnumerator StartCount(){
        StartUI.SetActive(true);
        //countdown 3으로 변경
        countText.text = "3";
        countText.gameObject.SetActive(true);

        //1초 뒤 countdowm 2로 변경
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);
        countText.text = "2";
        countText.gameObject.SetActive(true);

        //1초 뒤 countdowm 1로 변경
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);
        countText.text = "1";
        countText.gameObject.SetActive(true);

        //1초 뒤 countdown go로 변경
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);
        countText.text = "GO!";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);

        StartCoroutine("Timer");
        _Boat.GetComponent<BoatController>().enabled = true;

    }

    IEnumerator Timer(){
        _Timer.SetActive(true);
        while(true){
            curTime+=Time.deltaTime;
            curTimeText.text = string.Format("{0:00}:{1:00.00}",
                (int)(curTime/60%60), curTime%60);;
            yield return null;
        }
    }

    IEnumerator CalDistance(){
        curDistanceText.text = BoatDistance.ToString("F0")+"m";
        yield return null;
    }

    public void Start(){
        StartCoroutine("StartCount");
    }
    

    public void FixedUpdate(){
        BoatDistance = Vector3.Distance(FirstDistance, _Boat.transform.position);
        //Debug.Log(BoatDistance);
        StartCoroutine("CalDistance");
        
    }

    void LateUpdate(){
        if (BoatDistance>100 && isFinishMenu==true){
            _Boat.GetComponent<BoatController>().enabled = false;
            StopCoroutine("Timer");
            StopCoroutine("CalDistance");
            FinishMenu.SetActive(true);
            Invoke("Delay",3f);
            
            
        }
        
    }

    private void Delay(){
        isFinishMenu = false;
        FinishMenu.SetActive(false);        
        Ranking.SetActive(true);
    }

}
