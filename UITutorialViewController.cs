using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialViewController : MonoBehaviour {
	public Dictionary<int,UITutorialView> tutorialViews = new Dictionary<int,UITutorialView>();
	private int currentIndex;
	private UITutorialView currentView;
	private UITutorialView nextView;
    public UITabViewController tabController;
	public string Name;

	public Button firstNextButton;

	public int lastIndex = 3;

	public bool showTutorial;

	private void Awake(){
		foreach(UITutorialView tutorial in FindObjectsOfTypeAll(typeof(UITutorialView))){
			tutorial.gameObject.SetActive(true);
			tutorial.Init();
			tutorialViews.Add(tutorial.index,tutorial);
			tutorial.Hide();
			if(tutorial.index == 0){
				currentView = tutorial;
				currentView.Show();	
			}

			tutorial.onNextTutorial += nextTutorial;
		}
	}

	public void startTutorial(){
		nextTutorial(0);
		firstNextButton.gameObject.SetActive(true);
	}

	public void startWithoutTutorial(){
		Debug.LogError("Start without tutorial");
		if(showTutorial == false){
			firstNextButton.gameObject.SetActive(false);
			nextTutorial(0);
			tutorialViews[1].showTutorial = false;
			StartCoroutine(hideTutorial(2f));
		}
	}

	private void nextTutorial(int index){
		if(index == lastIndex){
			StartCoroutine(hideTutorial(0));
			return;
		}

		currentView.Hide();
		currentView = tutorialViews[index+1];
		currentView.Show();
	}

	private IEnumerator hideTutorial(float wait){
		yield return new WaitForSeconds(wait);
		tabController.switchToHomeScreen();
		gameObject.SetActive(false);
	}
}
