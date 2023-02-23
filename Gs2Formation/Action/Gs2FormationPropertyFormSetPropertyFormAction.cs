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
using PropertyForm = Gs2.Unity.Gs2Formation.ScriptableObject.OwnPropertyForm;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Formation
{
	[AddComponentMenu("GS2 UIKit/Formation/PropertyForm/Action/Gs2FormationPropertyFormSetPropertyFormAction")]
    public partial class Gs2FormationPropertyFormSetPropertyFormAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Formation.Namespace(
                this._context.PropertyForm.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).PropertyForm(
                this._context.PropertyForm.FormModelName,
                this._context.PropertyForm.PropertyId
            );
            var future = domain.SetPropertyForm(
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

                        this.onSetPropertyFormComplete.Invoke(future3.Result);
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

            this.onSetPropertyFormComplete.Invoke(future2.Result);
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

    public partial class Gs2FormationPropertyFormSetPropertyFormAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2FormationOwnPropertyFormContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2FormationOwnPropertyFormContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnPropertyFormContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationPropertyFormSetPropertyFormAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2FormationPropertyFormSetPropertyFormAction
    {
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
    public partial class Gs2FormationPropertyFormSetPropertyFormAction
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
        private class SetPropertyFormCompleteEvent : UnityEvent<EzPropertyForm>
        {

        }

        [SerializeField]
        private SetPropertyFormCompleteEvent onSetPropertyFormComplete = new SetPropertyFormCompleteEvent();
        public event UnityAction<EzPropertyForm> OnSetPropertyFormComplete
        {
            add => this.onSetPropertyFormComplete.AddListener(value);
            remove => this.onSetPropertyFormComplete.RemoveListener(value);
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
    public partial class Gs2FormationPropertyFormSetPropertyFormAction
    {
        [MenuItem("GameObject/Game Server Services/Formation/PropertyForm/Action/SetPropertyForm", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2FormationPropertyFormSetPropertyFormAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Formation/Prefabs/Action/Gs2FormationPropertyFormSetPropertyFormAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}