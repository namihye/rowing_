using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    public GameObject _Boat;
    public GameObject FinishMenu;
    public GameObject Ranking;
    float BoatDistance;
    Vector3 FirstDistance = new Vector3(0,0,0);
    bool isFinishMenu = true;

    public void FixedUpdate(){
        BoatDistance = Vector3.Distance(FirstDistance, _Boat.transform.position);
        //Debug.Log(BoatDistance);
        
    }

    void LateUpdate(){
        if (BoatDistance>100 && isFinishMenu==true){
            _Boat.GetComponent<BoatController>().enabled = false;
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
