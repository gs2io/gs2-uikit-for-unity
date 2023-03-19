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
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using UnityEngine;
using UnityEngine.Events;
using DataObject = Gs2.Unity.Gs2Datastore.ScriptableObject.OwnDataObject;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Datastore
{
	[AddComponentMenu("GS2 UIKit/Datastore/DataObject/Action/Gs2DatastoreDataObjectPrepareUploadAction")]
    public partial class Gs2DatastoreDataObjectPrepareUploadAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Datastore.Namespace(
                this._context.DataObject.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            );
            var future = domain.PrepareUpload(
                Name,
                Scope,
                ContentType,
                AllowUserIds.ToArray(),
                UpdateIfExists
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

                        this.onPrepareUploadComplete.Invoke(future3.Result);
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

            this.onPrepareUploadComplete.Invoke(future2.Result);
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

    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2DatastoreOwnDataObjectContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2DatastoreOwnDataObjectContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreOwnDataObjectContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {
        public string Name;
        public string Scope;
        public string ContentType;
        public List<string> AllowUserIds;
        public bool UpdateIfExists;

        public void SetName(string value) {
            Name = value;
            this.onChangeName.Invoke(Name);
        }

        public void SetScope(string value) {
            Scope = value;
            this.onChangeScope.Invoke(Scope);
        }

        public void SetContentType(string value) {
            ContentType = value;
            this.onChangeContentType.Invoke(ContentType);
        }

        public void SetAllowUserIds(List<string> value) {
            AllowUserIds = value;
            this.onChangeAllowUserIds.Invoke(AllowUserIds);
        }

        public void SetUpdateIfExists(bool value) {
            UpdateIfExists = value;
            this.onChangeUpdateIfExists.Invoke(UpdateIfExists);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {

        [Serializable]
        private class ChangeNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeNameEvent onChangeName = new ChangeNameEvent();
        public event UnityAction<string> OnChangeName
        {
            add => this.onChangeName.AddListener(value);
            remove => this.onChangeName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeScopeEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeScopeEvent onChangeScope = new ChangeScopeEvent();
        public event UnityAction<string> OnChangeScope
        {
            add => this.onChangeScope.AddListener(value);
            remove => this.onChangeScope.RemoveListener(value);
        }

        [Serializable]
        private class ChangeContentTypeEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeContentTypeEvent onChangeContentType = new ChangeContentTypeEvent();
        public event UnityAction<string> OnChangeContentType
        {
            add => this.onChangeContentType.AddListener(value);
            remove => this.onChangeContentType.RemoveListener(value);
        }

        [Serializable]
        private class ChangeAllowUserIdsEvent : UnityEvent<List<string>>
        {

        }

        [SerializeField]
        private ChangeAllowUserIdsEvent onChangeAllowUserIds = new ChangeAllowUserIdsEvent();
        public event UnityAction<List<string>> OnChangeAllowUserIds
        {
            add => this.onChangeAllowUserIds.AddListener(value);
            remove => this.onChangeAllowUserIds.RemoveListener(value);
        }

        [Serializable]
        private class ChangeUpdateIfExistsEvent : UnityEvent<bool>
        {

        }

        [SerializeField]
        private ChangeUpdateIfExistsEvent onChangeUpdateIfExists = new ChangeUpdateIfExistsEvent();
        public event UnityAction<bool> OnChangeUpdateIfExists
        {
            add => this.onChangeUpdateIfExists.AddListener(value);
            remove => this.onChangeUpdateIfExists.RemoveListener(value);
        }

        [Serializable]
        private class PrepareUploadCompleteEvent : UnityEvent<EzDataObject>
        {

        }

        [SerializeField]
        private PrepareUploadCompleteEvent onPrepareUploadComplete = new PrepareUploadCompleteEvent();
        public event UnityAction<EzDataObject> OnPrepareUploadComplete
        {
            add => this.onPrepareUploadComplete.AddListener(value);
            remove => this.onPrepareUploadComplete.RemoveListener(value);
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
    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {
        [MenuItem("GameObject/Game Server Services/Datastore/DataObject/Action/PrepareUpload", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2DatastoreDataObjectPrepareUploadAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Datastore/Prefabs/Action/Gs2DatastoreDataObjectPrepareUploadAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}