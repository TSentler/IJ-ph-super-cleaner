using UnityEngine;
using UnityEngine.UI;

namespace YaVk
{
    [RequireComponent(typeof(Button))]
    public class AdsButton : MonoBehaviour
    {
        private readonly SocialNetwork _socialNetwork = SocialNetwork.Instance;
        
        public void ShowInterstitialAds()
        {
            _socialNetwork.ShowInterstitialAds(
                error => Debug.Log("Show ads Error" + error),
                wasShown => Debug.Log("Ya ads close " + wasShown),
                () => Debug.Log("Ads open"),
                () => Debug.Log("Ya ads offline")
            );
        }
        
        public void ShowRewardedAds()
        {
            _socialNetwork.ShowRewardedAds(
                () => Debug.Log("Ads reward"),
                error => Debug.Log("Show rewarded ads Error" + error),
                () => Debug.Log("Ya rewaeded close"),
                () => Debug.Log("Ya rewarded open")
            );
        }
    }
}
