using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UPatterns
{
    public enum PanelState { Loading, Error, Data }

    public abstract class UPanel : MonoBehaviour
    {
        private static Dictionary<Type, UPanel> Panels = new();

        public static T GetInstance<T>(Transform parent = null) where T : UPanel
        {
            if (Panels.ContainsKey(typeof(T)))
            {
                var ins = Panels[typeof(T)] as T;
                ins.transform.SetParent(parent);
                return ins;
            }

            var instance = Instantiate(Resources.Load<GameObject>(typeof(T).Name)).GetComponent<T>();
            instance.Initialize();
            Panels.Add(typeof(T), instance);
            instance.transform.SetParent(parent);
            AddEventSystem();
            return instance;
        }

        protected static void RemoveInstance<T>() where T : UPanel
        {
            if (Panels.ContainsKey(typeof(T)))
                Panels.Remove(typeof(T));
        }

        public static UPanel CurrentPanel { private set; get; }
        public void ChangePanel()
        {
            if (CurrentPanel)
                CurrentPanel.Hide();
            CurrentPanel = this;
            CurrentPanel.Show();
        }

        public static void HideCurrentPanel()
        {
            if (CurrentPanel)
                CurrentPanel.Hide();
        }

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

        private static EventSystem eventSystem;
        private static void AddEventSystem()
        {
            if (eventSystem ??= GameObject.FindObjectOfType<EventSystem>())
                return;

            (eventSystem = (new GameObject(nameof(EventSystem)))
                .AddComponent<EventSystem>())
                .gameObject.AddComponent<StandaloneInputModule>();
        }
    }

    public abstract class UPanel<T> : UPanel where T : UPanel
    {
        public static T Instance =>
            GetInstance<T>();

        public static T GetInstance(Transform parent = null) =>
             GetInstance<T>(parent);

        public virtual void OnDestroy() =>
            RemoveInstance<T>();
    }

    public abstract class UPanelDataLoader<T> : UPanel<T> where T : UPanel
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