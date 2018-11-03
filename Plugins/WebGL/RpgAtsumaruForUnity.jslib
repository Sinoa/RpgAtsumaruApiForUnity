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

// (確認UnityVersion 2018.3.0b7)
// Unity用RPGアツマールオブジェクトを宣言
var RpgAtsumaruApiForUnity =
{
    // プラグインの状態を保持するコンテキストテーブルです
    $Context:
    {
        initialized: false,       // プラグインが初期化されたかどうか
        unityObjectName: "",      // 非同期操作時にUnityへ通知するためのゲームオブジェクト名
        volumeSubscription: null, // 音量APIのサブスクリプション参照
        unityMethodNames:         // 非同期的にUnityへ通知するための各種メソッド名
        {
            // Error
            errorHandler: "", // APIエラーハンドラ名

            // Storage API
            getItems: "",   // getItems APIの通知名
            setItems: "",   // setItems APIの通知名
            removeItem: "", // removeItem APIの通知名

            // Volume API
            volumeChanged: "", // changed APIの通知名

            // OpenLink API
            openLink: "", // openLink APIの通知名
        },
    },


    // 各APIの共通エラーをUnityへ送信します
    // error : 発生したエラーの内容（APIのエラーオブジェクトをそのまま渡してください）
    $SendError: function(error)
    {
        // エラー内容をJSON化して送る
        var jsonData = JSON.stringify(error);
        SendMessage(Context.unityObjectName, Context.unityMethodNames.errorHandler, jsonData);
    },


    // プラグインが初期化済みかどうか
    // 戻り値 : 初期化済みなら true を、未初期化なら false を返します
    IsInitialized: function()
    {
        // 初期化されているかどうかの情報をそのまま返す
        return Context.initialized;
    },


    // プラグインの初期化を行います
    // initializeParameterJson : 初期化するためのパラメータをJSON化した値
    Initialize: function(initializeParameterJson)
    {
        // 既に初期化済みなら
        if (Context.initialized)
        {
            // 直ちに終了
            return;
        }


        // 受け取ったjsonデータをパースする
        var jsonData = Pointer_stringify(initializeParameterJson);
        var initParam = JSON.parse(jsonData);


        // コンテキストを初期化していく
        Context.unityObjectName = initParam.UnityObjectName;
        Context.unityMethodNames.errorHandler = initParam.ErrorHandler;
        Context.unityMethodNames.getItems = initParam.GetItemsCallback;
        Context.unityMethodNames.setItems = initParam.SetItemsCallback;
        Context.unityMethodNames.removeItem = initParam.RemoveItemCallback;
        Context.unityMethodNames.volumeChanged = initParam.VolumeChangedCallback;
        Context.unityMethodNames.openLink = initParam.OpenLinkCallback;


        // 初期化済みをマーク
        Context.initialized = true;
    },


    // RPGアツマールサーバーストレージからデータを取得します
    GetStorageItems: function()
    {
        // 未初期化なら
        if (!Context.initialized)
        {
            // 直ちに終了する（応答するにも応答先を知らない）
            return;
        }


        // RPGアツマールサーバーストレージからデータを拾ってくる
        window.RPGAtsumaru.storage.getItems()
            .then(function(saveData)
            {
                // 無名の配列はUnityのJsonUtilityで処理できないのでSaveDataItemsという名前を付加してJSONデータを生成
                var finalSaveData = {SaveDataItems:saveData};
                var jsonData = JSON.stringify(finalSaveData);


                // Unityに生成したJsonデータを送る
                SendMessage(Context.unityObjectName, Context.unityMethodNames.getItems, jsonData);
            });
    },


    // RPGアツマールサーバーストレージにデータを設定します。
    // 設定するデータのキーは、仕様上 "system", "data{N}" といった予約キー名を使うことを推奨します。
    // saveDataJson : {"SaveDataItems":[{"key":"key","value":"value"},,,,]}の構造を持ったJSONデータ
    SetStorageItems: function(saveDataJson)
    {
        // 未初期化なら
        if (!Context.initialized)
        {
            // 直ちに終了する（応答するにも応答先を知らない）
            return;
        }


        // C#文字列のポインタからJS文字列として取得してJSONパースをする
        var saveData = JSON.parse(Pointer_stringify(saveDataJson));


        // セーブデータをRPGアツマールサーバーへ設定する
        window.RPGAtsumaru.storage.setItems(saveData.SaveDataItems)
            .then(function()
            {
                // 完了したことを通知する
                SendMessage(Context.unityObjectName, Context.unityMethodNames.setItems);
            });
    },


    // RPGアツマールサーバーストレージから指定されたキーのデータを削除します
    // key : 削除するデータのキー（仕様上 "system", "data{N}" といった予約キー名を使うことを推奨します）
    RemoveStorageItem: function(key)
    {
        // 未初期化なら
        if (!Context.initialized)
        {
            // 直ちに終了する（応答するにも応答先を知らない）
            return;
        }


        // C#文字列のポインタからJS文字列として取得
        var jsKey = Pointer_stringify(key);


        // RPGアツマールのストレージアイテム削除APIを叩く
        window.RPGAtsumaru.storage.removeItem(jsKey)
            .then(function()
            {
                // 完了したことを通知する
                SendMessage(Context.unityObjectName, Context.unityMethodNames.removeItem);
            });
    },


    // RPGアツマール側で設定されている、現在のマスターボリュームを0.0～1.0の正規化された値を取得します
    // 戻り値 : 正規化された現在のマスターボリュームを返します
    GetCurrentVolume: function()
    {
        // この関数は素直にバイパスするだけで、現在のボリューム値をそのまま返す
        return window.RPGAtsumaru.volume.getCurrentValue();
    },


    // RPGアツマールの音量変化通知を受け取るリスンを開始します
    StartVolumeListen: function()
    {
        // 未初期化 または 既に購読している なら
        if (!Context.initialized || Context.volumeSubscription != null)
        {
            // 直ちに終了する（応答するにも応答先を知らない）
            return;
        }


        // 音量変化を購読する
        Context.volumeSubscription = window.RPGAtsumaru.volume.changed.subscribe({
            next: function(volume)
            {
                // Unityに変化した音量を通知する
                SendMessage(Context.unityObjectName, Context.unityMethodNames.volumeChanged, volume);
            },
            error: function(err) {},
            complete: function() {},
        });
    },


    // RPGアツマールの音量変化通知を受け取るリスンを停止します
    StopVolumeListen: function()
    {
        // 未初期化 または 購読していない なら
        if (!Context.initialized || Context.volumeSubscription == null)
        {
            // 直ちに終了する（応答するにも応答先を知らない）
            return;
        }


        // 購読を停止する
        Context.volumeSubscription.unsubscribe();
        Context.volumeSubscription = null;
    },


    // RPGアツマール側で外部URLを開くポップアップを表示します
    // url : 開きたい外部URL
    OpenLink: function(url)
    {
        // openLink APIを叩く
        window.RPGAtsumaru.popups.openLink(url)
            .then(function()
            {
                // UnityにopenUrlを操作した通知をする
                SendMessage(Context.unityObjectName, Context.unityMethodNames.openLink);
            })
            .catch(function(error)
            {
                // 発生したエラーを送信する
                SendError(error);
            });
    },
};


// 内部依存定義の追加
autoAddDeps(RpgAtsumaruApiForUnity, "$Context");
autoAddDeps(RpgAtsumaruApiForUnity, "$SendError");


// Unityライブラリにライブラリオブジェクトを登録
mergeInto(LibraryManager.library, RpgAtsumaruApiForUnity);