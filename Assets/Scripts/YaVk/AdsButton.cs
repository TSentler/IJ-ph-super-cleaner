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
            _socialNetwork.ShowInterstitialAds(wasShown => Debug.Log("Ya ads close " + wasShown),
                error => Debug.Log("Show ads Error" + error),
                () => Debug.Log("Ads open"), () => Debug.Log("Ya ads offline"));
        }
        
        public void ShowRewardedAds()
        {
            _socialNetwork.ShowRewardedAds(
                () => Debug.Log("Ads reward"),
                () => Debug.Log("Ya rewaeded close"),
                error => Debug.Log("Show rewarded ads Error" + error), () => Debug.Log("Ya rewarded open"));
        }
    }
}
