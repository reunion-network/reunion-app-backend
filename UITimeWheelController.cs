using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UITimeWheelController : MonoBehaviour {

	public TimeWheel_UITimeWheel daysTimeWheel,hourTimeWheel,minutesTimeWheel;

	public void Init(){
		daysTimeWheel.Init();
		hourTimeWheel.Init();
		minutesTimeWheel.Init();
	}

	public double currentTime(){
		double time = 0;
		int m = minutesTimeWheel.currentTime();
		int h = hourTimeWheel.currentTime();
		int d = daysTimeWheel.currentTime();

		h = h*60;
		d = d*24*60;
		time = m+h+d;

		return time;
	}

	public void ClearTime(){
		minutesTimeWheel.ClearTime();
		hourTimeWheel.ClearTime();
		daysTimeWheel.ClearTime();
	}

	public void SetTime(double duration){
		int m = 0;
		int h = 0;
		int d = 0;

		d = (int)duration/24/60;

		h = (int)(duration/60)%24;

		m = (int)duration%60;

		Debug.LogWarning("TIMMMMME: "+ d + " : "+ h+ " : "+ m);

		minutesTimeWheel.SetTime(m.ToString());
		hourTimeWheel.SetTime(h.ToString());
		daysTimeWheel.SetTime(d.ToString());
		// h = duration/
	}

	public void setReadonly(){
		switchReadonly(true);
	}

	public void setEditable(){
		switchReadonly(false);
	}

	public void switchReadonly(bool isReadonly){
		minutesTimeWheel.display.readOnly = isReadonly;
        minutesTimeWheel.minusBtn.interactable = !isReadonly;
        minutesTimeWheel.plusBtn.interactable = !isReadonly;

		hourTimeWheel.display.readOnly = isReadonly;
        hourTimeWheel.minusBtn.interactable = !isReadonly;
        hourTimeWheel.plusBtn.interactable = !isReadonly;

		daysTimeWheel.display.readOnly = isReadonly;
        daysTimeWheel.minusBtn.interactable = !isReadonly;
        daysTimeWheel.plusBtn.interactable = !isReadonly;
	}
}
