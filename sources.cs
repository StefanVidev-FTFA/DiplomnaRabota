using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sources : MonoBehaviour {
	public float Distance2(GameObject obj1, GameObject obj2){// only by x and z
		float x1 = 0.0f;
		//float y1 = 0.0f;
		float z1 = 0.0f;
		if (obj1.transform.position.x > obj2.transform.position.x) {
			x1 = obj1.transform.position.x - obj2.transform.position.x;
		} else
			x1 = obj2.transform.position.x - obj1.transform.position.x;
		//-----------------------------------------------------------
//		if (obj1.transform.position.y > obj2.transform.position.y) {
//			y1 = obj1.transform.position.y - obj2.transform.position.y;
//		} else
//			y1 = obj2.transform.position.y - obj1.transform.position.y;
		//-----------------------------------------------------------
		if (obj1.transform.position.z > obj2.transform.position.z) {
			z1 = obj1.transform.position.z - obj2.transform.position.z;
		} else
			z1 = obj2.transform.position.z - obj1.transform.position.z;
		//-----------------------------------------------------------
		float firstTri = Mathf.Sqrt(x1*x1+z1*z1);
		//firstTri = Mathf.Sqrt (firstTri * firstTri + y1 * y1);
		return firstTri;
	}
	public GameObject[] ArrPush(GameObject[] arr, GameObject Element){// this function only puts in new members
		GameObject[] NewArr;
		if (arr == null) {
			NewArr = new GameObject[1];
			NewArr [0] = Element;
		} else {
			int flag = 0;
			NewArr = new GameObject[arr.Length + 1];
			for (int i = 0; i < arr.Length; i++) {
				if (Element == arr [i]) {
					flag = 1;
				}
			}
			if (flag == 0) {
				NewArr = new GameObject[arr.Length + 1];
				for (int i = 0; i < arr.Length; i++) {
					NewArr [i] = arr [i];
				}
				NewArr [arr.Length] = Element;
			} else
				NewArr = arr;
		}
		return NewArr;
	}
	public GameObject[] ArrPushV2(GameObject[] arr, GameObject Element){
		GameObject[] NewArr;
		if (arr == null) {
			NewArr = new GameObject[1];
			NewArr [0] = Element;
		} else {
			NewArr = new GameObject[arr.Length + 1];
			for (int i = 0; i < arr.Length; i++) {
				NewArr [i] = arr [i];
			}
			NewArr [arr.Length] = Element;
		}
		return NewArr;
	}
	public object[] ArrPush_object(object[] arr, object Element){//// this function only puts in new members
		object[] NewArr;
		if (arr == null) {
			NewArr = new object[1];
			NewArr [0] = Element;
		} else {
			int flag = 0;
			NewArr = new object[arr.Length + 1];
			for (int i = 0; i < arr.Length; i++) {
				if (Element == arr [i]) {
					flag = 1;
				}
			}
			if (flag == 0) {
				NewArr = new object[arr.Length + 1];
				for (int i = 0; i < arr.Length; i++) {
					NewArr [i] = arr [i];
				}
				NewArr [arr.Length] = Element;
			} else
				NewArr = arr;
		}
		return NewArr;
	}
	public GameObject[][] ArrofArrPush(GameObject[][] ArrofArrs,GameObject[] arr){
		GameObject[][] NewArrofArrs;
		if (ArrofArrs == null) {
			ArrofArrs = new GameObject[1][];
			ArrofArrs [0] = arr;
			NewArrofArrs = ArrofArrs;
		} else {
			NewArrofArrs = new GameObject[ArrofArrs.Length + 1][];
			for (int i = 0; i < ArrofArrs.Length; i++) {
				NewArrofArrs[i]=ArrofArrs[i];
			}
			NewArrofArrs [ArrofArrs.Length] = arr;
		}
		return NewArrofArrs;
	}
	public object[][] ArrofArrPush_object(object[][] ArrofArrs,object[] arr){
		object[][] NewArrofArrs;
		if (ArrofArrs == null) {
			ArrofArrs = new object[1][];
			ArrofArrs [0] = arr;
			NewArrofArrs = ArrofArrs;
		} else {
			NewArrofArrs = new object[ArrofArrs.Length + 1][];
			for (int i = 0; i < ArrofArrs.Length; i++) {
				NewArrofArrs[i]=ArrofArrs[i];
			}
			NewArrofArrs [ArrofArrs.Length] = arr;
		}
		return NewArrofArrs;
	}
	public object[][][] Arr3DimPush_object(object[][][] ArrofArrs,object[][] arr){
		object[][][] NewArrofArrs;
		if (ArrofArrs == null) {
			ArrofArrs = new object[1][][];
			ArrofArrs [0] = arr;
			NewArrofArrs = ArrofArrs;
		} else {
			NewArrofArrs = new object[ArrofArrs.Length + 1][][];
			for (int i = 0; i < ArrofArrs.Length; i++) {
				NewArrofArrs[i]=ArrofArrs[i];
			}
			NewArrofArrs [ArrofArrs.Length] = arr;
		}
		return NewArrofArrs;
	}
	public GameObject[] MergeGamObjArrs(GameObject[] arr1,GameObject[] arr2){
		GameObject[] NewArr = new GameObject[arr1.Length + arr2.Length];
		for (int i = 0; i < NewArr.Length; i++) {
			if (i < arr1.Length)
				NewArr [i] = arr1 [i];
			else
				NewArr [i] = arr2 [i - arr1.Length];
		}
		return NewArr;
	}
	public GameObject[] ArrPopSpecific(GameObject[] Myarr,GameObject element){
		bool check = false;
		for (int i = 0; i < Myarr.Length; i++) {
			if (Myarr [i].gameObject.name == element.gameObject.name) {
				check = true;
			}
		}
		if (check == true) {
			GameObject[] NewArr = new GameObject[Myarr.Length - 1];
			int br = 0;
			for (int i = 0; i < Myarr.Length; i++) {
				if (Myarr [i].gameObject.name != element.gameObject.name) {
					NewArr [br] = Myarr [i];
					br++;
				}
			}
			return NewArr;
		} else {
			return Myarr;
		}
	}
	public GameObject[] GetSasedi(GameObject BlockImStepingin,GameObject[] ArrofOtherBlocks){
		GameObject[] SasedniKletki=null;
		int ArrSize = 0;
		int br = 0;
		for (int i = 0; i < ArrofOtherBlocks.Length; i++) {
			if (Distance2 (BlockImStepingin, ArrofOtherBlocks [i])<2.1 && ArrofOtherBlocks[i]!=BlockImStepingin)
				ArrSize = ArrSize + 1;
		}
		SasedniKletki = new GameObject[ArrSize];
		for (int i = 0; i < ArrofOtherBlocks.Length; i++) {
			if (Distance2 (BlockImStepingin, ArrofOtherBlocks [i])<2.1 && ArrofOtherBlocks[i]!=BlockImStepingin) {
				SasedniKletki [br] = ArrofOtherBlocks [i];
				br = br + 1;
			}
		}
		return SasedniKletki;
	}
	public GameObject RandomNextStep(GameObject[] ArrOfCandidates){
			int chosen = Random.Range (0, ArrOfCandidates.Length);
			return ArrOfCandidates [chosen];
	}
	public GameObject[] ArrPopFromArr(GameObject[] array,GameObject[] ArrElToRemove){// removes all the object from arr2 inside arr1
		int br = 0;
		for (int i = 0; i < array.Length; i++) {
			bool check = false;
			for (int j = 0; j < ArrElToRemove.Length; j++) {
				if (array [i] == ArrElToRemove[j]) {
					check = true;
				}
			}
			if (check == false)
				br = br + 1;
		}
		int br2 = 0;
		GameObject[] NewArr = new GameObject[br];
		for (int i = 0; i < array.Length; i++) {
			bool check = false;
			for (int j = 0; j < ArrElToRemove.Length; j++) {
				if (array [i] == ArrElToRemove[j]) {
					check = true;
				}
			}
			if (check == false) {
				NewArr [br2] = array [i];
				br2++;
			}
				
		}
		return NewArr;
	}
	public GameObject[] PopArrFromArr(GameObject[] MAinArr,GameObject[] ArrOfObjsToRemove){
		int br = 0;
		for (int i = 0; i < MAinArr.Length; i++) {
			bool check = false;
			for (int j = 0; j < ArrOfObjsToRemove.Length; j++) {
				if (MAinArr [i] == ArrOfObjsToRemove [j])
					check = true;
			}
			if (check == false)
				br = br + 1;
		}
		GameObject[] NewArr = new GameObject[br];
		br = 0;
		for (int i = 0; i < MAinArr.Length; i++) {
			bool check = false;
			for (int j = 0; j < ArrOfObjsToRemove.Length; j++) {
				if (MAinArr [i] == ArrOfObjsToRemove [j])
					check = true;
			}
			if (check == false) {
				NewArr [br] = MAinArr [i];
				br = br + 1;
			}
		}
		return NewArr;
	}
	public object[][] SasediCrossOrdered(GameObject Mystep,GameObject[] Sasedi){// the sasedi arr should be always 4 objects
		// the first element is LEft, the scond is Right, third is Bottom, and fourth is the top;
		GameObject[] SortedArr = new GameObject[4];
		SortedArr [0] = null;SortedArr [1] = null;SortedArr [2] = null;SortedArr [3] = null;
		for (int i = 0; i < Sasedi.Length; i++) {
			float myX = Mystep.transform.position.x;
			float myZ = Mystep.transform.position.z;
			float hisX = Sasedi [i].transform.position.x;
			float hisZ = Sasedi [i].transform.position.z;
			if (hisZ < (myZ - 1)) {
				SortedArr [0] = Sasedi [i];
			}
			if (hisZ > (myZ + 1)) {
				SortedArr [1] = Sasedi [i];
			}
			if (hisX > (myX + 1)) {
				SortedArr [2] = Sasedi [i];
			}
			if (hisX < (myX - 1)) {
				SortedArr [3] = Sasedi [i];
			}
		}
		object[][] array = null;
		for (int i = 0; i < SortedArr.Length; i++) {
			if (SortedArr [i] != null) {
				object[] values = new object[3];
				values [0] = SortedArr [i];values [1] = i + 1;values [2] = true;
				array = ArrofArrPush_object (array, values);
			}
		}
		//print ("here" + array.Length);
		return array;
	}// this function wil return array of arrays each with has num1-Gameobject, num2-His rank(1-4), and a bool if he is to be branched or not;
	public bool IsObjInArr(GameObject Obj,GameObject[] Arr){
		bool MyFlag = false;
		if(Obj!=null && Arr!=null){
		for (int i = 0; i < Arr.Length; i++) {
			if (Obj == Arr [i])
				MyFlag = true;
		}
		}
		return MyFlag;
	}
	public GameObject GetThatGensNextStep(GameObject TheCurrStep,GameObject[] TheFitGen){
		GameObject FitStep = null;
		for (int i = 0; i < TheFitGen.Length; i++) {
			if (TheCurrStep == TheFitGen [i]) {
				FitStep = TheFitGen [i + 1];
			}
		}
		return FitStep;
	}
	public GameObject CalcIntelNextStep(GameObject CurrStep,GameObject[] ArrofAllSlots,GameObject[] ArrOfStepsThisLifeTime,GameObject[][] TotalGenInprint,int BestPathLength){
		GameObject RandomCleverStep = null;
		GameObject FitnessStep = null;
		GameObject[] NonPassedSasedi = null;
		GameObject TheIntelStep = null;
		//-------------------------------------------------- random clever step is done START ------------------
		GameObject[] Sasedi = GetSasedi (CurrStep, ArrofAllSlots);
		for (int i = 0; i < Sasedi.Length; i++) {
			if (!IsObjInArr (Sasedi [i], ArrOfStepsThisLifeTime)) {
				NonPassedSasedi = ArrPush (NonPassedSasedi, Sasedi [i]);
			}
		}
		if (NonPassedSasedi != null) {
			RandomCleverStep = RandomNextStep (NonPassedSasedi);
		} else
			RandomCleverStep = RandomNextStep (Sasedi);
		//-------------------------------------------------- random clever step is done END ------------------
		TheIntelStep = RandomCleverStep;
		if (TotalGenInprint == null)
			TheIntelStep = RandomCleverStep;
		else {//----------------------------------- IS THERE IS A GEN INPRINT START -----------------
			TheIntelStep = RandomCleverStep;// in the case this step was not stepped on in previous gen we go woth randomIntelstep
			//------------- Below i take the fittest Gen -----------------
			GameObject[] FittestGen = TotalGenInprint [0];
			int min = TotalGenInprint [0].Length;
			for (int i = 0; i < TotalGenInprint.Length; i++) {
				if (TotalGenInprint [i].Length < min) {
					FittestGen = TotalGenInprint [i];
					min = TotalGenInprint [i].Length;
				}
				}
			GameObject.Find("CBG").GetComponent<Text>().text = FittestGen.Length.ToString();
			if (IsObjInArr (CurrStep, FittestGen)) {
				FitnessStep = GetThatGensNextStep (CurrStep, FittestGen);
			}
			//------------- above i take the fittest Gen -----------------
			float Perc  = (float)BestPathLength/FittestGen.Length;
			float RanPerc = 1-Perc;RanPerc=RanPerc*100;
			float FitPerc = Perc;FitPerc = FitPerc * 100;
			float Sum = RanPerc + FitPerc;float DecideRan = Random.Range (0, Sum);
			if (DecideRan <= FitPerc && FitnessStep!=null) {
				TheIntelStep = FitnessStep;
			}
			else
				TheIntelStep = RandomCleverStep;
		}//----------------------------------------- IS THERE IS A GEN INPRINT END -----------------
		//if (TheIntelStep == null)
			//print ("HERE IT IS MY GUY");
		return TheIntelStep;
	}
	public GameObject[] RemoveUnneededLifeTimeObjs(GameObject[] LifeTimeSteps){
//		Afer the AIScrpt Has made its ArrOfLifeTimeSteps, this function will go int And 
//		Remove the ArrOfLifeTimeSteps which the AIScrpt walked in but then He
//		returned to a previous aolready existing int this Arr position
		for (int i = 0; i < LifeTimeSteps.Length; i++) {
			if (LifeTimeSteps [i] != null) {
				for (int j = 0; j < LifeTimeSteps.Length; j++) {
					if (j != i && LifeTimeSteps[i]==LifeTimeSteps[j]) {
						for (int d = i+1; d < j+1; d++) {
							LifeTimeSteps [d] = null;
						}
					}
				}
			}
		}
		GameObject[] NewArr = null;
		for (int i = 0; i < LifeTimeSteps.Length; i++) {
			if (LifeTimeSteps [i] != null) {
				NewArr = ArrPushV2 (NewArr, LifeTimeSteps [i]);
			}
		}
		return NewArr;
	}
}
