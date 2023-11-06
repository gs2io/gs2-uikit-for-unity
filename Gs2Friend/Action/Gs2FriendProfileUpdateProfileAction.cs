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
using Gs2.Unity.Gs2Friend.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using UnityEngine;
using UnityEngine.Events;
using Profile = Gs2.Unity.Gs2Friend.ScriptableObject.OwnProfile;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Friend
{
    public partial class Gs2FriendProfileUpdateProfileAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onUpdateProfileStart.Invoke();

            
            var domain = clientHolder.Gs2.Friend.Namespace(
                this._context.Profile.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Profile(
            );
            var future = domain.UpdateProfileFuture(
                PublicProfile,
                FollowerProfile,
                FriendProfile
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
                        var future3 = future.Result.ModelFuture();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onUpdateProfileComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.ModelFuture();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onUpdateProfileComplete.Invoke(future2.Result);
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

    public partial class Gs2FriendProfileUpdateProfileAction
    {
        private Gs2FriendOwnProfileContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2FriendOwnProfileContext>() ?? GetComponentInParent<Gs2FriendOwnProfileContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnProfileContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2FriendOwnProfileContext>() ?? GetComponentInParent<Gs2FriendOwnProfileContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendProfileUpdateProfileAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2FriendProfileUpdateProfileAction
    {
        public bool WaitAsyncProcessComplete;
        public string PublicProfile;
        public string FollowerProfile;
        public string FriendProfile;

        public void SetPublicProfile(string value) {
            this.PublicProfile = value;
            this.onChangePublicProfile.Invoke(this.PublicProfile);
            this.OnChange.Invoke();
        }

        public void SetFollowerProfile(string value) {
            this.FollowerProfile = value;
            this.onChangeFollowerProfile.Invoke(this.FollowerProfile);
            this.OnChange.Invoke();
        }

        public void SetFriendProfile(string value) {
            this.FriendProfile = value;
            this.onChangeFriendProfile.Invoke(this.FriendProfile);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendProfileUpdateProfileAction
    {

        [Serializable]
        private class ChangePublicProfileEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangePublicProfileEvent onChangePublicProfile = new ChangePublicProfileEvent();
        public event UnityAction<string> OnChangePublicProfile
        {
            add => this.onChangePublicProfile.AddListener(value);
            remove => this.onChangePublicProfile.RemoveListener(value);
        }

        [Serializable]
        private class ChangeFollowerProfileEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeFollowerProfileEvent onChangeFollowerProfile = new ChangeFollowerProfileEvent();
        public event UnityAction<string> OnChangeFollowerProfile
        {
            add => this.onChangeFollowerProfile.AddListener(value);
            remove => this.onChangeFollowerProfile.RemoveListener(value);
        }

        [Serializable]
        private class ChangeFriendProfileEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeFriendProfileEvent onChangeFriendProfile = new ChangeFriendProfileEvent();
        public event UnityAction<string> OnChangeFriendProfile
        {
            add => this.onChangeFriendProfile.AddListener(value);
            remove => this.onChangeFriendProfile.RemoveListener(value);
        }

        [Serializable]
        private class UpdateProfileStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private UpdateProfileStartEvent onUpdateProfileStart = new UpdateProfileStartEvent();

        public event UnityAction OnUpdateProfileStart
        {
            add => this.onUpdateProfileStart.AddListener(value);
            remove => this.onUpdateProfileStart.RemoveListener(value);
        }

        [Serializable]
        private class UpdateProfileCompleteEvent : UnityEvent<EzProfile>
        {

        }

        [SerializeField]
        private UpdateProfileCompleteEvent onUpdateProfileComplete = new UpdateProfileCompleteEvent();
        public event UnityAction<EzProfile> OnUpdateProfileComplete
        {
            add => this.onUpdateProfileComplete.AddListener(value);
            remove => this.onUpdateProfileComplete.RemoveListener(value);
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
    public partial class Gs2FriendProfileUpdateProfileAction
    {
        [MenuItem("GameObject/Game Server Services/Friend/Profile/Action/UpdateProfile", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2FriendProfileUpdateProfileAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Friend/Prefabs/Action/Gs2FriendProfileUpdateProfileAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}