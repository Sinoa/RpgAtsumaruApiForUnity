サーバーセーブをしよう {#serversave}
===

# 解説

RPGアツマールのサーバーセーブ機能を使い、ユーザーセーブデータを保存したり取得したりします。  
また、このAPIでは、システムデータと各セーブスロットを正しくハンドリングするように、内部で制御されています。  
つまり、ゲームロジック側では、システムデータ用の制御関数と、セーブデータスロットの制御関数を操作するだけで良いように設計されています。  
  
ただし、単純にセーブデータの取得と設定だけでは、RPGアツマールサーバと同期されないため  
同期するための関数を操作する必要があります。  
詳しくは、以下のサンプルを参照して下さい。

# サンプルコード

~~~{.cs}
using RpgAtsumaruApiForUnity;
using UnityEngine;

public class RpgAtsumaruSample : MonoBehaviour
{
    // SyncSaveDataAsync() 関数を待機するためには、関数に async 定義をしなければなりません
    private async void Awake()
    {
        // もしプラグインの初期化が終わっていないなら
        if (!RpgAtsumaruApi.Initialized)
        {
            // プラグインの初期化
            RpgAtsumaruApi.Initialize();
        }


        // ストレージAPIを取得して、初回同期をする
        // 同期を必ず最初に行い、サーバーからデータを貰うようにして下さい
        await RpgAtsumaruApi.StorageApi.SyncSaveDataAsync();
    }


    // ゲームのシステムデータを取得する
    public string GetSystemData()
    {
        // システムデータをゲームロジックに返す
        return RpgAtsumaruApi.StorageApi.GetSystemData();
    }


    // ゲームのシステムデータをセーブ
    public async void SaveSystemData(string systemData)
    {
        // システムデータを設定してサーバーと同期する
        RpgAtsumaruApi.StorageApi.SetSystemData(systemData);
        await RpgAtsumaruApi.StorageApi.SyncSaveDataAsync();
    }


    // 指定されたスロット番号のセーブデータを取得する
    public string GetGameSaveData(int slotNumber)
    {
        // セーブデータを返す
        return RpgAtsumaruApi.StorageApi.GetSaveData(slotNumber);
    }


    // 指定されたスロット番号にセーブデータをセーブする
    public async void SaveGameData(int slotNumber, string saveData)
    {
        // セーブデータを設定してサーバーと同期する
        RpgAtsumaruApi.StorageApi.SetSaveData(slotNumber, saveData);
        await RpgAtsumaruApi.StorageApi.SyncSaveDataAsync();
    }


    // セーブデータが存在するスロット番号を全て取得する
    public int[] GetAvailableSaveDataSlotNumbers()
    {
        // セーブデータの有効な番号を全て返す
        return RpgAtsumaruApi.StorageApi.GetAllSaveDataSlotId();
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.StorageApi | ストレージAPIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruStorage.SyncSaveDataAsync() | RPGアツマールサーバーと同期する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruStorage.GetSystemData() | 同期されたシステムデータを取得する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruStorage.SetSystemData() | ストレージAPIにシステムデータを設定する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruStorage.GetSaveData(int) | 同期されたゲームセーブデータを取得する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruStorage.SetSaveData(int, string) | ストレージAPIにゲームセーブデータを設定する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruStorage.GetAllSaveDataSlotId() | セーブされているスロットIDを全て取得する関数 |
