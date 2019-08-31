using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UITutorialView : MonoBehaviour {
	public Button nextButton;
	public bool isFilled;
	private Canvas canvas;
	public string id;
	public int index;

	public UnityEvent onBecomesVisible = new UnityEvent();
	public UnityEvent onBecomesInvisible = new UnityEvent();

	public bool showTutorial = false;

	public delegate void NextTutorial(int index);
	public event NextTutorial onNextTutorial;

	public virtual void Init(){
		nextButton = GetComponentInChildren<Button>();
		nextButton.onClick.AddListener(next);
	}
	public void Hide(){
		if (canvas == null)
			canvas = GetComponent<Canvas>();
		canvas.sortingOrder = -10000;
		
		onBecomesInvisible.Invoke();
	}

	public void Show(){
		if (canvas == null)
            canvas = GetComponent<Canvas>();
        canvas.sortingOrder = CONSTANTS_UI.POPUP_SORTING_ORDER;

		onBecomesVisible.Invoke();
	}

	private void next(){
		onNextTutorial(index);
	}
}
