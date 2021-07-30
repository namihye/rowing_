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
    public TextMeshProUGUI MainText;

    public TextMeshProUGUI BestRecord;
    public TextMeshProUGUI SecondRecord;
    public TextMeshProUGUI ThirdRecord;
    
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
        MainText.text = (TargetDistance/1000).ToString()+"km RANKING TOP3";
        avgSpeed = BoatDistance / curTime;
        RecordText.text = string.Format("time {0}    speed {1}m/s",
            curTimeText.text, avgSpeed.ToString("F0"));

        if(TargetDistance==1000){
            //현재 기록이 새로운 1등 기록일 때
            if(avgSpeed>PlayerPrefs.GetFloat("BestSpeed_1km",0)){
                //현재 기록을 1등 기록으로 놓고 기존 1,2등 기록을 2,3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_1km",PlayerPrefs.GetFloat("SecondSpeed_1km"));
                PlayerPrefs.SetFloat("SecondSpeed_1km",PlayerPrefs.GetFloat("BestSpeed_1km"));
                PlayerPrefs.SetFloat("BestSpeed_1km",avgSpeed);
                
                PlayerPrefs.SetString("ThirdRecord_1km",PlayerPrefs.GetString("SecondRecord_1km"));
                PlayerPrefs.SetString("SecondRecord_1km",PlayerPrefs.GetString("BestRecord_1km"));
                PlayerPrefs.SetString("BestRecord_1km",RecordText.text);
            }
            //현재 기록이 새로운 2등 기록일 때
            else if(avgSpeed>PlayerPrefs.GetFloat("SecondSpeed_1km",0)){
                //현재 기록을 2등 기록으로 놓고 기존 2등 기록을 3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_1km",PlayerPrefs.GetFloat("SecondSpeed_1km"));
                PlayerPrefs.SetFloat("SecondSpeed_1km",avgSpeed);

                PlayerPrefs.SetString("ThidRecord_1km",PlayerPrefs.GetString("SecondRecord_1km"));
                PlayerPrefs.SetString("SecondRecord_1km",RecordText.text);
            }
            //현재 기록이 새로운 3등 기록일 때
            else if(avgSpeed>PlayerPrefs.GetFloat("ThirdSpeed_1km",0)){
                //현재 기록을 3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_1km",avgSpeed);

                PlayerPrefs.SetString("ThirdRecord_1km",RecordText.text);
            }

            BestRecord.text = PlayerPrefs.GetString("BestRecord_1km");
            SecondRecord.text = PlayerPrefs.GetString("SecondRecord_1km");
            ThirdRecord.text = PlayerPrefs.GetString("ThirdRecord_1km");
        }

        if(TargetDistance==3000){
            //현재 기록이 새로운 1등 기록일 때
            if(avgSpeed>PlayerPrefs.GetFloat("BestSpeed_3km",0)){
                //현재 기록을 1등 기록으로 놓고 기존 1,2등 기록을 2,3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_3km",PlayerPrefs.GetFloat("SecondSpeed_3km"));
                PlayerPrefs.SetFloat("SecondSpeed_3km",PlayerPrefs.GetFloat("BestSpeed_3km"));
                PlayerPrefs.SetFloat("BestSpeed_3km",avgSpeed);
                
                PlayerPrefs.SetString("ThirdRecord_3km",PlayerPrefs.GetString("SecondRecord_3km"));
                PlayerPrefs.SetString("SecondRecord_3km",PlayerPrefs.GetString("BestRecord_3km"));
                PlayerPrefs.SetString("BestRecord_3km",RecordText.text);
            }
            //현재 기록이 새로운 2등 기록일 때
            else if(avgSpeed>PlayerPrefs.GetFloat("SecondSpeed_3km",0)){
                //현재 기록을 2등 기록으로 놓고 기존 2등 기록을 3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_3km",PlayerPrefs.GetFloat("SecondSpeed_3km"));
                PlayerPrefs.SetFloat("SecondSpeed_3km",avgSpeed);

                PlayerPrefs.SetString("ThidRecord_3km",PlayerPrefs.GetString("SecondRecord_3km"));
                PlayerPrefs.SetString("SecondRecord_3km",RecordText.text);
            }
            //현재 기록이 새로운 3등 기록일 때
            else if(avgSpeed>PlayerPrefs.GetFloat("ThirdSpeed_3km",0)){
                //현재 기록을 3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_3km",avgSpeed);

                PlayerPrefs.SetString("ThirdRecord_3km",RecordText.text);
            }

            BestRecord.text = PlayerPrefs.GetString("BestRecord_3km");
            SecondRecord.text = PlayerPrefs.GetString("SecondRecord_3km");
            ThirdRecord.text = PlayerPrefs.GetString("ThirdRecord_3km");
        }

        if(TargetDistance==5000){
            //현재 기록이 새로운 1등 기록일 때
            if(avgSpeed>PlayerPrefs.GetFloat("BestSpeed_5km",0)){
                //현재 기록을 1등 기록으로 놓고 기존 1,2등 기록을 2,3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_5km",PlayerPrefs.GetFloat("SecondSpeed_5km"));
                PlayerPrefs.SetFloat("SecondSpeed_5km",PlayerPrefs.GetFloat("BestSpeed_5km"));
                PlayerPrefs.SetFloat("BestSpeed_5km",avgSpeed);
                
                PlayerPrefs.SetString("ThirdRecord_5km",PlayerPrefs.GetString("SecondRecord_5km"));
                PlayerPrefs.SetString("SecondRecord_5km",PlayerPrefs.GetString("BestRecord_5km"));
                PlayerPrefs.SetString("BestRecord_5km",RecordText.text);
            }
            //현재 기록이 새로운 2등 기록일 때
            else if(avgSpeed>PlayerPrefs.GetFloat("SecondSpeed_5km",0)){
                //현재 기록을 2등 기록으로 놓고 기존 2등 기록을 3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_5km",PlayerPrefs.GetFloat("SecondSpeed_5km"));
                PlayerPrefs.SetFloat("SecondSpeed_5km",avgSpeed);

                PlayerPrefs.SetString("ThidRecord_5km",PlayerPrefs.GetString("SecondRecord_5km"));
                PlayerPrefs.SetString("SecondRecord_5km",RecordText.text);
            }
            //현재 기록이 새로운 3등 기록일 때
            else if(avgSpeed>PlayerPrefs.GetFloat("ThirdSpeed_5km",0)){
                //현재 기록을 3등 기록으로 업데이트
                PlayerPrefs.SetFloat("ThirdSpeed_5km",avgSpeed);

                PlayerPrefs.SetString("ThirdRecord_5km",RecordText.text);
            }

            BestRecord.text = PlayerPrefs.GetString("BestRecord_5km");
            SecondRecord.text = PlayerPrefs.GetString("SecondRecord_5km");
            ThirdRecord.text = PlayerPrefs.GetString("ThirdRecord_5km");
        }

        yield return new WaitForSecondsRealtime(3);
        FinishMenu.SetActive(false);
        Ranking.SetActive(true);
        
    }


    public void Start(){
        TargetDistance = PlayerPrefs.GetInt("TargetDistance");
        
        StartCoroutine("StartCount");
        oldPosition = transform.position;
    }


    public void FixedUpdate(){
        BoatDistance = Vector3.Distance(FirstDistance, _Boat.transform.position);

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
