# Unity-UIDemo

> 三套 UI 方案对照 ｜ Unity 2022.3.62f1 ｜ 3 个独立工程

同一套「登录 → 注册 → 选服」界面，分别用 **IMGUI、NGUI、UGUI** 三种方式实现，用来对比三者的开发方式、性能特征与适用场景。另附一个自研 IMGUI 控件库工程。

## 三个工程

| 工程 | UI 方案 | 内容 | 自研脚本 |
|---|---|---|---|
| `GUI-Demo` | IMGUI（`OnGUI`） | 自研控件库 + 6 个练习场景 | 17 个 / 约 760 行 |
| `UGUI_XM` | UGUI | 登录 / 注册 / 选服完整流程 | 17 个 / 约 1150 行 |
| `UI_NGUI_XM` | NGUI | 同功能 NGUI 版 + XML 存档 | 17 个 / 约 870 行 |

## 一、三套方案的差异

| 维度 | IMGUI | NGUI | UGUI |
|---|---|---|---|
| 渲染方式 | 每帧 `OnGUI` 重绘 | 网格 + DrawCall 合并 | 网格 + Canvas 合批 |
| 布局 | 代码里算坐标 | 拖拽 + Anchor | RectTransform + LayoutGroup |
| 事件 | 返回值判断 | `EventDelegate` 委托 | `Button.onClick` + `EventSystem` |
| 性能 | 差（无法合批、每帧重绘） | 好 | 好（同 Canvas 内自动合批） |
| 适用 | 编辑器工具、调试面板 | 老项目维护 | 新项目首选 |

## 二、GUI-Demo：自研 IMGUI 控件库

不依赖任何 UI 插件，用 `OnGUI` 手写了一套控件：

```
CustomGUIRoot          根节点，统一驱动子控件
  └── CustomGUIControl（抽象基类：样式绘制 / 位置计算）
        ├── CutomGUI_Button
        ├── CutomGUI_Input
        ├── CutomGUI_Label
        ├── CutomGUI_Slider
        ├── CutomGUI_Toggle
        ├── CutomGUI_ToggleGroup
        └── CutomGUI_Textrue
CustomGUIPos           九宫格对齐（E_Alignment_Type）+ 屏幕坐标换算
```

要点：

- 控件对外暴露 `add_clickEvent` / `add_changeValue`，用方法封装委托而非直接暴露 `event`，防止外部误清空监听链；
- `E_Style_OnOff` 支持常态与激活态两套样式；
- `CustomGUI_ToggleGroup` 保证组内互斥选中。

`Scripts/Other/` 下 6 个场景分别演示输入框、拖动条、下拉、组合控件等交互。

## 三、UGUI_XM：登录 / 注册 / 选服

完整的账号流程，包含数据校验与界面跳转：

| 模块 | 文件 | 职责 |
|---|---|---|
| 登录 | `LoginPanel`、`LoginMgr` | 账号密码校验、错误提示、进入选服 |
| 注册 | `RegisterPanel` | 格式校验、重复注册拦截、写回账号库 |
| 选服 | `ServerPanel`、`ChooseServerPanel`、`ServerLeftItem`、`ServerRightItem` | 分区列表、选中态联动、服务器状态展示 |
| 框架 | `UIManager`、`BasePanel` | 面板注册、显示隐藏、基础生命周期 |
| 数据 | `LoginData`、`RegisterData`、`ServerInfo`、`AccountServerData` | 账号库、服务器列表，`Dictionary<string, LoginData>` 组织账号与服务器的映射 |
| 存储 | `JsonMgr` | JSON 读写账号数据 |

## 四、UI_NGUI_XM：同功能的 NGUI 版

界面与流程和 UGUI 版一致，但 UI 层换成 NGUI：

- `UIPanel`、`UISprite`、`UILabel`、`UIButton`、`UIInput`、`UIToggle`、`UIGrid`、`UIScrollView`；
- 事件用 `btn.onClick.Add(new EventDelegate(() => { ... }))`；
- 数据层改用 XML：`XmlDataMar` + `SerializerDictionary<TKey, TValue>` 解决字典无法被 `XmlSerializer` 序列化的问题。

对照两版可以看出：**同一套业务逻辑，UI 层替换的成本主要来自事件绑定方式与布局体系不同**，数据层与流程控制可以复用。

## 目录结构

```
GUI-Demo/
  Scripts/Cutoms_GUI/    自研 IMGUI 控件库
  Scripts/Other/         6 个交互练习场景
UGUI_XM/
  Scripts/Login/         登录、注册、选服面板
  Scripts/UI/            UIManager、BasePanel
  Scripts/Data/          账号与服务器数据
UI_NGUI_XM/
  Scripts/UI/Login/      NGUI 版登录选服
  Scripts/XML/           XML 存档
```

三个工程各自独立，可分别用 Unity 打开。

## 说明

- `GUI-Demo` 与 `UGUI_XM`、`UI_NGUI_XM` 中的 `Assets/NGUI` 目录为第三方插件，不计入自研代码；
- 三个工程的账号体系均为本地模拟，未接入真实服务端。
