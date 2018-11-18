URLに添えられたパラメータを取り出そう {#queryget}
===

# 解説

ゲームURLにクエリとして「param1=～&param2=～」といった変数を付けて、ゲームを起動された際に  
このパラメータをゲーム側で取り出すために、クエリ変数取得機能を使います。  
RPGアツマールの仕様上、扱えるクエリ変数は"param"から始まる"1～9"までの変数となっております。  
サンプルコードは以下のとおりです。

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


    private void Start()
    {
        // もし param1 に特別なコードが設定されていたら
        if (RpgAtsumaruApi.GeneralApi.GetQuery("param1") == "specialcode")
        {
            // 無敵モードになる処理を入れたり、アイテムプレゼント処理を入れたり、してみると良いかもです。
        }
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.GeneralApi | 汎用APIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruGeneral.GetQuery() | URLのクエリ変数を取得する関数 |
