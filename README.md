# BatteryWatcher

タスクトレイの常駐アイコンの中に、バッテリー残量（%）を常時数字表示するWindows用ユーティリティ。

詳しい背景・設計方針は [docs/BW-001_調査・設計提案.md](docs/BW-001_調査・設計提案.md) を参照。

## 動作環境

- **Windows専用**（Windows 10 / 11）。WinForms + Win32ネイティブAPI（`GetSystemPowerStatus`等）に依存しているため、macOS/Linuxではビルド・実行不可。

## インストール

1. [Releasesページ](https://github.com/hidep-hub/batterywatcher/releases/latest) から最新版の `BatteryWatcher.exe`（自己完結の単一exe。.NETランタイムのインストール不要）をダウンロードし、任意のフォルダに配置する。
   - 自分でビルドする場合は後述の「開発者向け情報」内の「配布用ビルド」を参照。
2. `BatteryWatcher.exe` をダブルクリックで起動する。インストーラは無く、レジストリ登録もこの時点では発生しない。
3. タスクトレイに常駐アイコンが表示されれば起動完了（メインウィンドウは表示されない仕様）。

Windows起動時に自動的に立ち上げたい場合は、後述の設定画面から「Windows起動時に自動的に開始する」をONにする（`HKCU\...\Run` への登録で実現しており、レジストリを直接編集する必要はない）。

## 使い方

### タスクトレイアイコンの見方

| 状態 | 表示 |
|---|---|
| バッテリー駆動 | 残量%を数字表示（例: `73%`）。100%時は `FULL` |
| AC電源接続中 | プラグのアイコン表示 |
| バッテリー非搭載機 | `--` 表示（グレー） |
| 残量が「赤」のしきい値未満 | 点滅表示（点滅色A/Bを交互表示） |

数字の色は残量に応じて 緑／黄／赤 の3段階に自動で切り替わる（しきい値・配色は設定画面で変更可能）。トレイアイコンにマウスを乗せるとツールチップで詳細（%・AC/バッテリー・充電中/非充電）を確認できる。

### 右クリックメニュー

トレイアイコンを右クリックすると以下のメニューが出る。

- **設定**: 表示・配色・自動起動を変更する設定画面を開く
- **終了**: アプリを終了する

### 設定画面

| 項目 | 内容 |
|---|---|
| しきい値（緑/黄/赤 最小%） | 数字の色を切り替える残量の境界値 |
| 配色（電源接続・緑・黄・赤・点滅色A/B） | 各状態の表示色。ボタンクリックでカラーピッカーから変更 |
| 点滅を有効にする / 点滅間隔(ms) | 「赤」しきい値未満になったときの点滅ON/OFFと速さ |
| Windows起動時に自動的に開始する | チェックすると次回Windowsログオン時から自動起動 |

「OK」で設定を反映、「キャンセル」で破棄する。

<details>
<summary>開発者向け情報</summary>

## 開発に必要なソフトウェア

| ソフトウェア | バージョン | 用途 |
|---|---|---|
| [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) | 8.0系 | ビルド・実行（`net8.0-windows`をターゲット） |
| Visual Studio 2022 | 17.0以降（Community可） | IDE。「.NET デスクトップ開発」ワークロードが必要（WinForms設計者を使うため） |
| Git | 任意 | ソース管理 |

Visual Studioを使わない場合は、.NET 8 SDKと任意のエディタ（VS Code等）だけでもビルド可能（後述のCLIコマンド参照）。

## セットアップ

```powershell
git clone <このリポジトリのURL>
cd batterywatcher
```

Visual Studioで開く場合は `BatteryWatcher.sln` をダブルクリック。

## ビルド

### Visual Studioから
`BatteryWatcher.sln` を開き、通常のビルド（F6）/実行（F5）でOK。

### CLIから
```powershell
dotnet build src/BatteryWatcher/BatteryWatcher.csproj
```

### 配布用ビルド（自己完結・単一exe化）

`BatteryWatcher.csproj` は以下の設定になっており、`dotnet publish` すると **.NETランタイム未導入のPCでも動く単一exe** が生成される。

```powershell
dotnet publish src/BatteryWatcher/BatteryWatcher.csproj -c Release
```

出力先: `src/BatteryWatcher/bin/Release/net8.0-windows/win-x64/publish/`

生成された `BatteryWatcher.exe` を配布先のPCへコピーするだけで動作する（上記「インストール」参照）。

## リリース手順（GitHub Releases）

現時点では手動リリース運用（CIによる自動ビルド・公開は未整備）。

1. 上記の配布用ビルドコマンドで `BatteryWatcher.exe` を生成する。
2. `git tag vX.Y.Z` でバージョンタグを作成し `git push origin vX.Y.Z`。
3. GitHubの [Releasesページ](https://github.com/hidep-hub/batterywatcher/releases) で「Draft a new release」から上記タグを選択し、生成した `BatteryWatcher.exe` を添付してリリースを公開する。

## ビルド時の注意点

- **`RuntimeIdentifier` が `win-x64` に固定**されている。ARM64版Windowsでネイティブ動作させたい場合は `.csproj` の `RuntimeIdentifier` を書き換える必要あり（win-x64のまま起動はできるがエミュレーション経由になる）。
- `SelfContained=true` + `PublishSingleFile=true` + `IncludeNativeLibrariesForSelfExtract=true` の組み合わせなので、`publish` 後の実行ファイルは単体で動作するが、**サイズは数十MB規模になる**（.NETランタイム同梱のため）。`dotnet build` だけの出力（`bin/Debug` 等）は単一exeにはならず、別途DLL群が必要になる点に注意。
- `AllowUnsafeBlocks=true` かつ `Power/NativeMethods.cs` / `Icon/NativeMethods.cs` でP/Invoke（Win32 API直呼び）を行っている。ここを触る場合はアンマネージコードの扱いに注意。
- `InvariantGlobalization=true` のため、カルチャ依存の書式（日付・数値のロケール別フォーマット等）はデフォルトで無効化されている。日本語UI文字列自体はハードコードなので問題ないが、将来ロケール依存処理を追加する場合は要検討。
- タスクトレイ常駐アプリのため、`dotnet run` で起動すると通知領域にアイコンが出る。デバッグ時に多重起動すると複数アイコンが残るので、動作確認後は必ず終了（タスクトレイアイコン右クリック→終了、またはタスクマネージャー）してから再ビルドすること。

## プロジェクト構成

```
src/BatteryWatcher/
├── Program.cs                    # エントリポイント
├── Form1.cs / Form1.Designer.cs  # メインフォーム（非表示、トレイアイコンのホスト）
├── SettingsForm.cs / .Designer.cs# 右クリック設定画面（表示形式・しきい値・配色）
├── Power/                         # バッテリー状態取得・表示値算出（Win32 API）
├── Icon/                          # トレイアイコンの動的描画（GDI+）
└── Startup/                       # Windowsスタートアップ登録（レジストリRunキー）
```

</details>
