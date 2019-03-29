
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFaderScript : MonoBehaviour {

	public Animator animator;

	private int levelToLoad;
	// Update is called once per frame
	 void Update () {
		 if(Input.GetMouseButtonDown(0))
	 		FadeToLevel();
	 }

	public void FadeToLevel ()
	{
		levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
		animator.SetTrigger("FadeOut");
	}

	public void OnFadeComplete ()
	{
		SceneManager.LoadScene(levelToLoad);
	}
}
