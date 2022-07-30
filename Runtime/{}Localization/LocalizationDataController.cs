/* Created by Max.K.Kimo */

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.IO;

using Newtonsoft.Json;

namespace PixLi
{
	public class LocalizationDataController : MonoBehaviourSingleton<LocalizationDataController>
	{
		/// <summary>
		/// Primary Key
		/// Secondary Key
		/// </summary>

		public const string DEFAULT_LOCALIZATIONFOLDER_NAME = "#Localization Data";

		[SerializeField] private string _activeLocalizationFilePath;
		public string _ActiveLocalizationFilePath => this._activeLocalizationFilePath;

		private Dictionary<string, Dictionary<string, string>> _objectKey_jsonDataRelation;

		public Dictionary<string, string> GetPrimaryData(Localizer localizer)
		{
			return this._objectKey_jsonDataRelation[localizer._PrimaryKey];
		}

		public string GetData(Localizer localizer)
		{
			return this._objectKey_jsonDataRelation[localizer._PrimaryKey][localizer._SecondaryKey];
		}

		private List<Localizer> _localizers = new List<Localizer>(128);
		public List<Localizer> _Localizers => this._localizers;
		
		public void InitializeRelations()
		{
			string jsonData = File.ReadAllText(
				path: Path.Combine(
					Application.streamingAssetsPath,
					DEFAULT_LOCALIZATIONFOLDER_NAME,
					this._activeLocalizationFilePath
				)
			);

			this._objectKey_jsonDataRelation = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(
				value: jsonData, 
				settings: this._jsonSerializerSettings
			);

			for (int i = 0; i < this._localizers.Count; i++)
			{
				this._localizers[i].Localize();
			}
		}

		public void InitializeRelations(string localizationFilePath)
		{
			this._activeLocalizationFilePath = localizationFilePath;

			this.InitializeRelations();
		}

		public void InitializeRelationsBySystemLanguage()
		{
			this.InitializeRelations(Path.Combine(DEFAULT_LOCALIZATIONFOLDER_NAME, Application.systemLanguage + ".json"));
		}

		private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

		protected override void Awake()
		{
			base.Awake();

			this._jsonSerializerSettings.Formatting = Formatting.Indented;
			this._jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

#if UNITY_EDITOR
			Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, DEFAULT_LOCALIZATIONFOLDER_NAME));

			AssetDatabase.Refresh();
#endif

			this.InitializeRelations("Ukrainian.json");
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
	[CustomEditor(typeof(LocalizationDataController))]
	[CanEditMultipleObjects]
	public class LocalizationDataControllerEditor : Editor
	{
#pragma warning disable 0219, 414
		private LocalizationDataController _sLocalizationDataController;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sLocalizationDataController = this.target as LocalizationDataController;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}