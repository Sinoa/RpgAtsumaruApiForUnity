# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [2.0.1] - 2020-07-27
### Changed
- 古い情報のまま 2.0.0 バージョンを配信してしまったため 2.0.1 を配信させていただきました。

## [2.0.0] - 2020-07-25
### Security
- サポートUnityバージョンを 2019.4.0f1 以上に引き上げました。

### Fixed
- Windowsビルドに失敗する問題に対応しました。

### Changed
- パッケージ配信方法を変更しました。

## [1.2.0] - 2018-12-08
### Added
- モバイル版RPGアツマールでも起動できるようになりました。
- RPGアツマールの新スクリーンショットAPIに対応しました。
- RPGアツマール向けWebGLテンプレートを生成出来るようになりました。

### Fixed
- コントローラAPIにて、左ボタンが決定ボタンとして動作してしまう問題の修正。
- エディタツールがプログラムコンパイル時にエラーで閉じられてしまう問題の修正。

### Deprecated
- 新スクリーンショットAPI対応に伴い「General.SetDefaultScreenShotImgeData」関数が"Obsolete"になりました。

## [1.1.1] - 2018-12-02
### Changed
- スコアボードの送信スコア値が、規定値範囲外の場合例外をスローするようになりました。
- "DevelopmentBuild"でビルドされた場合に、ライブラリがダミー動作するように変更。

## [1.1.0] - 2018-11-24
### Added
- スクリーンショットAPIに対応。

## [1.0.0] - 2018-11-18
### Added
- 初版リリース。
