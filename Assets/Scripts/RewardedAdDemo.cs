using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;

public class RewardedAdDemo : MonoBehaviour {


    private RewardedAd _rewardedAd;
    private int _retryCount = 0;
    private int _retryLimit = 5;



    private void Start() {
        MobileAds.Initialize(initStatus => { });

        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
        .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
        .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        StartCoroutine(waitForAd());
    }

    private IEnumerator waitForAd() {
        yield return new WaitForSeconds(1f);
        CreateAndLoadRewardedAd();
    }


    public void UserChoseToWatchAd() {
        if (this._rewardedAd.IsLoaded()) {
            GameManager.instance.PauseGame();
            this._rewardedAd.Show();
        }
        else {
            CreateAndLoadRewardedAd();
            GameManager.instance.ReloadPlanetScene();
        }
    }



    //test = ca-app-pub-3940256099942544/5224354917
    //test = ca-app-pub-3940256099942544~3347511713
    public void CreateAndLoadRewardedAd() {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
        string adUnitId = "unexpected_platform";
#endif

        this._rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this._rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this._rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this._rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this._rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this._rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this._rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args) {
        this.CreateAndLoadRewardedAd();
    }


    public void HandleRewardedAdLoaded(object sender, EventArgs args) {
        _retryCount = 0;
    }

    public void HandleRewardedAdFailedToLoad(object sender, EventArgs args) {
        if (_retryCount < _retryLimit) {
            CreateAndLoadRewardedAd();
            _retryCount++;
        }
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args) {
        //MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args) {

    }

    public void HandleUserEarnedReward(object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);


        GameManager.instance.ReloadPlanetScene();
    }


}
