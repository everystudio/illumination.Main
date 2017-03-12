using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsInterstential : MonoBehaviour {
#if UNITY_EDITOR
	string adUnitId1 = "unused";
#elif UNITY_ANDROID
    string adUnitId1 = "ca-app-pub-5869235725006697/2551356364";
#elif UNITY_IPHONE
    string adUnitId1 = "ca-app-pub-5869235725006697/8458289166";
#else
    string adUnitId1 = "unexpected_platform";
#endif
	InterstitialAd interstitial;
	private AdRequest request;
	// Use this for initialization

	public bool IsLoaded {
		get
		{
			if(interstitial == null)
			{
				return false;
			}
			return interstitial.IsLoaded();
		}
	}

	public bool Show()
	{
		if(IsLoaded)
		{
			interstitial.Show();
			return true;
		}
		return false;
	}

	public void Load () {
		interstitial = new InterstitialAd(adUnitId1);
		request = new AdRequest.Builder()
					.AddTestDevice("B58A62380C00BF9DC7BA75C756B5F550")
					.AddTestDevice("30ec665ef7c68238905003e951174579")
					.Build();
		interstitial.LoadAd(request);
		interstitial.OnAdClosed += AdClosed;
	}

	// インタースティシャル広告を閉じた時に走る
	void AdClosed(object sender, System.EventArgs e)
	{
		Load();
	}
}
