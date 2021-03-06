﻿//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Example script that can be used to show tooltips.
/// </summary>

[AddComponentMenu("NGUI/Examples/Tooltip")]
public class UITooltip : MonoBehaviour
{
	static UITooltip mInstance;

	public Camera uiCamera;
	public UILabel text;
	public UISlicedSprite background;
	public float appearSpeed = 10f;
	public bool scalingTransitions = true;

	Transform mTrans;
	float mTarget = 0f;
	float mCurrent = 0f;
	Vector3 mPos;
	Vector3 mSize;

	UIWidget[] mWidgets;

	void Awake () { mInstance = this; }
	void OnDestroy () { mInstance = null; }

	/// <summary>
	/// Get a list of widgets underneath the tooltip.
	/// </summary>

	void Start ()
	{
		mTrans = transform;
		mWidgets = GetComponentsInChildren<UIWidget>();
		mPos = mTrans.localPosition;
		mSize = mTrans.localScale;
		if (uiCamera == null) uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
		SetAlpha(0f);
	}

	/// <summary>
	/// Update the tooltip's alpha based on the target value.
	/// </summary>

	void Update ()
	{
		if (mCurrent != mTarget)
		{
			mCurrent = Mathf.Lerp(mCurrent, mTarget, Time.deltaTime * appearSpeed);
			if (Mathf.Abs(mCurrent - mTarget) < 0.001f) mCurrent = mTarget;
			SetAlpha(mCurrent * mCurrent);

			if (scalingTransitions)
			{
				Vector3 offset = mSize * 0.25f;
				offset.y = -offset.y;

				Vector3 size = Vector3.one * (1.5f - mCurrent * 0.5f);
				Vector3 pos = Vector3.Lerp(mPos - offset, mPos, mCurrent);
				pos = NGUIMath.ApplyHalfPixelOffset(pos);

				mTrans.localPosition = pos;
				mTrans.localScale = size;
			}
		}
	}

	/// <summary>
	/// Set the alpha of all widgets.
	/// </summary>

	void SetAlpha (float val)
	{
		for (int i = 0, imax = mWidgets.Length; i < imax; ++i)
		{
			UIWidget w = mWidgets[i];
			Color c = w.color;
			c.a = val;
			w.color = c;
		}
	}

	/// <summary>
	/// Set the tooltip's text to the specified string.
	/// </summary>

	void SetText (string tooltipText)
	{
		if (!string.IsNullOrEmpty(tooltipText))
		{
			mTarget = 1f;
			
			if (text != null) text.text = tooltipText;

			// Orthographic camera positioning is trivial
			mPos = Input.mousePosition;

			if (background != null)
			{
				Transform backgroundTrans = background.transform;

				if (text != null && text.font != null)
				{
					Transform textTrans = text.transform;
					Vector3 offset = textTrans.localPosition;
					Vector3 textScale = textTrans.localScale;

					// Calculate the dimensions of the printed text
					mSize = text.font.CalculatePrintedSize(tooltipText, true);

					// Scale by the transform and adjust by the padding offset
					mSize.x *= textScale.x;
					mSize.y *= textScale.y;
					mSize.x += offset.x * 2f;
					mSize.y -= offset.y * 2f;
					mSize.z = 1f;

					backgroundTrans.localScale = mSize;
				}
			}

			if (uiCamera != null)
			{
				// Since the screen can be of different than expected size, we want to convert
				// mouse coordinates to view space, then convert that to world position.
				mPos.x = Mathf.Clamp01(mPos.x / Screen.width);
				mPos.y = Mathf.Clamp01(mPos.y / Screen.height);

				// Calculate the ratio of the camera's target orthographic size to current screen size
				float activeSize = uiCamera.orthographicSize / mTrans.parent.lossyScale.y;
				float ratio = (Screen.height * 0.5f) / activeSize;

				// Calculate the maximum on-screen size of the tooltip window
				Vector2 max = new Vector2(ratio * mSize.x / Screen.width, ratio * mSize.y / Screen.height);

				// Limit the tooltip to always be visible
				mPos.x = Mathf.Min(mPos.x, 1f - max.x);
				mPos.y = Mathf.Max(mPos.y, max.y);

				// Update the absolute position and save the local one
				mTrans.position = uiCamera.ViewportToWorldPoint(mPos);
				mPos = mTrans.localPosition;
			}
			else
			{
				// Don't let the tooltip leave the screen area
				if (mPos.x + mSize.x > Screen.width) mPos.x = Screen.width - mSize.x;
				if (mPos.y - mSize.y < 0f) mPos.y = mSize.y;

				// Simple calculation that assumes that the camera is of fixed size
				mPos.x -= Screen.width * 0.5f;
				mPos.y -= Screen.height * 0.5f;
			}

			// Set the final position
			mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(mPos);
		}
		else mTarget = 0f;
	}

	/// <summary>
	/// Show a tooltip with the specified text.
	/// </summary>

	static public void ShowText (string tooltipText)
	{
		if (mInstance != null)
		{
			mInstance.SetText(tooltipText);
		}
	}

	/// <summary>
	/// Show a tooltip with the tooltip text for the specified item.
	/// </summary>

	static public void ShowItem (InvGameItem item)
	{
		if (item != null)
		{
			InvBaseItem bi = item.baseItem;

			if (bi != null)
			{
				string t = "[" + NGUITools.EncodeColor(item.color) + "]" + item.name + "[-]\n";
				
				t += "[AFAFAF]Level " + item.itemLevel + " " + bi.slot;
				
				List<InvStat> stats = item.CalculateStats();

				for (int i = 0, imax = stats.Count; i < imax; ++i)
				{
					InvStat stat = stats[i];
					if (stat.amount == 0) continue;

					if (stat.amount < 0)
					{
						t += "\n[FF0000]" + stat.amount;
					}
					else
					{
						t += "\n[00FF00]+" + stat.amount;
					}

					if (stat.modifier == InvStat.Modifier.Percent) t += "%";
					t += " " + stat.id;
					t += "[-]";
				}
				
				if (!string.IsNullOrEmpty(bi.description)) t += "\n[FF9900]" + bi.description;
				ShowText(t);
				return;
			}
		}
		if (mInstance != null) mInstance.mTarget = 0f;
	}
}