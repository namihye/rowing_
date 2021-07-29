using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public TextMeshProUGUI curSpeedText;
    public TextMeshProUGUI RecordText;
    public TextMeshProUGUI BestRecord;
    

    float curTime;
    float BoatDistance;
    float speed;
    float avgSpeed;
    int TargetDistance;
    
    Vector3 FirstDistance = new Vector3(0,0,0);
    Vector3 currentPosition;
    Vector3 oldPosition;

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

    IEnumerator CalVelocity(){
        currentPosition = transform.position;
        float distance = Vector3.Distance(oldPosition,currentPosition);
        speed = distance / Time.deltaTime;
        curSpeedText.text = speed.ToString("F0")+"m/s";
        oldPosition = currentPosition;
        yield return null;
    }

    IEnumerator curScore(){
        isFinishMenu = false;
        avgSpeed = BoatDistance / curTime;
        RecordText.text = string.Format("time {0}    dist {1}km    speed {2}m/s",
            curTimeText.text, (TargetDistance/1000).ToString(), avgSpeed.ToString("F0"));

        if(TargetDistance==1000){
            if(avgSpeed>PlayerPrefs.GetFloat("BestSpeed",0)){
                PlayerPrefs.SetFloat("BestSpeed",avgSpeed);
                PlayerPrefs.SetString("BestRecord",RecordText.text);
            }
        }

        BestRecord.text = PlayerPrefs.GetString("BestRecord");
        // Ranking_second.text = PlayerPrefs.GetString("Rankig_second");
        // Ranking_third.text = PlayerPrefs.GetString("Rankig_third");

        yield return new WaitForSecondsRealtime(3);
        FinishMenu.SetActive(false);
        Ranking.SetActive(true);
        
    }

    // float GetBestSpeed(){
    //     float BestSpeed = PlayerPrefs.GetFloat("BestSpeed",0);
    //     return BestSpeed;
    // }

    // float GetSpeed_second(){
    //     float Speed_second = PlayerPrefs.GetFloat("Speed_second",0);
    //     return Speed_second;
    // }

    // float GetSpeed_third(){
    //     float Speed_third = PlayerPrefs.GetFloat("Speed_third",0);
    //     return Speed_third;
    // }


    public void Start(){
        TargetDistance = PlayerPrefs.GetInt("TargetDistance");
        
        // Rowing rowing = GameObject.Find("TargetDistance").GetComponent<Rowing>();
        // _TargetDistance = int.Parse(rowing. isTargetDistance);
        StartCoroutine("StartCount");
        oldPosition = transform.position;
    }
    

    public void FixedUpdate(){
        BoatDistance = Vector3.Distance(FirstDistance, _Boat.transform.position);
        //Debug.Log(BoatDistance);
        StartCoroutine("CalDistance");
        StartCoroutine("CalVelocity");
        
    }

    void LateUpdate(){
        
        if (BoatDistance > TargetDistance/10 && isFinishMenu == true){
            _Boat.GetComponent<BoatController>().enabled = false;
            StopCoroutine("Timer");
            StopCoroutine("CalDistance");
            StopCoroutine("CalVelocity");
            FinishMenu.SetActive(true);
            StartCoroutine("curScore");
            
            
        }
        
    }

}
