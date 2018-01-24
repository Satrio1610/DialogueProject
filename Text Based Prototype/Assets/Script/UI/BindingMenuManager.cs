using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BindingMenuManager : MonoBehaviour {

	public enum KEY_SELECTED {
		NONE,
		QUICKSAVE,
		QUICKLOAD,
		NEXTDIAL
	}

	[SerializeField]
	private Button quickSaveKeyBindingButton; 
	[SerializeField]
	private Button quickLoadKeyBindingButton;
	[SerializeField]
	private Button nextDialogueKeyBindingButton; 
	[SerializeField]
	private HotKeyManager hkm;

	KEY_SELECTED selectedKeyCode;
	Event keyEvent; 
	bool hasKeySelected; 

	void Awake() {
		hasKeySelected = false;
		selectedKeyCode = KEY_SELECTED.NONE;
		keyEvent = Event.current; 

		subscribeToButtonOnClickEvent ();
		setCurrentSelectedKeyText ();
	}

	void Start() {
		
	}

	void setCurrentSelectedKeyText() {
		quickSaveKeyBindingButton.GetComponentInChildren<Text> ().text = hkm.quickSave.ToString();
		quickLoadKeyBindingButton.GetComponentInChildren<Text> ().text = hkm.quickLoad.ToString ();
		nextDialogueKeyBindingButton.GetComponentInChildren<Text> ().text = hkm.skipOrNextDialogue.ToString ();
	}

	void subscribeToButtonOnClickEvent(){
		quickLoadKeyBindingButton.onClick.AddListener (setSelectedKeyCodeToQuickLoad);
		quickSaveKeyBindingButton.onClick.AddListener (setSelectedKeyCodeToQuickSave);
		nextDialogueKeyBindingButton.onClick.AddListener (setSelectedKeyCodeToSetNextDialogue);
	}

	void OnGUI() {
		
		if (hasKeySelected && selectedKeyCode != null) {
			keyEvent = Event.current;


			if(keyEvent != null && keyEvent.isKey ){
				switch(selectedKeyCode){
				case KEY_SELECTED.QUICKLOAD:
					hkm.quickLoad = keyEvent.keyCode;
					quickLoadKeyBindingButton.GetComponentInChildren<Text> ().text = keyEvent.keyCode.ToString ();
					break; 
				case KEY_SELECTED.NEXTDIAL:
					hkm.skipOrNextDialogue = keyEvent.keyCode;
					nextDialogueKeyBindingButton.GetComponentInChildren<Text> ().text = keyEvent.keyCode.ToString ();
					break;
				case KEY_SELECTED.QUICKSAVE:
					hkm.quickSave = keyEvent.keyCode;
					quickSaveKeyBindingButton.GetComponentInChildren<Text> ().text = keyEvent.keyCode.ToString ();
					break;
				}

				hkm.setHotKeyManagerMode (HotKeyManager.MODE.INGAME);
				selectedKeyCode = KEY_SELECTED.NONE;
				setCurrentSelectedKeyText ();
			}

		}
	}

	void Update() {


	}

	void setSelectedKeyCode(KEY_SELECTED newSelectedKey){
		this.selectedKeyCode = newSelectedKey;
		hkm.setHotKeyManagerMode (HotKeyManager.MODE.BINDING_MENU);
		hasKeySelected = true; 
	}

	public void setSelectedKeyCodeToQuickLoad() {
		setSelectedKeyCode (KEY_SELECTED.QUICKLOAD);
	}

	public void setSelectedKeyCodeToQuickSave() {
		setSelectedKeyCode (KEY_SELECTED.QUICKSAVE);
	}

	public void setSelectedKeyCodeToSetNextDialogue(){
		setSelectedKeyCode(KEY_SELECTED.NEXTDIAL);
	}

}
