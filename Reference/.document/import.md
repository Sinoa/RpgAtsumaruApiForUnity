Unityへインポート {#import}
===

## ダウンロードしたzipを解凍しよう

[ダウンロードページ]からダウンロードしたzipファイルは、自身が管理する適切なライブラリディレクトリなどに解凍して下さい。  
解凍された場所が、今後Unityプロジェクトから参照されるディレクトリとなりますので一時的な場所は避けましょう。  
![](@ref image001.jpg)  

## Unityのパッケージマネージャを開いてインポートボタンをクリック

"RPGアツマールAPI for Unity"は、Unityパッケージマネージャによる管理方式をサポートしています。  
そのため、Unityプロジェクトに直接インポートしなくても、プロジェクトに機能を追加することが出来ます。  
なので、Unityパッケージマネージャからインポートをします。  
  
![Unityパッケージマネージャを選択](@ref image002.jpg)  
  
![ウィンドウの下部にある+ボタンをクリックし Add package from disk... を選択](@ref image003.jpg)  

## パッケージを選択して完了

解凍した"RPGアツマールAPI for Unity"のディレクトリ直下に、"package.json"というパッケージマニフェストがあるので  
そのファイルを選択することで、Unityパッケージマネージャによってインポートされ  
プロジェクトで"RPGアツマールAPI for Unity"が使えるようになります。  
  
![package.json を選択](@ref image004.jpg)  
  
![RPGアツマールAPI for Unityがインポートされている事が確認できる](@ref image005.jpg)  

[ダウンロードページ]: https://github.com/Sinoa/RpgAtsumaruApiForUnity/releases "ダウンロードページ"
