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

#pragma warning disable 618

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
        private ImtAwaitableManualReset<string> creatorInfoShowAwaitable;
        private ImtAwaitableManualReset<string> screenshotAwaitable;



        /// <summary>
        /// RpgAtsumaruGeneral のインスタンスを初期化します
        /// </summary>
        /// <param name="receiver">RPGアツマールネイティブAPIコールバックを拾うレシーバ</param>
        internal RpgAtsumaruGeneral(RpgAtsumaruApi.RpgAtsumaruApiCallbackReceiver receiver)
        {
            // レシーバにイベントを登録する
            receiver.OpenLinkCompleted += OnOpenLinkCompleted;
            receiver.CreatorInfoShown += OnCreatorInfoShown;


            // マニュアルリセット待機可能オブジェクトをシグナル状態で生成する
            openLinkAwaitable = new ImtAwaitableManualReset<string>(true);
            creatorInfoShowAwaitable = new ImtAwaitableManualReset<string>(true);
            screenshotAwaitable = new ImtAwaitableManualReset<string>(true);
        }


        /// <summary>
        /// RPGアツマールのURLポップアップ表示をした完了イベントを処理します
        /// </summary>
        /// <param name="result">openLink関数の実行結果を含んだjsonデータ</param>
        private void OnOpenLinkCompleted(string result)
        {
            // 待機オブジェクトに送られてきたjsonデータ付きでシグナルを設定する
            openLinkAwaitable.Set(result);
        }


        /// <summary>
        /// RPGアツマールの作者情報ダイアログを表示した完了イベントを処理します
        /// </summary>
        /// <param name="result">displayCreatorInformationModal関数の実行結果を含んだjsonデータ</param>
        private void OnCreatorInfoShown(string result)
        {
            // 待機オブジェクトに送られてきたjsonデータ付きでシグナルを設定する
            creatorInfoShowAwaitable.Set(result);
        }


        /// <summary>
        /// RPGアツマールのスクリーンショットとそのダイアログ表示の完了イベントを処理します
        /// </summary>
        /// <param name="result">screenshot.displayModal関数の実行結果を含んだjsonデータ</param>
        private void OnScreenshotCompleted(string result)
        {
            // 待機オブジェクトに送られてきたjsonデータ付きでシグナルを設定する
            screenshotAwaitable.Set(result);
        }


        /// <summary>
        /// ゲームURLのクエリに設定された値を取得します（RPGアツマールの仕様上クエリの変数名は param1～param9 になります）
        /// </summary>
        /// <param name="name">取得したいクエリ名</param>
        /// <returns>指示されたクエリ名の変数に設定された値を返します</returns>
        public virtual string GetQuery(string name)
        {
            // ネイティブプラグイン関数の結果をそのまま伝える
            return RpgAtsumaruNativeApi.GetQuery(name);
        }


        /// <summary>
        /// RPGアツマールのURLを開くポップアップを非同期で表示します
        /// </summary>
        /// <param name="url">URLを開くポップアップに表示するURL</param>
        /// <returns>URLを開くポップアップを開く操作をしているタスクを返します</returns>
        public virtual async Task<(bool isError, string message)> OpenLinkAsync(string url)
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
            var result = JsonUtility.FromJson<RpgAtsumaruBasicResult>(jsonData);


            // 結果を返す
            return (result.ErrorOccured, result.Error.message);
        }


        /// <summary>
        /// 指定されたニコニコユーザーIDの作者情報ダイアログを非同期で表示します
        /// </summary>
        /// <param name="niconicoUserId">表示するニコニコユーザーID</param>
        /// <returns>作者情報ダイアログを開く操作をしているタスクを返します</returns>
        public virtual async Task<(bool isError, string message)> ShowCreatorInformationAsync(int niconicoUserId)
        {
            // もし、シグナル状態なら
            if (creatorInfoShowAwaitable.IsCompleted)
            {
                // 非シグナル状態にしてShowCreatorInformationネイティブプラグイン関数を叩く
                creatorInfoShowAwaitable.Reset();
                RpgAtsumaruNativeApi.ShowCreatorInformation(niconicoUserId);
            }


            // シグナル状態になるまで待って結果を受け取る
            var jsonData = await creatorInfoShowAwaitable;
            var result = JsonUtility.FromJson<RpgAtsumaruBasicResult>(jsonData);


            // 結果を返す
            return (result.ErrorOccured, result.Error.message);
        }


        /// <summary>
        /// スクリーンショットを撮った後にTwitter投稿ダイアログを非同期で操作します
        /// </summary>
        /// <returns>スクリーンショットとTwitter投稿ダイアログを操作しているタスクを返します</returns>
        [System.Obsolete("この関数は、現在RPGツクールMV専用の機能となっています")]
        public virtual async Task<(bool isError, string message)> ScreenshotAsync()
        {
            // もし、シグナル状態なら
            if (screenshotAwaitable.IsCompleted)
            {
                // 非シグナル状態にしてScreenshotネイティブプラグイン関数を叩く
                screenshotAwaitable.Reset();
                RpgAtsumaruNativeApi.Screenshot();
            }


            // シグナル状態になるまで待って結果を受け取る
            var jsonData = await screenshotAwaitable;
            var result = JsonUtility.FromJson<RpgAtsumaruBasicResult>(jsonData);


            // 結果を返す
            return (result.ErrorOccured, result.Error.message);
        }
    }
}