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
    public partial class Gs2DatastoreDataObjectRestoreDataObjectAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Datastore.Namespace(
                this._context.DataObject.NamespaceName
            );
            var future = domain.RestoreDataObject(
                DataObjectId
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

                        this.onRestoreDataObjectComplete.Invoke(future3.Result);
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

            this.onRestoreDataObjectComplete.Invoke(future2.Result);
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

    public partial class Gs2DatastoreDataObjectRestoreDataObjectAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2DatastoreOwnDataObjectContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2DatastoreOwnDataObjectContext>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreOwnDataObjectContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            this._context = GetComponent<Gs2DatastoreOwnDataObjectContext>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectContext>(true);
            if (_context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreDataObjectRestoreDataObjectAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2DatastoreDataObjectRestoreDataObjectAction
    {
        public string DataObjectId;

        public void SetDataObjectId(string value) {
            DataObjectId = value;
            this.onChangeDataObjectId.Invoke(DataObjectId);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectRestoreDataObjectAction
    {

        [Serializable]
        private class ChangeDataObjectIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeDataObjectIdEvent onChangeDataObjectId = new ChangeDataObjectIdEvent();
        public event UnityAction<string> OnChangeDataObjectId
        {
            add => this.onChangeDataObjectId.AddListener(value);
            remove => this.onChangeDataObjectId.RemoveListener(value);
        }

        [Serializable]
        private class RestoreDataObjectCompleteEvent : UnityEvent<EzDataObject>
        {

        }

        [SerializeField]
        private RestoreDataObjectCompleteEvent onRestoreDataObjectComplete = new RestoreDataObjectCompleteEvent();
        public event UnityAction<EzDataObject> OnRestoreDataObjectComplete
        {
            add => this.onRestoreDataObjectComplete.AddListener(value);
            remove => this.onRestoreDataObjectComplete.RemoveListener(value);
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
    public partial class Gs2DatastoreDataObjectRestoreDataObjectAction
    {
        [MenuItem("GameObject/Game Server Services/Datastore/DataObject/Action/RestoreDataObject", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2DatastoreDataObjectRestoreDataObjectAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Datastore/Prefabs/Action/Gs2DatastoreDataObjectRestoreDataObjectAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}