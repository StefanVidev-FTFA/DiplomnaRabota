using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIScrpt : MonoBehaviour {
	// Use this for initialization
	public Material PassedCell;
	GameObject AIUnit;
	GameObject StartBlock;
	GameObject FinishBlock;
	GameObject[] ArrOfAllPassableSlots;
	GameObject MyStep;
	GameObject[] LifeTimeSteps=null;
	GameObject[][] LifeTimesRecords=null;
	 Image PauseGO;
	 Image PlayGO;
	float Starttime;
	bool RunSimulation = false;
	public GameObject CGO;
	void Start () {//----------------------------------- first we locate the AIunit at the start current start block
		LifeTimeSteps=null;
		PauseGO = GameObject.Find("PauseImg").GetComponent<Image>();
		PlayGO = GameObject.Find("PlayImg").GetComponent<Image>();
		PauseGO.enabled=true;
		PlayGO.enabled = false;
		Starttime = Time.time;
		sources srcs = gameObject.GetComponent<sources>();//---------- connecting with the sources script
		StartBlock= GameObject.FindGameObjectWithTag ("StartUnit").gameObject;
		FinishBlock= GameObject.FindGameObjectWithTag ("FinishUnit").gameObject;
		MyStep = StartBlock;
		LifeTimeSteps = srcs.ArrPush (LifeTimeSteps, MyStep);
		AIUnit = transform.gameObject;
		AIUnit.transform.position = StartBlock.transform.position;
		AIUnit.transform.position = new Vector3 (AIUnit.transform.position.x, AIUnit.transform.position.y + 1.2f, AIUnit.transform.position.z);
		ArrOfAllPassableSlots = GameObject.FindGameObjectsWithTag ("FreeSpace");
		ArrOfAllPassableSlots = srcs.ArrPush (ArrOfAllPassableSlots, FinishBlock);
	}
	
	// Update is called once per frame
	void Update () {
        //CGO.GetComponent<Text>().text = LifeTimesRecords.Length.ToString();
        if (LifeTimesRecords != null)
        {
			CGO.GetComponent<Text>().text = LifeTimesRecords.Length.ToString();
        }
        else
        {
			CGO.GetComponent<Text>().text = "0";
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (RunSimulation == true)
			{
				RunSimulation = false;
				PauseGO.enabled=true;
				PlayGO.enabled=false;
			}
			else
			{
				RunSimulation = true;
				PauseGO.enabled=false;
				PlayGO.enabled=true;
			}

		}
		sources srcs = gameObject.GetComponent<sources>();
		float SimSpeed = 1-(GameObject.Find("SpeedSlider").GetComponent<Slider>().value);
		if (Time.time - Starttime >= SimSpeed && RunSimulation==true) {//-------------------- TIMER START
			Starttime = Time.time;//------------- for the timer
			MyStep = srcs.CalcIntelNextStep(MyStep,ArrOfAllPassableSlots,LifeTimeSteps,LifeTimesRecords,PathFinder.BestPath.Length);
			LifeTimeSteps = srcs.ArrPushV2 (LifeTimeSteps, MyStep);
			if (MyStep != FinishBlock && MyStep != StartBlock) {
				MyStep.GetComponent<MeshRenderer> ().material = PassedCell;
			}
			AIUnit.transform.position = new Vector3 (MyStep.transform.position.x, MyStep.transform.position.y + 1.2f, MyStep.transform.position.z);
			//------------------------------------------------------------------------------- WHEN WE REACH THE FINISH START
			if (MyStep == FinishBlock) {
				IndicCon IC = GameObject.Find("PlatForm").GetComponent<IndicCon>();
				IC.ReColor();
				GameObject[] CleanLifetimePath = srcs.RemoveUnneededLifeTimeObjs (LifeTimeSteps);
				LifeTimesRecords = srcs.ArrofArrPush (LifeTimesRecords, CleanLifetimePath);
				LifeTimeSteps = null;
				MyStep = StartBlock;
				LifeTimeSteps = srcs.ArrPush (LifeTimeSteps, MyStep);
				IndicCon.ResetFreeCells = true;
			}
			//------------------------------------------------------------------------------- WHEN WE REACH THE FINISH END
			}

		}
	public void ResetAI()
    {
		LifeTimeSteps = null;
		LifeTimesRecords = null;
		RunSimulation = false;
		LifeTimeSteps = null;
		PauseGO = GameObject.Find("PauseImg").GetComponent<Image>();
		PlayGO = GameObject.Find("PlayImg").GetComponent<Image>();
		PauseGO.enabled = true;
		PlayGO.enabled = false;
		Starttime = Time.time;
		sources srcs = gameObject.GetComponent<sources>();//---------- connecting with the sources script
		StartBlock = GameObject.FindGameObjectWithTag("StartUnit").gameObject;
		FinishBlock = GameObject.FindGameObjectWithTag("FinishUnit").gameObject;
		MyStep = StartBlock;
		LifeTimeSteps = srcs.ArrPush(LifeTimeSteps, MyStep);
		AIUnit = transform.gameObject;
		AIUnit.transform.position = StartBlock.transform.position;
		AIUnit.transform.position = new Vector3(AIUnit.transform.position.x, AIUnit.transform.position.y + 1.2f, AIUnit.transform.position.z);
		ArrOfAllPassableSlots = GameObject.FindGameObjectsWithTag("FreeSpace");
		ArrOfAllPassableSlots = srcs.ArrPush(ArrOfAllPassableSlots, FinishBlock);
		ArrOfAllPassableSlots = srcs.ArrPush(ArrOfAllPassableSlots, StartBlock);

	}
}
