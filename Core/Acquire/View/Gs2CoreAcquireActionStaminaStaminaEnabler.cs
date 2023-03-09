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
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Gs2Stamina.Request;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Acquire;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Core
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/AcquireAction/View/Gs2CoreAcquireActionStaminaStaminaEnabler")]
    public partial class Gs2CoreAcquireActionStaminaStaminaEnabler : MonoBehaviour
    {
        public IEnumerator Process() {
            while (true) {
                yield return new WaitForSeconds(0.1f);
                
                if (_context.AcquireActions != null && _context.AcquireActions.Count(v => v.Action == "Gs2Stamina:RecoverStaminaByUserId") > 0) {
                    var request = RecoverStaminaByUserIdRequest.FromJson(JsonMapper.ToObject(_context.AcquireActions.First(v => v.Action == "Gs2Stamina:RecoverStaminaByUserId").Request));
                    
                    if (request == null) continue;

                    var future = this._clientHolder.Gs2.Stamina.Namespace(
                        request.NamespaceName
                    ).Me(
                        this._sessionHolder.GameSession
                    ).Stamina(
                        request.StaminaName
                    ).Model();
                    yield return future;
                    if (future.Error != null) {
                        this.onError.Invoke(new CanIgnoreException(future.Error), null);
                    }

                    var stamina = future.Result;
                    
                    if (stamina == null) continue;
                    
                    var future2 = this._clientHolder.Gs2.Stamina.Namespace(
                        request.NamespaceName
                    ).StaminaModel(
                        request.StaminaName
                    ).Model();
                    yield return future2;
                    if (future2.Error != null) {
                        this.onError.Invoke(new CanIgnoreException(future2.Error), null);
                    }

                    var staminaModel = future2.Result;

                    if (staminaModel == null) continue;

                    if (stamina.Value + request.RecoverValue > staminaModel.MaxCapacity) {
                        target.SetActive(this.denyReachMax);
                    }
                    else {
                        target.SetActive(this.allow);
                    }
                }
                else
                {
                    target.SetActive(false);
                }
            }
        }

        public void Start() {
            StartCoroutine(nameof(Process));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreAcquireActionStaminaStaminaEnabler
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _sessionHolder;
        private Gs2CoreAcquireActionsContext _context;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _sessionHolder = Gs2GameSessionHolder.Instance;
            _context = GetComponentInParent<Gs2CoreAcquireActionsContext>();
            
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreAcquireActionsContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreAcquireActionStaminaStaminaEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreAcquireActionStaminaStaminaEnabler
    {
        public bool allow;
        public bool denyReachMax;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreAcquireActionStaminaStaminaEnabler
    {

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }
}