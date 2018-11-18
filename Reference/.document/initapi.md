初期化をしよう {#initapi}
===

# 解説

"RPGアツマールAPI for Unity"は、プラグイン内部にコンテキストを持っているため  
このコンテキストを初期化しなければ、あらゆるAPIの動作は失敗してしまいます。  
その、コンテキストを初期化するための手段が用意されています。

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
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.Initialize() | プラグインの初期化関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.Initialized | 初期化が出来ているかの確認プロパティ |
