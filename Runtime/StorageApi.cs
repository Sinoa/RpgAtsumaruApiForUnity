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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RpgAtsumaruApiForUnity
{
    /// <summary>
    /// RPGアツマールのサーバーストレージを使ったセーブデータの管理を行うクラスです
    /// </summary>
    public class RpgAtsumaruStorage : IDisposable
    {
        #region WebAPI Import
        /// <summary>
        /// RPGアツマールサーバーストレージからデータをすべて取得します
        /// </summary>
        /// <param name="objectName">取得結果の通知を受けるゲームオブジェクト名</param>
        /// <param name="methodName">objectName に送信される公開関数名（この関数には、Json化されたセーブデータを受け取る1つのstring引数が必要です）</param>
        [DllImport("__Internal")]
        extern private static void GetStorageItems(string objectName, string methodName);

        /// <summary>
        /// RPGアツマールサーバーストレージへデータを設定します。
        /// 設定するデータのキー名には、RPGアツマールの推奨名（"system", "data{N}"）があります。
        /// </summary>
        /// <param name="key">設定するデータのキー名（RPGアツマール推奨の名前を設定することが望ましいです）</param>
        /// <param name="value">文字列化された設定するデータ（1ブロック = 約1KB相当）</param>
        /// <param name="objectName">セーブ完了通知を受けるゲームオブジェクト名</param>
        /// <param name="methodName">objectName に送信される公開関数名（この関数は、引数を受け取りません）</param>
        [DllImport("__Internal")]
        extern private static void SetStorageItem(string key, string value, string objectName, string methodName);

        /// <summary>
        /// RPGアツマールサーバーストレージから指定されたキー名のデータを削除します
        /// </summary>
        /// <param name="key">削除するデータのキー名</param>
        /// <param name="objectName">削除された通知を受けるゲームオブジェクト名</param>
        /// <param name="methodName">objectName に送信される公開関数名（この関数は、引数を受け取りません）</param>
        [DllImport("__Internal")]
        extern private static void RemoveStorageItem(string key, string objectName, string methodName);
        #endregion



        /// <summary>
        /// セーブデータの受信が終わった時のイベントです
        /// </summary>
        public event Action SaveDataReceived;


        /// <summary>
        /// セーブデータのセーブが完了した時のイベントです
        /// </summary>
        public event Action SaveCompleted;


        /// <summary>
        /// セーブデータの削除が完了したときのイベントです
        /// </summary>
        public event Action RemoveCompleted;



        // 定数定義
        private const string SystemSaveDataKeyName = "system";
        private const string PrefixGameDataKeyName = "data";

        // メンバ変数定義
        private RpgAtsumaruStorageApiHandler handler;
        private Dictionary<string, string> saveDataTable;
        private bool disposed;



        #region コンストラクタ＆ファイナライザ＆Dispose
        /// <summary>
        /// RpgAtsumaruStorage のインスタンスを初期化します
        /// </summary>
        /// <param name="gameObjectName">RpgAtsumaruStorage インスタンスがAPI制御に用いるゲームオブジェクト名</param>
        /// <exception cref="ArgumentNullException">gameObjectName が null です</exception>
        public RpgAtsumaruStorage(string gameObjectName)
        {
            // 既定のゲームオブジェクトが存在するか確認するが見つけられなかったら
            var gameObject = GameObject.Find(gameObjectName ?? throw new ArgumentNullException(nameof(gameObjectName)));
            if (gameObject == null)
            {
                // 生成をして初期化をする
                gameObject = new GameObject(gameObjectName);
                gameObject.hideFlags = HideFlags.HideInHierarchy;
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
            }


            // APIハンドラコンポーネントの取得をして自身の登録をする
            handler = gameObject.GetComponent<RpgAtsumaruStorageApiHandler>() ?? gameObject.AddComponent<RpgAtsumaruStorageApiHandler>();
            handler.AddOwner(this);


            // セーブデータテーブルを生成
            saveDataTable = new Dictionary<string, string>();
        }


        /// <summary>
        /// RpgAtsumaruStorage のインスタンスを破棄します
        /// </summary>
        ~RpgAtsumaruStorage()
        {
            // ファイナライザからのDispose呼び出し
            Dispose(false);
        }


        /// <summary>
        /// リソースの破棄を行います
        /// </summary>
        public void Dispose()
        {
            // Disposeからの呼び出しとして呼んで、ファイナライザを抑制
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// 実際のリソースの破棄を行います
        /// </summary>
        /// <param name="disposing">Disposeからの呼び出しの場合は true を、それ以外の場合は false を指定</param>
        protected virtual void Dispose(bool disposing)
        {
            // 既に解放済みなら
            if (disposed)
            {
                // 何もしない
                return;
            }


            // マネージ解放なら
            if (disposing)
            {
                // ハンドラから自身を捨ててもらう
                handler.RemoveOwner(this);
            }


            // 解放済みマーク
            disposed = true;
        }
        #endregion


        #region WebAPIイベントハンドラ
        /// <summary>
        /// RPGアツマールサーバーストレージからセーブデータを受信したときのハンドリングを行います
        /// </summary>
        /// <param name="saveData">受信したセーブデータの参照</param>
        private void OnSaveDataReceived(in SaveData saveData)
        {
            // 現在のセーブデータテーブルをクリアする
            saveDataTable.Clear();


            // セーブデータのレコード分回る
            foreach (var record in saveData.SaveDataItems)
            {
                // keyとvalueをそのままぶっこむ
                saveDataTable[record.key] = record.value;
            }


            // セーブデータの受信完了イベントを呼ぶ
            SaveDataReceived?.Invoke();
        }


        /// <summary>
        /// RPGアツマールサーバーストレージからセーブ完了を受信したときのハンドリングを行います
        /// </summary>
        private void OnSaveCompleted()
        {
            // イベントを呼ぶだけ
            SaveCompleted?.Invoke();
        }


        /// <summary>
        /// RPGアツマールサーバーストレージから削除完了を受信したときのハンドリングを行います
        /// </summary>
        private void OnRemoveCompleted()
        {
            // イベントを呼ぶだけ
            RemoveCompleted?.Invoke();
        }
        #endregion


        #region APIインターフェイス
        /// <summary>
        /// RPGアツマールサーバーストレージからすべてのセーブデータをロードします。
        /// また、一度ロードを終えた場合はインスタンスが解放されない限り、セーブデータはメモリに残り続けるため
        /// 何度もロードをする必要はありません。
        /// </summary>
        /// <exception cref="ObjectDisposedException">このオブジェクトは既に解放済みです</exception>
        public void InitializeSaveData()
        {
            // 例外通知処理をする
            ThrowIfDisposed();


            // ハンドラの全データ取得関数を叩く
            handler.GetStorageItems();
        }


        /// <summary>
        /// システムデータをロードします
        /// </summary>
        /// <returns>ロードされたシステムデータを返しますが、ロードができなかった場合は null を返します</returns>
        /// <exception cref="ObjectDisposedException">このオブジェクトは既に解放済みです</exception>
        public string LoadSystemData()
        {
            // 例外通知処理をする
            ThrowIfDisposed();


            // セーブデータテーブルからシステムデータを取り出せるのなら
            if (saveDataTable.TryGetValue(SystemSaveDataKeyName, out var gameData))
            {
                // ゲームデータを返す
                return gameData;
            }


            // 取り出せなかったのならnullを返す
            return null;
        }


        /// <summary>
        /// システムデータをセーブします。
        /// この関数は、メモリ上に保存すると共にRPGアツマールストレージへセーブも行います
        /// </summary>
        /// <param name="systemData">セーブするシステムデータ</param>
        /// <exception cref="ObjectDisposedException">このオブジェクトは既に解放済みです</exception>
        public void SaveSystemData(string systemData)
        {
            // 例外通知処理をする
            ThrowIfDisposed();
            ThrowIfNullData(systemData, nameof(systemData));


            // システムデータを反映しつつアツマールへ保存する
            saveDataTable[SystemSaveDataKeyName] = systemData;
            handler.SetStorageItem(SystemSaveDataKeyName, systemData);
        }


        /// <summary>
        /// 指定されたスロット番号からゲームデータをロードします
        /// </summary>
        /// <param name="slotId">ロードするセーブデータのスロット番号（0～）</param>
        /// <returns>ロードされたゲームデータを返しますが、ロードができなかった場合は null を返します</returns>
        /// <exception cref="ObjectDisposedException">このオブジェクトは既に解放済みです</exception>
        /// <exception cref="ArgumentOutOfRangeException">セーブデータのスロット番号に負の値は指定できません</exception>
        public string LoadGameData(int slotId)
        {
            // 例外通知処理をする
            ThrowIfDisposed();
            ThrowIfNegative(slotId);


            var keyName = $"{PrefixGameDataKeyName}{slotId}";
            if (saveDataTable.TryGetValue(keyName, out var gameData))
            {
                return gameData;
            }


            return null;
        }


        public void SaveGameData(int slotId, string gameData)
        {
            // 例外通知処理をする
            ThrowIfDisposed();
            ThrowIfNegative(slotId);
            ThrowIfNullData(gameData, nameof(gameData));


            var keyName = $"{PrefixGameDataKeyName}{slotId}";
            saveDataTable[keyName] = gameData;
            handler.SetStorageItem(keyName, gameData);
        }
        #endregion


        #region 例外通知
        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }


        private void ThrowIfNegative(int slotId)
        {
            if (slotId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slotId), "セーブデータのスロット番号に負の値は指定できません");
            }
        }


        private void ThrowIfNullData(string gameData, string paramName)
        {
            if (gameData == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
        #endregion



        #region SaveData関連構造体定義
        /// <summary>
        /// RPGアツマールサーバーストレージAPIから返却される全体構造を保持する構造体です
        /// </summary>
        [Serializable]
        private struct SaveData
        {
            /// <summary>
            /// 各データスロット毎のレコード情報
            /// </summary>
            public DataRecord[] SaveDataItems;
        }



        /// <summary>
        /// RPGアツマールサーバーストレージが扱うデータスロットの構造を保持する構造体です（KeyValueではなくkeyvalueと小文字）
        /// </summary>
        [Serializable]
        private struct DataRecord
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
        #endregion



        #region イベントハンドラコンポーネントクラス
        /// <summary>
        /// StorageAPI通知のハンドリングを行うコンポーネントクラスです
        /// </summary>
        private sealed class RpgAtsumaruStorageApiHandler : MonoBehaviour
        {
            // メンバ変数定義
            private List<RpgAtsumaruStorage> ownerList;



            /// <summary>
            /// コンポーネントの初期化を行います
            /// </summary>
            private void Awake()
            {
                // オーナーリストの生成
                ownerList = new List<RpgAtsumaruStorage>();
            }


            /// <summary>
            /// 指定されたストレージを管理リストへ追加します。
            /// 追加されたストレージにイベントの通知が行われます。
            /// </summary>
            /// <param name="owner">追加するストレージ</param>
            /// <exception cref="ArgumentNullException">owner が null です</exception>
            public void AddOwner(RpgAtsumaruStorage owner)
            {
                // まだ追加されていないなら
                if (!ownerList.Contains(owner ?? throw new ArgumentNullException(nameof(owner))))
                {
                    // 追加をする
                    ownerList.Add(owner);
                }
            }


            /// <summary>
            /// 指定されたストレージを管理リストから削除します。
            /// </summary>
            /// <param name="owner">削除するストレージ</param>
            /// <exception cref="ArgumentNullException">owner が null です</exception>
            public void RemoveOwner(RpgAtsumaruStorage owner)
            {
                // 存在するなら
                if (ownerList.Contains(owner ?? throw new ArgumentNullException(nameof(owner))))
                {
                    // 削除する
                    ownerList.Remove(owner);
                }
            }


            /// <summary>
            /// RPGアツマールストレージAPIである GetStorageItems を呼び出します
            /// </summary>
            public void GetStorageItems()
            {
                // GetStorageitems APIを叩く
                RpgAtsumaruStorage.GetStorageItems(gameObject.name, nameof(OnDataReceived));
            }


            /// <summary>
            /// RPGアツマールストレージAPIである SetStorageItem を呼び出します
            /// </summary>
            /// <param name="key">設定するキー名</param>
            /// <param name="value">設定するデータ</param>
            public void SetStorageItem(string key, string value)
            {
                // SetStorageITem APIを叩く
                RpgAtsumaruStorage.SetStorageItem(key, value, gameObject.name, nameof(OnSaveCompleted));
            }


            /// <summary>
            /// RPGアツマールストレージAPIである RemoveStorageItem を呼び出します
            /// </summary>
            /// <param name="key">削除するデータのキー名</param>
            public void RemoveStorageItem(string key)
            {
                // RemoveStorageItem APIを叩く
                RpgAtsumaruStorage.RemoveStorageItem(key, gameObject.name, nameof(OnRemoveCompleted));
            }


            /// <summary>
            /// RPGアツマールサーバーストレージからデータの受信ハンドリングを行います
            /// </summary>
            /// <param name="jsonData">受信したJsonデータ</param>
            public void OnDataReceived(string jsonData)
            {
                // Jsonデータからセーブデータへデシリアライズする
                var saveData = JsonUtility.FromJson<SaveData>(jsonData);


                // オーナーの数分ループ
                foreach (var owner in ownerList)
                {
                    // セーブデータの受信イベントを通知する
                    owner.OnSaveDataReceived(in saveData);
                }
            }


            /// <summary>
            /// RPGアツマールサーバーストレージからセーブ完了ハンドリングを行います
            /// </summary>
            public void OnSaveCompleted()
            {
                // オーナーの数分ループ
                foreach (var owner in ownerList)
                {
                    // セーブ完了イベントを通知する
                    owner.OnSaveCompleted();
                }
            }


            /// <summary>
            /// RPGアツマールサーバーストレージからセーブ削除完了ハンドリングを行います
            /// </summary>
            public void OnRemoveCompleted()
            {
                // オーナーの数分ループ
                foreach (var owner in ownerList)
                {
                    // 削除完了イベントを通知する
                    owner.OnRemoveCompleted();
                }
            }
        }
        #endregion
    }
}