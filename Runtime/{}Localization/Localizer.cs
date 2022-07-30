/* Created by Max.K.Kimo */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	public abstract class Localizer : MonoBehaviour
	{
		[SerializeField] private string _primaryKey;
		public string _PrimaryKey => this._primaryKey;

		[SerializeField] private string _secondaryKey;
		public string _SecondaryKey => this._secondaryKey;

		public abstract void Localize();

		protected virtual void Awake()
		{
			LocalizationDataController._Instance._Localizers.Add(this);

			this.Localize();
		}

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}
#endif
	}

	public abstract class Localizer<T> : Localizer
		where T : Component
    {
		protected T _localizationObject;

		protected override void Awake()
		{
			this._localizationObject = this.GetComponent<T>();

			base.Awake();
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
    [CustomEditor(typeof(Localizer))]
    [CanEditMultipleObjects]
    public class LocalizerEditor : Editor
    {
#pragma warning disable 0219, 414
        private Localizer _sLocalizer;
#pragma warning restore 0219, 414

        private void OnEnable()
        {
            this._sLocalizer = target as Localizer;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
#endif
}