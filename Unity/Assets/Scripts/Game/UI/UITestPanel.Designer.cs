using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:a21aa4ef-c733-4e88-98c4-8eb9023b33b5
	public partial class UITestPanel
	{
		public const string Name = "UITestPanel";
		
		[SerializeField]
		public QFramework.Example.AddControl AddControl;
		
		private UITestPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			AddControl = null;
			
			mData = null;
		}
		
		public UITestPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITestPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITestPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
