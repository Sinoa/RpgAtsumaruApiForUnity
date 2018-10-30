// zlib/libpng License
//
// Copyright (c) 2018 Sinoa
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.

using UnityEngine;

namespace RpgAtsumaruApiForUnity
{
    /// <summary>
    /// RPGアツマールのAPIの中心となるクラスです。
    /// あらゆるAPIはこのクラスからアクセスする事になります。
    /// </summary>
    public static class RpgAtsumaruApi
    {
        // 定数定義
        private const string CallbackReceiverGameObjectName = "__RPGATSUMARU_CALLBACK_RECEIVER__";



        /// <summary>
        /// RPGアツマールAPIの初期化を行います。
        /// あらゆるAPIを呼び出す前に必ず一度だけ呼び出してください。
        /// </summary>
        public static void Initialize()
        {
            // 既に初期化済みなら
            if (RpgAtsumaruNativeApi.IsInitialized())
            {
                // 直ちに終了
                return;
            }


            // コールバックを受け取るためのゲームオブジェクトを生成して、トランスフォームの取得と必要コンポーネントのアタッチをする
            var gameObject = new GameObject(CallbackReceiverGameObjectName);
            var transform = gameObject.GetComponent<Transform>();
            gameObject.AddComponent<RpgAtsumaruApiCallbackReceiver>();


            // ゲームオブジェクトをヒエラルキから姿を消してシーン遷移による削除を受けないようにする
            gameObject.hideFlags = HideFlags.HideInHierarchy;
            Object.DontDestroyOnLoad(gameObject);


            // トランスフォームを初期化する
            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.eulerAngles = Vector3.zero;


            // ネイティブAPIの初期化パラメータを生成して初期化する
            var nativeApiInitializeParam = new RptAtsumaruNativeApiInitializeParameter()
            {
                // オブジェクト名や、コールバック名を設定していく
                UnityObjectName = CallbackReceiverGameObjectName,
                GetItemsCallback = nameof(RpgAtsumaruApiCallbackReceiver.OnStorageItemsReceived),
                SetItemsCallback = nameof(RpgAtsumaruApiCallbackReceiver.OnStorageSetItemCompleted),
                RemoveItemCallback = nameof(RpgAtsumaruApiCallbackReceiver.OnStorageRemoveItemCompleted),
                VolumeChangedCallback = nameof(RpgAtsumaruApiCallbackReceiver.OnVolumeChanged),
            };


            // 初期化パラメータのJSONデータ化してネイティブAPIの初期化をする
            var jsonData = JsonUtility.ToJson(nativeApiInitializeParam);
            RpgAtsumaruNativeApi.Initialize(jsonData);
        }



        /// <summary>
        /// RPGアツマールネイティブAPIからのコールバックを受け付けるレシーバコンポーネントクラスです
        /// </summary>
        private sealed class RpgAtsumaruApiCallbackReceiver : MonoBehaviour
        {
            /// <summary>
            /// RPGアツマールのサーバーストレージからすべてのデータをjsonで受け取ったときのイベントを処理します
            /// </summary>
            /// <param name="jsonData">受け取ったデータのjson文字列データ</param>
            public void OnStorageItemsReceived(string jsonData)
            {
            }


            /// <summary>
            /// RPGアツマールのサーバーストレージにデータを設定した完了イベントを処理します
            /// </summary>
            public void OnStorageSetItemCompleted()
            {
            }


            /// <summary>
            /// RPGアツマールのサーバーストレージからデータを削除した完了イベントを処理します
            /// </summary>
            public void OnStorageRemoveItemCompleted()
            {
            }


            /// <summary>
            /// RPGアツマールのマスター音量を変更したときの通知イベントを処理します
            /// </summary>
            /// <param name="volume">変更された音量（OFF 0.0 ～ 1.0 ON）</param>
            public void OnVolumeChanged(float volume)
            {
            }
        }
    }
}