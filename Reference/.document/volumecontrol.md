音量を制御しよう {#volumecontrol}
===

# 解説

RPGアツマールには、音量調整バーによるマスター音量を調整する機能があります。  
しかし、Unityのマスター音量を直接制御する機能では無いため、RPGアツマールの音量調整バーを  
監視して、値に応じた音量調整をしなければなりません。  
しかし、"RPGアツマールAPI for Unity"にはその自動調整を行う機能があります。  
サンプルコードは以下の通りです。

# サンプルコード

~~~{.cs}
using RpgAtsumaruApiForUnity;
using UnityEngine;

public class RpgAtsumaruSample : MonoBehaviour
{
    private void Awake()
    {
        // もしプラグインの初期化が終わっていないなら
        if (!RpgAtsumaruApi.Initialized)
        {
            // プラグインの初期化
            RpgAtsumaruApi.Initialize();


            // 音量APIを取得して音量バーの監視を開始する
            RpgAtsumaruApi.VolumeApi.StartVolumeChangeListen();


            // RPGアツマールの音量調整バーが調整されたときに自動でUnityのマスター音量も調整する
            RpgAtsumaruApi.VolumeApi.EnableAutoVolumeSync = true;
        }
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.VolumeApi | ボリュームAPIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruVolume.StartVolumeChangeListen() | RPGアツマールの音量調整バーを監視の開始をする関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruVolume.EnableAutoVolumeSync | 音量調整バーに合わせて自動的にUnityのマスター音量を調整するプロパティ |
