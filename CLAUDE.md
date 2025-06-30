# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

BusInfoSys is a bus information system with a LINE Bot integration. The system provides real-time bus information, route planning, and reminder services through both a web interface and LINE Bot.

## Architecture

The project consists of three main components:

1. **ASP.NET Core Web API** (`/api/`) - Backend API with Entity Framework Core, JWT authentication for LINE ID tokens, and SQL Server database
2. **React Frontend** (`/frontend/`) - Web interface built with React, Material-UI, and React Router
3. **LINE Bot Integration** - Webhook handling, rich menu management, and message broadcasting

### Key Services

- **TDXTokenService**: Handles authentication with Taiwan's TDX (Transport Data eXchange) API
- **BusInfoService**: Retrieves real-time bus data from TDX API
- **LineMessageService**: Manages LINE Bot message sending and broadcasting
- **ReminderBackgroundService**: Background service for scheduled bus arrival reminders
- **RemindRepository**: Database operations for user reminder settings

## Development Commands

### Docker Development Environment

Start all services:

```bash
docker-compose up -d
```

Individual services:

- Web API: `docker-compose up webapi`
- SQL Server: `docker-compose up sqlserver`
- React: `docker-compose up react`
- ngrok (for LINE webhook): `docker-compose up ngrok`

### ASP.NET Core API (`/api/`)

Build:

```bash
dotnet build
```

Run locally:

```bash
dotnet run
```

Database migrations:

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### React Frontend (`/frontend/`)

Install dependencies:

```bash
npm install
```

Start development server:

```bash
npm start
```

Build for production:

```bash
npm run build
```

Run tests:

```bash
npm test
```

## Database

- SQL Server database configured in docker-compose
- Entity Framework Core with Code First approach
- Connection string in `appsettings.Development.json`
- Models include User, Bus, Stop, Favorite, and Remind entities

## External API Integration

- **TDX API**: Taiwan's transport data API for real-time bus information
- **LINE Messaging API**: For bot functionality and user authentication
- JWT authentication configured for LINE ID tokens

## Key Configuration

- LINE Bot channel configuration in `appsettings.json`
- TDX API credentials in configuration
- ngrok tunnel for LINE webhook development (token in docker-compose.yml)
- CORS disabled but configuration present for React frontend

## Static Files

The API serves static HTML/CSS/JS files from `wwwroot/` for the LINE LIFF (LINE Front-end Framework) application pages.


# 前後端分離架構規劃

建議的 React + Material-UI 架構：

frontend/src/
├── components/ # 共用元件
│ ├── BusStopCard.js # 公車站點卡片
│ ├── DirectionTabs.js # 方向切換標籤
│ ├── NumericKeyboard.js # 數字鍵盤
│ ├── ProgressBar.js # 進度條
│ ├── TimeSelector.js # 時間選擇器
│ └── WeekSelector.js # 星期選擇器
├── pages/
│ ├── BusRouteInput.js # 路線輸入頁面
│ ├── BusStopOfRoute.js # 站點列表頁面
│ └── BusRemind.js # 提醒設定頁面
├── services/
│ ├── api.js # API 統一管理
│ ├── liffService.js # LIFF 服務
│ └── busService.js # 公車資料服務
├── hooks/
│ ├── useLiff.js # LIFF Hook
│ ├── useBusData.js # 公車資料 Hook
│ └── useInterval.js # 定時更新 Hook
├── contexts/
│ └── BusDataContext.js # 全域狀態管理
└── utils/
├── storage.js # LocalStorage 操作
└── timeFormat.js # 時間格式化

Material-UI 元件對應：

- TextField + InputAdornment → 搜尋框
- Tabs + Tab → 方向切換
- Card + CardContent → 站點卡片
- LinearProgress → 進度條
- ButtonGroup → 數字鍵盤
- FormControlLabel + Radio → 提醒時間選擇
- Checkbox → 星期選擇
- TimePicker → 時間範圍設定
- BottomSheet (自製或第三方) → 底部選單

  關鍵整合點：

  1. LIFF SDK - 維持原有 LINE 登入機制
  2. 即時更新 - 使用 React Hook 重寫 15 秒輪詢
  3. 狀態管理 - Context API 或 Redux 管理全域公車資料
  4. 路由管理 - React Router 替代原有頁面跳轉
  5. 響應式設計 - Material-UI Grid 系統適配行動裝置

# 任務紀錄

## 2025-06-27

### 已完成基礎架構設定：

已完成的工作：

1. 清理舊的 Home 和 Tour 頁面
2. 建立新的資料夾結構 (components, pages, services, hooks, contexts, utils)
3. 建立公車系統的基礎路由結構
4. 建立 LIFF Hook 和 Service

目前的架構狀態：

- 建立了三個基礎頁面: BusRouteInput, BusStopOfRoute, BusRemind
- 設定了 Material-UI 主題和基礎路由
- 建立了 LIFF 整合服務 (liffService.js) 和 Hook (useLiff.js)
- 資料夾結構完整，準備好進行後續開發
