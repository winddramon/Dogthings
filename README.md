# DogThings - 狗东西Mod

[English](#english) | [中文](#chinese)

---

<a name="chinese"></a>
## 🇨🇳 中文

### 简介

为游戏《Casualties: Unknown》（DEMO V6.1）添加一系列狗狗主题的道具，让你的Expie享受地球小狗的快乐（？）生活。

### 前置依赖

- **BepInEx** (5.x)
- **RshLib** (2.1.5)

### 安装方法

1. 确保已安装 BepInEx 和 RshLib
2. 将 `DogThings.dll` 及 `img/` 文件夹（包含所有 PNG 图片）放入 `BepInEx/plugins/` 目录

### 包含道具

| 道具 ID | 名称 | 效果 |
|---------|------|------|
| `dogthings.dogcollar` | 狗项圈 | 佩戴时缓慢增加心情（上限 30） |
| `dogthings.dogbowl` | 狗食盆 | 从中喝取液体时额外增加点心情（上限 50） |
| `dogthings.petball` | 宠物球 | 拾取时增加心情 |
| `dogthings.e-collar` | 伊丽莎白圈 | 佩戴后防止一次自伤 |
| `dogthings.dogchew` | 狗咬胶 | 可食用多次，增加微量饱食和心情 |

### 编译指南

- 目标框架：`.NET Framework 4.7.2`
- 需要引用的 DLL：
  - `BepInEx.dll`
  - `RshLib.dll`
  - `Assembly-CSharp.dll`
  - `UnityEngine*.dll`

### 版本历史

#### v1.0.0
- 初始版本
- 添加 5 个狗狗主题道具

### 致谢

- 感谢RshLib作者rushellxyz提供的自定义物品框架

### 许可

#### DogThings Mod 本体

本 Mod 的原创代码（包括所有 `.cs` 文件和 Mod 专属资源）采用 **WTFPL**（Do What The Fuck You Want To Public License）协议。

简单来说：**你想做什么就做什么**。复制、修改、再分发、商用，都可以。

完整的协议文本见 http://www.wtfpl.net/ 页面。

#### 依赖组件

本 Mod 所依赖的其他组件（BepInEx、RshLib）遵循它们各自的许可条款。

---

<a name="english"></a>
## 🇺🇸 English

### Introduction

Adds a series of dog-themed items to *Casualties: Unknown* (Demo V6.1), allowing your Expie to enjoy the happy (?) life of an Earth dog.

### Requirements

- **BepInEx** (5.x)
- **RshLib** (2.1.5)

### Installation

1. Make sure BepInEx and RshLib are installed
2. Place `DogThings.dll` and the `img/` folder (contains all PNG files) into `BepInEx/plugins/`

### Items

| Item ID | Name | Effect |
|---------|------|--------|
| `dogthings.dogcollar` | Dog Collar | Slowly increases happiness when worn (max 30) |
| `dogthings.dogbowl` | Dog Bowl | Grants extra happiness when drinking from it (max 50) |
| `dogthings.petball` | Pet Ball | Grants happiness on pickup |
| `dogthings.e-collar` | Elizabethan Collar | Prevents self-harm once while worn |
| `dogthings.dogchew` | Dog Chew | Edible multiple times, restores small amounts of hunger and happiness |

### Build Guide

- Target Framework: `.NET Framework 4.7.2`
- Required DLL references:
  - `BepInEx.dll`
  - `RshLib.dll`
  - `Assembly-CSharp.dll`
  - `UnityEngine*.dll`

### Changelog

#### v1.0.0
- Initial release
- Added 5 dog-themed items

### Credits

- Thanks to rushellxyz (the author of RshLib) for the custom item framework

### License

#### DogThings Mod

The original code of this mod (including all `.cs` files and mod-specific resources) is licensed under the **WTFPL** (Do What The Fuck You Want To Public License).

**In short: Do whatever the fuck you want.** Copy, modify, redistribute, even commercially — go ahead.

Full license text: http://www.wtfpl.net/

#### Dependencies

Other components used by this mod (BepInEx, RshLib) are subject to their respective license terms.