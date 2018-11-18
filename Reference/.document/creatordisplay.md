作者の情報を表示しよう {#creatordisplay}
===

# 解説

プレイヤーに自身が制作した他のゲームなどを教えたり、自己紹介を兼ねたダイアログを表示する機能が  
RPGアツマールに存在しており、この機能を呼び出すこtが出来ます。  
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
        }
    }


    private async void Start()
    {
        // ニコニコユーザーID 12345 番のクリエイター情報を表示（待機しているのはRPGアツマールからの結果受け取りであって表示結果ではありません）
        await RpgAtsumaruApi.GeneralApi.ShowCreatorInformationAsync(12345);
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.GeneralApi | 汎用APIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruGeneral.ShowCreatorInformationAsync() | クリエイター情報表示ダイアログを表示する関数 |
