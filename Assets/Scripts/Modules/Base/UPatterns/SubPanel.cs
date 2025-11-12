using System;
using UnityEngine;

namespace UPatterns
{

    public abstract class USubPanel : MonoBehaviour
    {
        public bool IsShow { protected set; get; }
        public event Action onShow;
        public event Action onHide;
        public void SetOnShow(Action action) => onShow = action;
        public void SetOnHide(Action action) => onHide = action;

        public virtual void Initialize() { }
        public virtual void Show()
        {
            IsShow = true;
            gameObject.SetActive(true);
            onShow?.Invoke();
            transform.SetAsLastSibling();
        }

        public virtual void Hide()
        {
            IsShow = false;
            gameObject.SetActive(false);
            onHide?.Invoke();
        }
    }

    public abstract class USubPanelDataLoader : USubPanel
    {
        [SerializeField] private GameObject LoadingPanel;
        [SerializeField] private GameObject ErrorPanel;
        [SerializeField] private GameObject DataPanel;

        public void SetState(PanelState state)
        {
            switch (state)
            {
                case PanelState.Loading:
                    Set(loading: true);
                    OnLoading();
                    break;
                case PanelState.Error:
                    Set(error: true);
                    OnError();
                    break;
                case PanelState.Data:
                    Set(data: true);
                    OnLoadData();
                    break;
            }

            void Set(bool loading = false, bool error = false, bool data = false)
            {
                if (LoadingPanel) LoadingPanel.SetActive(loading);
                if (ErrorPanel) ErrorPanel.SetActive(error);
                if (DataPanel) DataPanel.SetActive(data);
            }
        }

        public virtual void OnLoadData() { }
        public virtual void OnError() { }
        public virtual void OnLoading() { }

        public virtual void FetchData() { }

        public virtual void BaseShow() => base.Show();

        public override void Show()
        {
            base.Show();
            FetchData();
            SetState(PanelState.Loading);
        }
    }
}