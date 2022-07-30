/* Created by Max.K.Kimo */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

namespace PixLi
{
	public class TextMeshProLocalizer : Localizer<TMP_Text>
	{
		public override void Localize()
		{
			this._localizationObject.text = LocalizationDataController._Instance.GetData(this);
		}

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}
#endif
	}
}

namespace PixLi
{
#if UNITY_EDITOR
	[CustomEditor(typeof(TextMeshProLocalizer))]
	[CanEditMultipleObjects]
	public class TextMeshProLocalizerEditor : Editor
	{
#pragma warning disable 0219, 414
		private TextMeshProLocalizer _sTextMeshProLocalizer;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sTextMeshProLocalizer = this.target as TextMeshProLocalizer;
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
		}
	}
#endif
}