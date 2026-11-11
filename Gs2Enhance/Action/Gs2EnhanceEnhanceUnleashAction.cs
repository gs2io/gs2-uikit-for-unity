/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Enhance.Context;
using UnityEngine;
using UnityEngine.Events;
using User = Gs2.Unity.Gs2Enhance.ScriptableObject.User;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    public partial class Gs2EnhanceEnhanceUnleashAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onUnleashStart.Invoke();

            
            var domain = clientHolder.Gs2.Enhance.Namespace(
                this._context.Namespace.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Enhance(
            );
            var future = domain.UnleashFuture(
                RateName,
                TargetItemSetId,
                Materials.ToArray(),
                RecipeName,
                RecipeMaterials.ToArray(),
                Config.ToArray()
            );
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is TransactionException e)
                {
                    IEnumerator Retry()
                    {
                        var retryFuture = e.Retry();
                        yield return retryFuture;
                        if (retryFuture.Error != null)
                        {
                            this.onError.Invoke(future.Error, Retry);
                            yield break;
                        }
                        this.onUnleashComplete.Invoke();
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            if (this.WaitAsyncProcessComplete && future.Result != null) {
                var transaction = future.Result;
                var future2 = transaction.WaitFuture();
                yield return future2;
            }
            this.onUnleashComplete.Invoke();
        }

        public void OnEnable()
        {
            Gs2ClientHolder.Instance.StartCoroutine(Process());
        }

        public void OnDisable()
        {

        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnhanceEnhanceUnleashAction
    {
        private Gs2EnhanceNamespaceContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2EnhanceNamespaceContext>() ?? GetComponentInParent<Gs2EnhanceNamespaceContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnhanceNamespaceContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2EnhanceNamespaceContext>() ?? GetComponentInParent<Gs2EnhanceNamespaceContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnhanceEnhanceUnleashAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2EnhanceEnhanceUnleashAction
    {
        public bool WaitAsyncProcessComplete;
        public string RateName;
        public string TargetItemSetId;
        public List<string> Materials;
        public string RecipeName;
        public List<Gs2.Unity.Gs2Enhance.Model.EzUnleashMaterialSelection> RecipeMaterials;
        public List<Gs2.Unity.Gs2Enhance.Model.EzConfig> Config;

        public void SetRateName(string value) {
            this.RateName = value;
            this.onChangeRateName.Invoke(this.RateName);
            this.OnChange.Invoke();
        }

        public void SetTargetItemSetId(string value) {
            this.TargetItemSetId = value;
            this.onChangeTargetItemSetId.Invoke(this.TargetItemSetId);
            this.OnChange.Invoke();
        }

        public void SetMaterials(List<string> value) {
            this.Materials = value;
            this.onChangeMaterials.Invoke(this.Materials);
            this.OnChange.Invoke();
        }

        public void SetRecipeName(string value) {
            this.RecipeName = value;
            this.onChangeRecipeName.Invoke(this.RecipeName);
            this.OnChange.Invoke();
        }

        public void SetRecipeMaterials(List<Gs2.Unity.Gs2Enhance.Model.EzUnleashMaterialSelection> value) {
            this.RecipeMaterials = value;
            this.onChangeRecipeMaterials.Invoke(this.RecipeMaterials);
            this.OnChange.Invoke();
        }

        public void SetConfig(List<Gs2.Unity.Gs2Enhance.Model.EzConfig> value) {
            this.Config = value;
            this.onChangeConfig.Invoke(this.Config);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceEnhanceUnleashAction
    {

        [Serializable]
        private class ChangeRateNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeRateNameEvent onChangeRateName = new ChangeRateNameEvent();
        public event UnityAction<string> OnChangeRateName
        {
            add => this.onChangeRateName.AddListener(value);
            remove => this.onChangeRateName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeTargetItemSetIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeTargetItemSetIdEvent onChangeTargetItemSetId = new ChangeTargetItemSetIdEvent();
        public event UnityAction<string> OnChangeTargetItemSetId
        {
            add => this.onChangeTargetItemSetId.AddListener(value);
            remove => this.onChangeTargetItemSetId.RemoveListener(value);
        }

        [Serializable]
        private class ChangeMaterialsEvent : UnityEvent<List<string>>
        {

        }

        [SerializeField]
        private ChangeMaterialsEvent onChangeMaterials = new ChangeMaterialsEvent();
        public event UnityAction<List<string>> OnChangeMaterials
        {
            add => this.onChangeMaterials.AddListener(value);
            remove => this.onChangeMaterials.RemoveListener(value);
        }

        [Serializable]
        private class ChangeRecipeNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeRecipeNameEvent onChangeRecipeName = new ChangeRecipeNameEvent();
        public event UnityAction<string> OnChangeRecipeName
        {
            add => this.onChangeRecipeName.AddListener(value);
            remove => this.onChangeRecipeName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeRecipeMaterialsEvent : UnityEvent<List<Gs2.Unity.Gs2Enhance.Model.EzUnleashMaterialSelection>>
        {

        }

        [SerializeField]
        private ChangeRecipeMaterialsEvent onChangeRecipeMaterials = new ChangeRecipeMaterialsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Enhance.Model.EzUnleashMaterialSelection>> OnChangeRecipeMaterials
        {
            add => this.onChangeRecipeMaterials.AddListener(value);
            remove => this.onChangeRecipeMaterials.RemoveListener(value);
        }

        [Serializable]
        private class ChangeConfigEvent : UnityEvent<List<Gs2.Unity.Gs2Enhance.Model.EzConfig>>
        {

        }

        [SerializeField]
        private ChangeConfigEvent onChangeConfig = new ChangeConfigEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Enhance.Model.EzConfig>> OnChangeConfig
        {
            add => this.onChangeConfig.AddListener(value);
            remove => this.onChangeConfig.RemoveListener(value);
        }

        [Serializable]
        private class UnleashStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private UnleashStartEvent onUnleashStart = new UnleashStartEvent();

        public event UnityAction OnUnleashStart
        {
            add => this.onUnleashStart.AddListener(value);
            remove => this.onUnleashStart.RemoveListener(value);
        }

        [Serializable]
        private class UnleashCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private UnleashCompleteEvent onUnleashComplete = new UnleashCompleteEvent();
        public event UnityAction OnUnleashComplete
        {
            add => this.onUnleashComplete.AddListener(value);
            remove => this.onUnleashComplete.RemoveListener(value);
        }

        public UnityEvent OnChange = new UnityEvent();

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }

#if UNITY_EDITOR

    /// <summary>
    /// Context Menu
    /// </summary>
    public partial class Gs2EnhanceEnhanceUnleashAction
    {
        [MenuItem("GameObject/Game Server Services/Enhance/Enhance/Action/Unleash", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2EnhanceEnhanceUnleashAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Enhance/Prefabs/Action/Gs2EnhanceEnhanceUnleashAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}