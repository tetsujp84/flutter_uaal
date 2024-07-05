# Flutter Unity as a Library サンプルプロジェクト

このリポジトリには、Flutterアプリケーション内でUnityを実行する方法を示すサンプルプロジェクトが含まれています。本プロジェクトは、ARFoundation、ARKit、ARCoreを使用したアバタートラッキング機能と、iOS専用のフェイストラッキング機能を含みます。FlutterとUnityの連携には[flutter-unity-view-widget](https://github.com/juicycleff/flutter-unity-view-widget)を利用しています。

![sample](https://github.com/tetsujp84/flutter_uaal/assets/11347090/f3606d9f-07fe-4a51-8f71-a54050180479)

## 特徴


- **Unity統合**: Flutterプロジェクト内でUnityをライブラリとして実行。FlutterとUnity間でのメッセージングを実現。
- **アバタートラッキング**: ARFoundation、ARKit、ARCoreを使用したアバタートラッキング。
- **フェイストラッキング（iOSのみ）**: iOSデバイスでのARKitを利用したフェイストラッキング。

## 前提条件

開始する前に、以下の要件を満たしていることを確認してください

- **Flutter**: [Flutter公式サイト](https://flutter.dev/docs/get-started/install)からFlutter SDKをインストール。
- **Unity**: [Unity公式サイト](https://unity.com/)からUnity Hubおよび最新バージョンのUnity Editorをインストール（使用バージョン: 2022.3.35f1）。
- **ARKit/ARCore**: デバイスがARKit（iOS）またはARCore（Android）をサポートしていることを確認。

## 始め方

### ステップ1: リポジトリをクローン

```bash
git clone https://github.com/tetsujp84/flutter_uaal.git
cd flutter_uaal
```

### ステップ2: Flutterプロジェクトの設定

Flutterの依存関係をインストールします。

```bash
flutter pub get
```

### ステップ3: Unityプロジェクトの設定

1. Unity Hubで`unity/Unity-UaaL`ディレクトリを開きます。
2. Unityプロジェクトのメニューから `Flutter > Export iOS/Android` を実行し、ビルドを行います。これにより、`UnityLibrary(iOS)/unityLibrary(Android)`フォルダが生成されます。

### ステップ4: アプリケーションの実行

コマンドラインを使用してiOS/Androidデバイスでアプリを実行します。

```bash
flutter run
```

## トラブルシューティング
### iOSビルドに失敗する
[flutter-unity-view-widget](https://github.com/juicycleff/flutter-unity-view-widget)のSetupを参考に設定を行ってください。特に[Platform specific setup](https://github.com/juicycleff/flutter-unity-view-widget?tab=readme-ov-file#platform-specific-setup-after-unity-export)の項目内の`Unity-Iphone.xcodeproj`が正しく参照されているかや`UnityFramework.framework`が追加されているかに注意してください。
