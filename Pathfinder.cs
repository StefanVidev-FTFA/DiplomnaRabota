using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFinder : MonoBehaviour {
	GameObject[] ArrFreeSpacs;
	GameObject[] Sasedi;
	GameObject obj;
	GameObject StartObj;
	GameObject FinishObj;
	//--------------- BELOW ARE VARIABLES FOR THE PATHFINDING ONLY!!!!!!!!!!!!! ---------------------
	object[][][] LayerMatrix = null;
	GameObject[] CurrentArrOfPAth;
	GameObject[][] AllPathCombinations=null;
	public static GameObject[] BestPath= null;
	private int bestNumRN;
	int count = 0;
	//--------------- ABOVE ARE VARIABLES FOR THE PATHFINDING ONLY!!!!!!!!!!!!! ---------------------
	void Awake () {
		bestNumRN = 0;
		sources srcs = gameObject.GetComponent<sources>();
		CurrentArrOfPAth = null;AllPathCombinations = null;
		StartObj = GameObject.FindGameObjectWithTag ("StartUnit").gameObject;
		FinishObj =  GameObject.FindGameObjectWithTag ("FinishUnit").gameObject;
		ArrFreeSpacs = GameObject.FindGameObjectsWithTag ("FreeSpace");
		ArrFreeSpacs = srcs.ArrPush (ArrFreeSpacs, FinishObj);
		GetCombinations (ArrFreeSpacs, StartObj, FinishObj);
		BestPath = AllPathCombinations [0];
		int min = AllPathCombinations [0].Length;
		for (int i = 0; i < AllPathCombinations.Length; i++) {
			if (AllPathCombinations [i].Length < min) {
				BestPath = AllPathCombinations [i];
				min = AllPathCombinations [i].Length;
			}
		}
		GameObject.Find("BPG").GetComponent<Text>().text = BestPath.Length.ToString();
	}
	void Update () {
	}
	public GameObject MyRecursion(GameObject CurrStepRec){
		count += 1;
		sources srcs = gameObject.GetComponent<sources>();
		GameObject NewStep = null;
		NewStep = GNNBC (CurrStepRec, ArrFreeSpacs);
		if (NewStep != null) {
			CurrentArrOfPAth = srcs.ArrPush(CurrentArrOfPAth, NewStep);
			if (NewStep != FinishObj) {
				object[][] emtyArr = null;
				if (LayerMatrix.Length <= CurrentArrOfPAth.Length) {
					LayerMatrix = srcs.Arr3DimPush_object (LayerMatrix, emtyArr);
				}
				return MyRecursion (NewStep);
			} else {/////// in here when he has reached the Finish obj START
//				print("i reached finish");
//				print ("---------------------------------------------------");
//				for (int i = 0; i < CurrentArrOfPAth.Length; i++) {
//					print ("num:"+(i+1)+"]----->"+CurrentArrOfPAth[i].name);
//				}
//				print ("---------------------------------------------------");
				AllPathCombinations=srcs.ArrofArrPush(AllPathCombinations,CurrentArrOfPAth);
				GameObject NewCurrStep = GetNewCrop ();
				if (NewCurrStep != null) {
					//print (NewCurrStep.name);
					//return null;
					return MyRecursion (NewCurrStep);
				} else {
					//CurrentArrOfPAth = null;
					//CurrentArrOfPAth = srcs.ArrPush(CurrentArrOfPAth, StartObj);
					return null;
				}
			}//////////////// in here when he has reached the Finish obj END
		} else {// in here when he reaches a dead end START
			//print("i reached a deadend");
//			print ("---------------------------------------------------");
//			for (int i = 0; i < CurrentArrOfPAth.Length; i++) {
//				print ("num:"+(i+1)+"]----->"+CurrentArrOfPAth[i].name);
//			}
//			print ("---------------------------------------------------");
			GameObject NewCurrStep = GetNewCrop ();
			if (NewCurrStep != null) {
				//return null;
				return MyRecursion (NewCurrStep);
			} else {	
				return null;
			}
		}/////////// in here when he reaches a dead end END
	}
	public void GetCombinations(GameObject[] FreeSpaces,GameObject Start,GameObject Finish){
		sources srcs = gameObject.GetComponent<sources>();
		CurrentArrOfPAth = srcs.ArrPush (CurrentArrOfPAth, Start);
		object[][] b0ss = null;
		LayerMatrix = srcs.Arr3DimPush_object (LayerMatrix, b0ss);
		count = 0;
		MyRecursion (Start);
	}
	public GameObject GNNBC(GameObject CurrStep,GameObject[] ArrOfDaFreSpacs){//---- GET THE NEXT NON BRANCHED CHILD-----
		sources srcs = gameObject.GetComponent<sources>();
		GameObject[] HoldArr0 = srcs.GetSasedi (CurrStep,ArrOfDaFreSpacs);// this guy has been checked dw
		object[][] PotentialNextSteps = srcs.SasediCrossOrdered (CurrStep, HoldArr0);// this guy has been checked dw
		GameObject TheChosenOne = null;
		object[] TheChosenOneArr = null;
		int BigPrior = 5;
		for (int i = 0; i < PotentialNextSteps.Length; i++) {
			if (srcs.IsObjInArr((GameObject)PotentialNextSteps[i][0],CurrentArrOfPAth)==false){
				object[] zapis = null;zapis=srcs.ArrPush_object(zapis,(GameObject)PotentialNextSteps[i][0]);
				zapis=srcs.ArrPush_object(zapis,(int)PotentialNextSteps[i][1]);zapis=srcs.ArrPush_object(zapis,(bool)PotentialNextSteps[i][2]);
				zapis=srcs.ArrPush_object(zapis,CurrentArrOfPAth);
				if(CheckIfZapisExists(zapis)==false){
				LayerMatrix [CurrentArrOfPAth.Length-1] = srcs.ArrofArrPush_object (LayerMatrix [CurrentArrOfPAth.Length-1], zapis);
				}
				}
			if ((int)PotentialNextSteps [i] [1] < BigPrior && srcs.IsObjInArr((GameObject)PotentialNextSteps[i][0],CurrentArrOfPAth)==false) {
				if(CanIUnbranchThisGuy(PotentialNextSteps [i])){
				TheChosenOne = (GameObject)PotentialNextSteps [i] [0];
				BigPrior = (int)PotentialNextSteps [i] [1];
				TheChosenOneArr = new object[4];
				TheChosenOneArr [0] = TheChosenOne;TheChosenOneArr [1] = PotentialNextSteps [i] [1];
				TheChosenOneArr [2] = PotentialNextSteps [i] [2];TheChosenOneArr [3] = CurrentArrOfPAth;
					GameObject[] GamArr = TheChosenOneArr [3] as GameObject[];
				}
			}
		}
		if (TheChosenOne != null) {
			MarkAsBranched (TheChosenOneArr);
		}
		return TheChosenOne;
	}
	public void MarkAsBranched(object[] TheChosenSArr){// in here we check off as false branches to mark them as branched based on priority
		int LayerIndex = CurrentArrOfPAth.Length - 1;
		for (int i = 0; i < LayerMatrix[LayerIndex].Length; i++) {
			if (LayerMatrix [LayerIndex] [i] [0] == TheChosenSArr [0] && (int)LayerMatrix [LayerIndex] [i] [1] == (int)TheChosenSArr [1]) {
				LayerMatrix [LayerIndex][i] [2] = false;
				//print("i was here");
			}
		}
	}
	public bool CanIUnbranchThisGuy(object[] ChosenArrBoi){
		bool check = true;
		int LayerIndex = CurrentArrOfPAth.Length - 1;
		for (int i = 0; i < LayerMatrix [LayerIndex].Length; i++) {
			if (LayerMatrix [LayerIndex] [i] [0] == ChosenArrBoi [0] && (int)LayerMatrix [LayerIndex] [i] [1] == (int)ChosenArrBoi [1]) {
				if ((bool)LayerMatrix [LayerIndex] [i] [2] == false) {
					check = false;
				}
			}
		}
		return check;
	}
	public GameObject GetNewCrop(){//----------------- in here we also update the CurrArrOfPath -----------
		GameObject TheNewCropStep = null;
		int BigPrior = 5;
		for (int i = 0; i < LayerMatrix.Length; i++) {
			if (LayerMatrix[i]!=null){
				for (int j = 0; j < LayerMatrix [i].Length; j++) {
					if ((bool)LayerMatrix [i] [j] [2] == true) {
						GameObject[] ItsPathInprint = LayerMatrix [i] [j] [3] as GameObject[];
							TheNewCropStep = ItsPathInprint [ItsPathInprint.Length - 1];
						CurrentArrOfPAth = ItsPathInprint;
						break;
					}
				}
		}
			if(TheNewCropStep!=null){
				break;
			}
			TheNewCropStep = null;
		}
		//print (TheNewCropStep.name);
		return TheNewCropStep;
	}
	public bool CheckIfZapisExists(object[] TheZapis){//////// returns true if zapis is already signed in
		bool check = false;
		int LayerIndex = CurrentArrOfPAth.Length - 1;
		if (LayerMatrix [LayerIndex] != null){
			for (int i = 0; i < LayerMatrix [LayerIndex].Length; i++) {
				//print (TheZapis [1]+"<------one");
				//print (LayerMatrix [LayerIndex] [i] [1]+"<------two");
				if (TheZapis [0] == LayerMatrix [LayerIndex] [i] [0] && (int)TheZapis [1] == (int)LayerMatrix [LayerIndex] [i] [1]) {
					check = true;
				}
			}
	}
		return check;
	}
	public void ReCalculateBestPath()
    {
		BestPath = null;
		LayerMatrix = null;
		CurrentArrOfPAth = null; AllPathCombinations = null;
		sources srcs = gameObject.GetComponent<sources>();
		StartObj = GameObject.FindGameObjectWithTag("StartUnit").gameObject;
		FinishObj = GameObject.FindGameObjectWithTag("FinishUnit").gameObject;
		ArrFreeSpacs = GameObject.FindGameObjectsWithTag("FreeSpace");
		ArrFreeSpacs = srcs.ArrPush(ArrFreeSpacs, FinishObj);
		GetCombinations(ArrFreeSpacs, StartObj, FinishObj);
		BestPath = AllPathCombinations[0];
		int min = AllPathCombinations[0].Length;
		for (int i = 0; i < AllPathCombinations.Length; i++)
		{
			if (AllPathCombinations[i].Length < min)
			{
				BestPath = AllPathCombinations[i];
				min = AllPathCombinations[i].Length;
			}
		}
		GameObject.Find("BPG").GetComponent<Text>().text = BestPath.Length.ToString();
	}
}
