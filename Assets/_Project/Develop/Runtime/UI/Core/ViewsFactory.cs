using System;
using System.Collections.Generic;
using _Project.Develop.Utilities.AssetsManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.UI.Core
{
    public class ViewsFactory
    {
        private const string MainMenuScreenPath = "UI/MainMenuScreenView";
        private const string GameplayScreenPath = "UI/GameplayScreenView";
        private const string ConfirmPopupPath = "UI/ConfirmPopup";
        private const string GameEndPopupPath = "UI/GameEndPopup";

        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;

        private readonly Dictionary<string, string> _viewIDToResourcesPath = new()
        {
            { ViewIDs.MainMenuScreen, MainMenuScreenPath },
            { ViewIDs.GameplayScreen, GameplayScreenPath },
            { ViewIDs.ConfirmPopup, ConfirmPopupPath },
            { ViewIDs.GameEndPopup, GameEndPopupPath }
        };

        public ViewsFactory(ResourcesAssetsLoader resourcesAssetsLoader)
        {
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }

        public TView Create<TView>(string viewID, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_viewIDToResourcesPath.TryGetValue(viewID, out string resourcePath) == false)
            {
                throw new ArgumentException($"You didn't set resource path for {typeof(TView)}, searched id: {viewID}");
            }

            GameObject prefab = _resourcesAssetsLoader.Load<GameObject>(resourcePath);
            
            if (prefab == null)
            {
                throw new InvalidOperationException($"Prefab not found at path: {resourcePath}");
            }

            GameObject instance = Object.Instantiate(prefab, parent);
            TView view = instance.GetComponent<TView>();

            if (view == null)
            {
                throw new InvalidOperationException($"Not found {typeof(TView)} component on view instance");
            }

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }
}