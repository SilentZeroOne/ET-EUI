using System;
using TMPro;
using UnityEngine;

namespace ET
{
    public class DlgDownloadingBg: MonoBehaviour
    {
        private static DlgDownloadingBg _instance;
        public static DlgDownloadingBg Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject fixedRoot = GameObject.Find("Global").GetComponent<ReferenceCollector>().Get<GameObject>("FixedRoot");
                    GameObject obj = Resources.Load<GameObject>("UI/DownLoadingBG");
                    var loadingBG = Instantiate(obj, fixedRoot.transform);
                    _instance = loadingBG.GetComponent<DlgDownloadingBg>();
                }

                return _instance;
            }
        }

        private TextMeshProUGUI _totalProgress;
        private TextMeshProUGUI _currentProgress;
        private float _totalProgressValue;
        
        private void Awake()
        {
            var referenceCollect = GetComponent<ReferenceCollector>();
            this._totalProgress = referenceCollect.Get<TextMeshProUGUI>("TotalProgress");
            this._currentProgress = referenceCollect.Get<TextMeshProUGUI>("CurrentProgress");
        }

        public void UpdateCurrentProgress(float progress)
        {
            var value = progress / 100 * _totalProgressValue;
            this._currentProgress.SetText((value).ToString());
        }

        public void UpdateTotalProgress(float progress)
        {
            _totalProgressValue = progress;
            this._totalProgress.SetText($"KB/{progress}KB");
        }
    }
}