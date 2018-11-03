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

using System.Threading.Tasks;
using IceMilkTea.Core;
using UnityEngine;

namespace RpgAtsumaruApiForUnity
{
    /// <summary>
    /// RPGアツマールの細かいAPIや汎用的なAPIをまとめたクラスになります
    /// </summary>
    public class RpgAtsumaruGeneral
    {
        // メンバ変数定義
        private ImtAwaitableManualReset<string> openLinkAwaitable;



        /// <summary>
        /// RpgAtsumaruGeneral のインスタンスを初期化します
        /// </summary>
        /// <param name="receiver">RPGアツマールネイティブAPIコールバックを拾うレシーバ</param>
        internal RpgAtsumaruGeneral(RpgAtsumaruApi.RpgAtsumaruApiCallbackReceiver receiver)
        {
            // レシーバにイベントを登録する
            receiver.OpenLinkCompleted += OnOpenLinkCompleted;


            // マニュアルリセット待機可能オブジェクトをシグナル状態で生成する
            openLinkAwaitable = new ImtAwaitableManualReset<string>(true);
        }


        /// <summary>
        /// RPGアツマールのURLポップアップ表示をした完了イベントを処理します
        /// </summary>
        /// <param name="result">openLink関数の実行結果を含んだjsonデータ</param>
        public void OnOpenLinkCompleted(string result)
        {
            // 待機オブジェクトに送られてきたjsonデータ付きでシグナルを設定する
            openLinkAwaitable.Set(result);
        }


        /// <summary>
        /// RPGアツマールのURLを開くポップアップを非同期で表示します
        /// </summary>
        /// <param name="url">URLを開くポップアップに表示するURL</param>
        /// <returns>URLを開くポップアップを開く操作をしているタスクを返します</returns>
        public async Task<(bool isError, string message)> OpenLink(string url)
        {
            // もし、シグナル状態なら
            if (openLinkAwaitable.IsCompleted)
            {
                // 非シグナル状態にしてopenLinkネイティブプラグイン関数を叩く
                openLinkAwaitable.Reset();
                RpgAtsumaruNativeApi.OpenLink(url);
            }


            // シグナル状態になるまで待って結果を受け取る
            var jsonData = await openLinkAwaitable;
            var result = JsonUtility.FromJson<RpgAtsumaruOpenLinkResult>(jsonData);


            // 結果を返す
            return (result.ErrorOccured, result.Error.message);
        }
    }
}