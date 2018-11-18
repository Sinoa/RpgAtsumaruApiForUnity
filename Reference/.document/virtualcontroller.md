コントローラの入力を受け取ろう {#virtualcontroller}
===

# 解説

RPGアツマールには、PCタブレットなどでもゲーム操作ができるように仮想コントローラが実装されています。  
しかし、Unityでは直接使うことは出来ないため、プラグインから取得する必要があります。  
また、API構造はUnityのInputクラスに合わせているため、ほぼ違和感なく利用することが可能です。  
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


            // RPGアツマールの仮想コントローラの監視を開始する
            RpgAtsumaruApi.ControllerApi.StartControllerListen();
        }
    }


    private void Update()
    {
        // 1フレームに1度だけ必ず入力情報を更新する事
        var input = RpgAtsumaruApi.ControllerApi;
        input.Update();


        // 下方向ボタンが押されているなら
        if (input.GetButton(RpgAtsumaruInputKey.Down))
        {
            // プレイヤーを下に移動する処理など
        }


        // 決定ボタンが押された瞬間なら
        if (input.GetButtonDown(RpgAtsumaruInputKey.Enter))
        {
            // アイテムを使用する処理など
        }
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.ControllerApi | コントローラAPIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruController.Update() | RPGアツマール入力ステータスをラッチして状態を更新する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruController.GetButton() | キー入力されているかどうかを取得する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruController.GetButtonDown() | キー入力された瞬間のフレームかどうかを取得する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruInputKey | RPGアツマールで定義されているキーを定義しています |
