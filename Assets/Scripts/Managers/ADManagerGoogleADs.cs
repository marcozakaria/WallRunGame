using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class ADManagerGoogleADs : MonoBehaviour
{
    public static ADManagerGoogleADs instance = null;

    public int lossesToDisplayAD = 2;
    private static int currentlossCount = 0;

    private readonly string appID = "ca-app-pub-8130926851647334~3583814452";

    public BannerView bannerView;
    private readonly string bannerADID = "ca-app-pub-3940256099942544/6300978111";

    private InterstitialAd videoInterstitialAd;
    private readonly string videoID = "ca-app-pub-3940256099942544/1033173712";

    //private RewardBasedVideoAd rewardBasedVideoAd;
    // private readonly string rewardedVideoID = "ca-app-pub-3940256099942544/5224354917";

    static bool initialized = false;

    private void Awake()
    {
        if (instance == null) //singletone
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);      
    }

    private void Start()
    {
        if (initialized)
        {
            return;
        }
        MobileAds.Initialize(appID);

        RequestVideoAd(); // to load ad in memory

        //this.rewardBasedVideoAd = RewardBasedVideoAd.Instance;
       // RequestRewardedVideoAD();

        RequestBannerAD();
        initialized = true;
    }

    public void RequestBannerAD()
    {
        if (bannerView != null)
        {
            this.bannerView.Destroy();
        }

        /*if (Screen.height != Display.main.systemHeight) // notch deveice
        {
            int yDP = PixelsToBannerSize(Screen.height + Display.main.systemHeight);
            int xDP = Mathf.RoundToInt(PixelsToBannerSize(Display.main.systemWidth - BannerSizeToPixels(AdSize.Banner.Width)) / 2f);
            this.bannerView = new BannerView(bannerADID, AdSize.Banner, xDP, yDP);
        }
        else
        {
            this.bannerView = new BannerView(bannerADID, AdSize.Banner, AdPosition.Bottom);
        }*/
        this.bannerView = new BannerView(bannerADID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
        
        this.bannerView.Show();

        //float yp = Screen.height - bannerView.GetHeightInPixels();
       // Debug.Log(yp);
        //debugText.text = "Show Banner";

        bannerView.OnAdFailedToLoad += BannerFailedToLoad;
        bannerView.OnAdLoaded += BannerLoadedSucc;
        bannerView.OnAdOpening += BannerADOpened;
    }

    private int BannerSizeToPixels(int size) //ScreenPixels
    {
        return size * Mathf.RoundToInt(Screen.dpi / 160);
    }

    private int PixelsToBannerSize(int size) //DensityIndependentPixels
    {
        return size / Mathf.RoundToInt(Screen.dpi / 160);
    }


    private void BannerFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log( "Failed To get Banner");
    }

    private void BannerLoadedSucc(object sender, EventArgs args)
    {
        Debug.Log("Banner Loaded Succ");
    }

    private void BannerADOpened(object sender, EventArgs args)
    {
        Debug.Log("Banner AD Opened succ");
    }

    public void CheckToPLayAD()
    {
        currentlossCount++;
        if (currentlossCount > lossesToDisplayAD)
        {
            ShowVideoAD();
            currentlossCount = 0;
        }
    }

    public void DestroyBannerAD()
    {
        this.bannerView.Destroy();
    }

    public void HideBanner()
    {
        this.bannerView.Hide();
        
    }
    
    public void ShowBanner()
    {
        if (bannerView == null)
        {
            RequestBannerAD();
            return;
        }
        bannerView.Show();
    }

    public void OnDisable()
    {
        if (instance != null)
        {
           // Debug.Log("Destroy AD");
           // DestroyBannerAD();
        }      
    }

    public void RequestVideoAd()
    {
        videoInterstitialAd = new InterstitialAd(videoID);
        AdRequest request = new AdRequest.Builder().Build();
        videoInterstitialAd.LoadAd(request);

        videoInterstitialAd.OnAdFailedToLoad += VideoFalledToLoad;
        videoInterstitialAd.OnAdClosed += HandleClosingVideoAD;
    }

    private void HandleClosingVideoAD(object sender,EventArgs args)
    {
        RequestVideoAd();
    }

    private void VideoFalledToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Failed To get Video");
    }

    public void ShowVideoAD()
    {
        if (videoInterstitialAd.IsLoaded())
        {
            videoInterstitialAd.Show();
           // Debug.Log("PLay Video");
        }
        else
        {
            RequestVideoAd();
            Debug.Log("no availble Video");
        }
    }

   /* public void RequestRewardedVideoAD()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideoAd.LoadAd(request, rewardedVideoID);

        rewardBasedVideoAd.OnAdClosed += HandleOnRewardedADClosed;
    }

    private void HandleOnRewardedADClosed(object sender,EventArgs args)
    {
        RequestRewardedVideoAD();
    }

    public bool CheckRewardVideoISloaded()
    {
        if (rewardBasedVideoAd.IsLoaded())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowRewardAD()
    {
        if (rewardBasedVideoAd.IsLoaded())
        {
            rewardBasedVideoAd.Show();
        }
        else
        {
            RequestRewardedVideoAD();
        }
    }*/
}
