using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogSelector : MonoBehaviour
{
	
	public UITable tableRoot;
	private string functionName;
	private GameObject _messageTarget;
	private UIPanel _previuosPanel;
	private List<ResourceDialogElement> _elements;
	public UIAtlas iconsAtlas;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void FillTableWithElements (List<ResourceDialogElement> elements, GameObject messageTarget, string functionName)
	{
		RemoveAll ();
		
		_elements = new List<ResourceDialogElement> (elements);
		_messageTarget = messageTarget;
		this.functionName = functionName;
		bool isFirst = true;
		
		foreach (ResourceDialogElement element in _elements) {
			GameObject newItem = Instantiate (EditorMenu.Instance.TableItemPrefab) as GameObject;
			if (isFirst) {
				newItem.GetComponent<UICheckbox> ().isChecked = true;
				isFirst = false;
			}
			newItem.transform.parent = tableRoot.transform;
			newItem.transform.localPosition = Vector3.zero;
			newItem.transform.localScale = Vector3.one;
			newItem.name = element.name;
			newItem.transform.FindChild ("Name").GetComponent<UILabel> ().text = element.name;
			newItem.transform.FindChild ("Description").GetComponent<UILabel> ().text = element.descr;
			newItem.GetComponent<UICheckbox> ().radioButtonRoot = tableRoot.transform;
			
			UISprite elementSprite = newItem.transform.FindChild ("Icon").GetComponent<UISprite> ();
			if (iconsAtlas.GetSprite (element.sprite) == null) {
				elementSprite.spriteName = "none";
			} else {
				elementSprite.spriteName = element.sprite;
			}
		}
		
		tableRoot.repositionNow = true;
	}

	public void RemoveAll ()
	{
		if (_elements != null) {
			_elements.Clear ();
		}
		foreach (Transform item in tableRoot.transform) {
			GameObject.Destroy (item.gameObject);
		}
	}

	void PanelFlipIn (UIPanel previuosPanel)
	{
		_previuosPanel = previuosPanel;
	}
	
	void OnOkayBtnClick ()
	{
		if (_messageTarget != null) {
			string returnValue = "";
			
			// Find checked chekbox and setting appropriate value
			UICheckbox[] cbs = tableRoot.GetComponentsInChildren<UICheckbox> ();
			for (int i = 0, imax = cbs.Length; i < imax; ++i) {
				UICheckbox cb = cbs [i];
				if (cb.isChecked) {
					string cbName = cb.transform.FindChild ("Name").GetComponent<UILabel> ().text;
					
					for (int j = 0, jmax = _elements.Count; j < jmax; j++) {
						if (_elements [j].name == cbName) {
							returnValue = _elements [j].name;
							break;
						}
					}
					break;
				}
			}
			
			// If there is no checked checkbox - return "none"
			if (string.IsNullOrEmpty (returnValue)) {
				returnValue = "none";
			}
			
			// Send Message with result value
			_messageTarget.SendMessage (functionName, returnValue, SendMessageOptions.DontRequireReceiver);
			
			RemoveAll ();
			EditorMenu.FlipPanels (EditorMenu.Instance.area_dialogSelectorPanel, _previuosPanel);
		}
	}
	
	void OnCancelBtnClick ()
	{
		EditorMenu.FlipPanels (EditorMenu.Instance.area_dialogSelectorPanel, _previuosPanel);
	}
}

public class ResourceDialogElement
{
	public string name;
	public string sprite;
	public string descr;
	
	public ResourceDialogElement (string _sprite, string _name, string _descr)
	{	
		name = _name;
		sprite = _sprite;
		descr = _descr;
	}
}