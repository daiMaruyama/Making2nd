# 🎮 いますぐあなたに会いたいの (Project: Anaai)

**第2回審査会 提出作品**

**「連打」×「ノベル」**
単純な「連打アクション」というメカニクスに、DOTweenを用いたリッチなUI演出とストーリー（ノベルパート）を組み合わせることで、**「どこまで没入感のある体験を作れるか」**を検証した実験的プロジェクトです。

<p align="left">
  <img src="https://img.shields.io/badge/Unity-6000.1.0f1-000000?style=for-the-badge&logo=unity&logoColor=white" />
  <img src="https://img.shields.io/badge/Tech-DOTween-691564?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Genre-Narrative_Clicker-FF4081?style=for-the-badge" />
</p>

<a href="https://daimaruyama.github.io/projects/anaai.html">
  <img src="https://img.shields.io/badge/🎨_Visuals-View_Portfolio_Page-ff69b4?style=for-the-badge&logo=github" />
</a>

---

## 🎯 Concept & Challenge

**"Elevating Simple Mechanics with UX"**
ゲームシステム自体は「ボタンを連打して走る」という非常にシンプルなものです。
しかし、そこに**「今すぐヒロインに会いに行かなければならない」**というストーリーの文脈と、感情を揺さぶる演出を乗せることで、単純作業を「焦燥感のあるゲーム体験」へ昇華させることを目指しています。

### 🚀 Phase 2: Polish & Refactoring
本リポジトリ（Making2nd）では、初期プロトタイプからさらに体験の質を高めるため、以下の実装を行っています。
* **Visual Polish**: DOTweenを活用した「気持ちいい」UIモーションの実装
* **Game Modes**: 1人プレイ（対AI）と2人対戦モードの実装
* **Narrative UX**: ノベルパートにおけるテキスト表示演出の多様化

---

## 📝 Technical Highlights

### ✨ DOTweenによるUI演出の強化 (Polished UI)
カジュアルゲームにおいて重要な「触り心地」を追求するため、コードベースで詳細なアニメーション制御を行いました。
* **Sequence制御**: タイトルロゴや文字演出において、複数のアニメーションを `Sequence` で連結し、ストーリー性を感じる動きを構築。
* **Ease設定**: 単調な動きにならないようイージング（Ease）を調整し、吸い付くような「手触り」を実現。

### 🤖 ゲームシステムとAIの実装 (Game Logic)
* **Versus AI**: 1人プレイ時に対戦相手となるAIロジックを構築。ランダム性だけでなく、プレイヤーに程よい緊張感を与えるバランス調整を実施。
* **Mode Switching**: シングルプレイとローカル2人対戦（2P）の両方に対応するゲームループ設計。

### 💬 感情を伝える「文字送り」システム (Text UX)
ノベルパートの没入感を高めるため、以下の3パターンの文字送りを実装・検証しました。
1. **Standard**: 等速表示
2. **Punctuation Wait**: 句読点の間
3. **Emotional**: 感情値による速度変化

> **Detailed Article**
> 
> 文字送りの実装詳細や、UX比較で得られた知見は技術記事として公開しています。
> <br>
> <a href="https://note.com/m4rud/n/nd02a6b4c9b19">
>   <img src="https://img.shields.io/badge/Note-文字送り3パターンの実装とUX比較-262626?style=for-the-badge&logo=notion&logoColor=white" />
> </a>

---

## 📅 Development Log

| Date | Category | Details |
| :--- | :--- | :--- |
| **2025-09-25** | `UX / Polish` | **文字送り演出の3パターン実装**<br>ノベルパートの没入感を高めるため、テキスト表示速度や「間」の制御を細分化。<br>実装知見を[Note記事](https://note.com/m4rud/n/nd02a6b4c9b19)として公開。 |
| **2025-09-20** | `Refactor` | **仕様検討・設計フェーズ**<br>第2フェーズ始動。単純な連打ゲーとしての面白さを底上げするため、実装方針の再検討を開始。 |

---

## 🔗 Links

- **Play on UnityRoom**: [https://unityroom.com/games/anaai_0919](https://unityroom.com/games/anaai_0919)

---

## 🛠 Environment

- **Engine**: Unity 6 (6000.1.0f1)
- **Language**: C#
- **IDE**: Visual Studio 2022
- **Assets**: DOTween
