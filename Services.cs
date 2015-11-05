using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;

public class Services : MonoBehaviour {

	public static string leaderboardID = "CgkIjt2bs5YJEAIQCg";
	public static string firstPlayID = "CgkIjt2bs5YJEAIQAQ";
	public static string jumperID = "CgkIjt2bs5YJEAIQAg";
	public static string jumperJumperID = "CgkIjt2bs5YJEAIQBA";
	public static string jumpyJumperID = "CgkIjt2bs5YJEAIQAw";
	public static string tinyDreamerID = "CgkIjt2bs5YJEAIQBQ";
	public static string bigDreamerID = "CgkIjt2bs5YJEAIQBg";
	public static string nowMasterID = "CgkIjt2bs5YJEAIQBw";
	public static string imNotGiveUpID = "CgkIjt2bs5YJEAIQCA";
	public static string imGiveUpID = "CgkIjt2bs5YJEAIQCQ";
	
	public static string admobBannerID = "ca-app-pub-7709531834691151/1102472624";
	public static string admobInterstitialID = "ca-app-pub-7709531834691151/2579205825";
	public static int interstitialShowPerGame = 3;
	
	private static Dictionary<string, int> dictAchievement = new Dictionary<string, int>();
	
	private enum GPGS_STATUS{
		LOGIN, FAILED, AUTHENTICATING
	}
	private static GPGS_STATUS gpgsStatus = GPGS_STATUS.FAILED;
	
	private static BannerView bannerView;
	private static InterstitialAd interstitial;
	
    public static void SetupGPGS(){
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
	
		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		Login();
	}
	
	public static void Login(){
		// authenticate user:
		gpgsStatus = GPGS_STATUS.AUTHENTICATING;
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if(success)
				gpgsStatus = GPGS_STATUS.LOGIN;
			else
				gpgsStatus = GPGS_STATUS.FAILED;
		});
	}
	
	public static void IncrementAchievement(string ids){
		if(dictAchievement.ContainsKey(ids)){
			dictAchievement[ids] += 1;
		}
		else{
			dictAchievement.Add(ids, 1);
		}
	}
	
	public static void FlushAchievement(){
		foreach (string ach in dictAchievement.Keys)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(ach, dictAchievement[ach], (bool success) => { });
            }
            dictAchievement.Clear();
	}
	
	public static void UnlockingAchievement(string ids){
		// unlock achievement (achievement ID "Cfjewijawiu_QA")
		Social.ReportProgress(ids, 100.0f, (bool success) => {
		// handle success or failure
		});
	}
	
	public static void PostScoreLeaderboard(int score){
		// post score 12345 to leaderboard ID "Cfji293fjsie_QA")
		Social.ReportScore(score, leaderboardID, (bool success) => {
			// handle success or failure
		});
	}

	public static void ShowLeaderboard(){
		if(gpgsStatus == GPGS_STATUS.LOGIN)
			Social.ShowLeaderboardUI();
		else if(gpgsStatus == GPGS_STATUS.FAILED)
			Login();
	}	
	public static void ShowAchievement(){
		if(gpgsStatus == GPGS_STATUS.LOGIN)
			Social.ShowAchievementsUI();
		else if(gpgsStatus == GPGS_STATUS.FAILED)
			Login();
	}
	
	
	//ADMOB
	public static void BannerSetup(){
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(
				admobBannerID, AdSize.Banner, AdPosition.Top);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}
	
	public static void InterstitialSetup(){
		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(admobInterstitialID);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}
	
	public static void ShowBanner(){
		bannerView.Show();
	}
	
	public static void ShowInterstitial(){
		if(interstitial.IsLoaded())
			interstitial.Show();
	}
	
}
