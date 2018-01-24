using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class RegexTest : MonoBehaviour {
	private string regex = @"^\[(.*)\]\s?(.*)?";
	private string sampleString = "[ clicking sound ] ...";

	public IntDictionary nd;
	// Use this for initialization
	void Start () {
		Regex thisRegex = new Regex (regex);

		Match a = thisRegex.Match (sampleString);

		Debug.Log (a.Groups.Count);
		Debug.Log (a.Groups[0].ToString());
		Debug.Log (a.Groups[1]);
		Debug.Log (a.Groups[2]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
