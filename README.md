# 🎮 今すぐあなたに会いたい (Project: Anaai)

**「連打」×「ノベル」**
単純な「連打アクション」というメカニクスに、リッチなUI演出とストーリー（ノベルパート）を組み合わせることで、**「どこまで没入感のある体験を作れるか」**を検証する実験的プロジェクトです。

<p align="left">
  <img src="https://img.shields.io/badge/Unity-6000.1.0f1-000000?style=for-the-badge&logo=unity&logoColor=white" />
  <img src="https://img.shields.io/badge/Genre-Narrative_Clicker-FF4081?style=for-the-badge" />
</p>

<a href="https://daimaruyama.github.io/projects/anaai.html">
  <img src="https://img.shields.io/badge/🎨_Visuals-View_Portfolio_Page-ff69b4?style=for-the-badge&logo=github" />
</a>

---

## 🎯 Concept & Challenge

**"Elevating Simple Mechanics with UX"**
ゲームシステム自体は「ボタンを連打して走る」という非常にシンプルなものです。
しかし、そこに**「今すぐヒロインに会いに行かなければならない」**というストーリーの文脈と、感情を揺さぶる**テキスト演出**を乗せることで、単純作業を「焦燥感のあるゲーム体験」へ昇華させることを目指しています。

### 🚀 Phase 2: Polish & Refactoring
本リポジトリ（Making2nd）では、初期プロトタイプからさらに体験の質を高めるため、以下の実装を行っています。
* **Game Feel**: 連打時のUIフィードバックやパーティクル作成など、「手触り」の強化
* **Narrative UX**: ノベルパートにおけるテキスト表示演出の多様化
* **Refactoring**: 拡張性を考慮した設計への移行

---

## 📝 Technical Highlights

### 💬 感情を伝える「文字送り」システム
ノベルパートにおいて、プレイヤーを物語に引き込むために**「テキストの表示演出（Typewriter Effect）」**を作り込みました。
単調な表示を避け、以下の3パターンを実装・比較検証しています。

1.  **Standard**: 基本的な等速表示
2.  **Punctuation Wait**: 句読点で一瞬の間を持たせ、読むリズムを作る
3.  **Emotional**: 感情値に応じて表示速度を動的に変化させる

> **Detailed Article**
> 
> 文字送りの実装詳細や、UX比較で得られた知見はNoteにて技術記事として公開しています。
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

## 🛠 Usage & Environment

- **Engine**: Unity 6 (6000.1.0f1)
- **Language**: C#
- **IDE**: Visual Studio 2022
