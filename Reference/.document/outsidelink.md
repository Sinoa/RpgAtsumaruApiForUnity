外部リンクに誘導しよう {#outsidelink}
===

# 解説

ゲームから公式サイトや攻略サイトなど、外部のサイトへ誘導する場合は、RPGアツマールから別のサイトに移動する  
ウィンドウを表示し、プレイヤーに別のサイトが表示される事を通知する必要があります。

また、表示する通知ダイアログにはリンク文字列も表示されます。  
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
        // 外部リンクを開いてRPGアツマールから結果を受け取るまで待機（表示完了の待機ではありません）
        await RpgAtsumaruApi.GeneralApi.OpenLinkAsync("http://hogehogeabcdefg.com/foobar.html");
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.GeneralApi | 汎用APIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruGeneral.OpenLinkAsync() | RPGアツマールに外部リンク誘導ダイアログを表示してもらう関数 |
