import 'package:flutter/material.dart';
import 'package:flutter_unity_widget/flutter_unity_widget.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'UaaL Sample',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: const MainSceneWidget(),
    );
  }
}

class MainSceneWidget extends StatefulWidget {
  const MainSceneWidget({super.key});

  @override
  State<StatefulWidget> createState() => _MainSceneWidgetState();
}

class _MainSceneWidgetState extends State<MainSceneWidget> {
  UnityWidgetController? _unityWidgetController;
  String displayedText = "";

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('flutter uaal avatar sample'),
      ),
      body: SafeArea(
        child: Stack(children: <Widget>[
          UnityWidget(
            onUnityCreated: onUnityCreated,
            onUnityMessage: onUnityMessage,
            useAndroidViewSurface: true,
          ),
          Align(
            alignment: Alignment.bottomCenter,
            child: Padding(
              padding: const EdgeInsets.only(bottom: 50),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                mainAxisSize: MainAxisSize.min,
                children: [
                  ElevatedButton(
                    onPressed: () {
                      _unityWidgetController!.postMessage("Manager", 'PreviousAnimation', "");
                    },
                    child: const Text('<'),
                  ),
                  SizedBox(
                    width: 200, // テキスト表示領域の幅を固定
                    child: Text(
                      displayedText,
                      style: const TextStyle(fontSize: 20),
                      textAlign: TextAlign.center,
                    ),
                  ),
                  ElevatedButton(
                    onPressed: () {
                      _unityWidgetController!.postMessage("Manager", 'NextAnimation', "");
                    },
                    child: const Text('>'),
                  ),
                ],
              ),
            ),
          ),
        ]),
      ),
    );
  }

  void onUnityMessage(message) {
    // メッセージを受信したら、表示するテキストを更新
    final messages = message.toString().split("?");
    if (messages[0] == "animation") {
      setState(() {
        displayedText = messages[1];
      });
    }
  }

  // Callback that connects the created controller to the unity controller
  void onUnityCreated(controller) {
    _unityWidgetController = controller;
  }
}
