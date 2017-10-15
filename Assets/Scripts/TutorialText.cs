using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {
    Text helpText;

	// Use this for initialization
	void Start () {
        helpText = gameObject.GetComponent<Text>();
        StartCoroutine("DisplayHelpText");
	}

    IEnumerator DisplayHelpText() {
        yield return new WaitForSeconds(2);

        helpText.text = "Your goal is to get to the door.";
        yield return new WaitForSeconds(5);

        helpText.text = "But, watch out for enemies!";
        yield return new WaitForSeconds(5);

        helpText.text = "Paint trails can help you target them.";
        yield return new WaitForSeconds(5);

        helpText.text = "You can jump on enemies once you shoot them.";
        yield return new WaitForSeconds(5);

        helpText.text = "";
        yield return new WaitForSeconds(2);

        helpText.text = "Shadows can help you locate platforms.";
        yield return new WaitForSeconds(5);

        helpText.text = "You can wall jump too!";
        yield return new WaitForSeconds(5);
        helpText.text = "";
    }
}
