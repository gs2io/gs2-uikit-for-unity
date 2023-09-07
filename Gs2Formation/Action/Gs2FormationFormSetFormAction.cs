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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using UnityEngine;
using UnityEngine.Events;
using Form = Gs2.Unity.Gs2Formation.ScriptableObject.OwnForm;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Formation
{
    public partial class Gs2FormationFormSetFormAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Formation.Namespace(
                this._context.Form.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Mold(
                this._context.Form.MoldModelName
            ).Form(
                this._context.Form.Index
            );
            var future = domain.SetForm(
                Slots.ToArray(),
                KeyId
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
                        var future3 = future.Result.Model();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onSetFormComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.Model();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onSetFormComplete.Invoke(future2.Result);
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationFormSetFormAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2FormationOwnFormContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2FormationOwnFormContext>() ?? GetComponentInParent<Gs2FormationOwnFormContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnFormContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2FormationOwnFormContext>() ?? GetComponentInParent<Gs2FormationOwnFormContext>(true);
            if (_context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationFormSetFormAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2FormationFormSetFormAction
    {
        public bool WaitAsyncProcessComplete;
        public List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature> Slots;
        public string KeyId;

        public void SetSlots(List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature> value) {
            Slots = value;
            this.onChangeSlots.Invoke(Slots);
        }

        public void SetKeyId(string value) {
            KeyId = value;
            this.onChangeKeyId.Invoke(KeyId);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationFormSetFormAction
    {

        [Serializable]
        private class ChangeSlotsEvent : UnityEvent<List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature>>
        {

        }

        [SerializeField]
        private ChangeSlotsEvent onChangeSlots = new ChangeSlotsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature>> OnChangeSlots
        {
            add => this.onChangeSlots.AddListener(value);
            remove => this.onChangeSlots.RemoveListener(value);
        }

        [Serializable]
        private class ChangeKeyIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeKeyIdEvent onChangeKeyId = new ChangeKeyIdEvent();
        public event UnityAction<string> OnChangeKeyId
        {
            add => this.onChangeKeyId.AddListener(value);
            remove => this.onChangeKeyId.RemoveListener(value);
        }

        [Serializable]
        private class SetFormCompleteEvent : UnityEvent<EzForm>
        {

        }

        [SerializeField]
        private SetFormCompleteEvent onSetFormComplete = new SetFormCompleteEvent();
        public event UnityAction<EzForm> OnSetFormComplete
        {
            add => this.onSetFormComplete.AddListener(value);
            remove => this.onSetFormComplete.RemoveListener(value);
        }

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
    public partial class Gs2FormationFormSetFormAction
    {
        [MenuItem("GameObject/Game Server Services/Formation/Form/Action/SetForm", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2FormationFormSetFormAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Formation/Prefabs/Action/Gs2FormationFormSetFormAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}