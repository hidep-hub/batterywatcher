# BatteryWatcher

タスクトレイの常駐アイコンの中に、バッテリー残量（%）を常時数字表示するWindows用ユーティリティ。

詳しい背景・設計方針は [docs/BW-001_調査・設計提案.md](docs/BW-001_調査・設計提案.md) を参照。

## 動作環境

- **Windows専用**（Windows 10 / 11）。WinForms + Win32ネイティブAPI（`GetSystemPowerStatus`等）に依存しているため、macOS/Linuxではビルド・実行不可。

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
└── Icon/                          # トレイアイコンの動的描画（GDI+）
```
