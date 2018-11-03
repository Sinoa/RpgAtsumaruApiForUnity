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

#pragma warning disable 649

using System;
using System.Runtime.InteropServices;

namespace RpgAtsumaruApiForUnity
{
    /// <summary>
    /// RPGアツマールのネイティブAPIとの境界になる静的クラスです
    /// </summary>
    internal static class RpgAtsumaruNativeApi
    {
        /// <summary>
        /// ネイティブプラグインの初期化を行います。
        /// どんなAPIを使う前に必ず一度だけ呼び出してください。
        /// </summary>
        /// <param name="initializeParameterJson">ネイティブプラグインを初期化するパラメータを保持したjsonデータ</param>
        [DllImport("__Internal")]
        public static extern void Initialize(string initializeParameterJson);


        /// <summary>
        /// ネイティブプラグインが初期化済みかどうか
        /// </summary>
        /// <returns>ネイティブプラグインが初期化済みの場合は true を、未初期化の場合は false を返します</returns>
        [DllImport("__Internal")]
        public static extern bool IsInitialized();


        /// <summary>
        /// RPGアツマールのサーバーストレージに保存されたデータをすべて取得します
        /// </summary>
        [DllImport("__Internal")]
        public static extern void GetStorageItems();


        /// <summary>
        /// 指定されたJSONデータでRPGアツマールのサーバーストレージに設定します。
        /// </summary>
        /// <remarks>
        /// キー名はRPGアツマール側の仕様により、システムファイルは "system"、スロットファイルは "data{N}"、といったパターンである必要があります。
        /// もし、仕様と異なるキー名で渡した場合は "その他" として扱われます。
        /// </remarks>
        /// <param name="saveDataJson">{"SaveDataItems":[{"key":"key","value":"value"},,,,]}の構造を持ったJSONデータ</param>
        [DllImport("__Internal")]
        public static extern void SetStorageItems(string saveDataJson);


        /// <summary>
        /// 指定されたキー名のデータをRPGアツマールのサーバーストレージから削除します
        /// </summary>
        /// <param name="key">削除するデータのキー名</param>
        [DllImport("__Internal")]
        public static extern void RemoveStorageItem(string key);


        /// <summary>
        /// RPGアツマールに設定されている現在のマスターボリュームを取得します
        /// </summary>
        /// <returns>現在のマスターボリュームを （OFF ～ 最大）0.0 ～ 1.0 といった正規化した値で返します</returns>
        [DllImport("__Internal")]
        public static extern float GetCurrentVolume();


        /// <summary>
        /// RPGアツマールの音量調整バーの監視を開始します。
        /// 監視を開始すると自動的に音量調整バーが表示されます。
        /// </summary>
        [DllImport("__Internal")]
        public static extern void StartVolumeListen();


        /// <summary>
        /// RPGアツマールの音量調整バーの監視を停止します。
        /// 監視を停止しても一度表示された音量調整バーは非表示にはなりません。
        /// </summary>
        [DllImport("__Internal")]
        public static extern void StopVolumeListen();


        /// <summary>
        /// RPGアツマールのURLを開くポップアップを表示します。
        /// </summary>
        /// <param name="url">開いてほしいURL</param>
        [DllImport("__Internal")]
        public static extern void OpenLink(string url);


        /// <summary>
        /// ゲームURLのクエリに設定された値を取得します（RPGアツマールの仕様上クエリの変数名は param1～param9 になります）
        /// </summary>
        /// <param name="name">取得したいクエリ名</param>
        /// <returns>指示されたクエリ名の変数に設定された値を返します</returns>
        [DllImport("__Internal")]
        public static extern string GetQuery(string name);
    }



    /// <summary>
    /// ネイティブプラグインを初期化する為のパラメータを定義した構造体です
    /// </summary>
    [Serializable]
    internal struct RptAtsumaruNativeApiInitializeParameter
    {
        /// <summary>
        /// ネイティブプラグインからUnityへ通知される際、その通知を受けるゲームオブジェクトの名前
        /// </summary>
        public string UnityObjectName;


        /// <summary>
        /// Storage API の getItems 関数を呼び出した時の完了通知を受けるコールバック関数名（引数には取得したJSONデータを受け取る文字列型が必要です）
        /// </summary>
        public string GetItemsCallback;


        /// <summary>
        /// Storage API の setItems 関数を呼び出した時の完了通知を受けるコールバック関数名
        /// </summary>
        public string SetItemsCallback;


        /// <summary>
        /// Storage API の removeItem 関数を呼び出した時の完了通知を受けるコールバック関数名
        /// </summary>
        public string RemoveItemCallback;


        /// <summary>
        /// Volume API の changed.subscrive 関数に登録したオブザーバの値を受けった通知を受けるコールバック関数名（引数には音量を受け取るfloat型が必要です）
        /// </summary>
        public string VolumeChangedCallback;


        /// <summary>
        /// OpenLink API の openLink 関数を呼び出した時の完了通知を受けるコールバック関数名（引数にはエラーが発生したかどうかを含むJSONデータを受ける文字列型が必要です）
        /// </summary>
        public string OpenLinkCallback;
    }



    /// <summary>
    /// RPGアツマールの共通エラーオブジェクトの構造を定義した構造体です
    /// </summary>
    [Serializable]
    internal struct RpgAtsumaruApiError
    {
        /// <summary>
        /// APIの呼び出し方に問題が発生しています。ネイティブプラグインの実装に問題が無いか確認してください。
        /// </summary>
        public const string ErrorCodeBadRequest = "BAD_REQUEST";

        /// <summary>
        /// ユーザーのログイン情報が必要なAPIを、非ログイン状態で呼び出しています。
        /// </summary>
        public const string ErrorCodeUnauthorized = "UNAUTHORIZED";

        /// <summary>
        /// RPGアツマールサーバーに問題が発生しました。しばらく時間を置いてから再度実行を試みてください。
        /// </summary>
        public const string ErrorCodeInternalServerError = "INTERNAL_SERVER_ERROR";



        /// <summary>
        /// APIのエラータイプ
        /// </summary>
        public string errorType;


        /// <summary>
        /// エラーコード
        /// </summary>
        public string code;


        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string message;
    }



    /// <summary>
    /// RPGアツマールサーバーストレージAPIから返却される全体構造を保持する構造体です
    /// </summary>
    [Serializable]
    internal struct RpgAtsumaruSaveData
    {
        /// <summary>
        /// 各データスロット毎のレコード情報
        /// </summary>
        public RpgAtsumaruDataRecord[] SaveDataItems;
    }



    /// <summary>
    /// RPGアツマールサーバーストレージが扱うデータスロットの構造を保持する構造体です（KeyValueではなくkeyvalueと小文字）
    /// </summary>
    [Serializable]
    internal struct RpgAtsumaruDataRecord
    {
        /// <summary>
        /// データスロットのキー名
        /// </summary>
        public string key;


        /// <summary>
        /// データスロットのデータ
        /// </summary>
        public string value;
    }



    /// <summary>
    /// RPGアツマールのopneLink関数を実行した際の結果を表す構造体です
    /// </summary>
    [Serializable]
    internal struct RpgAtsumaruOpenLinkResult
    {
        /// <summary>
        /// エラーが発生したかどうか
        /// </summary>
        public bool ErrorOccured;


        /// <summary>
        /// エラーが発生した場合のエラー情報
        /// </summary>
        public RpgAtsumaruApiError Error;
    }
}