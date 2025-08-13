# Legends League Monorepo

This repository hosts:
- **apps/api-dotnet** — .NET 8 Web API (modular monolith, SignalR).
- **apps/admin-next** — Next.js Admin (TypeScript).
- **apps/mobile-flutter** — Flutter mobile app (iOS/Android).
- **packages/contracts** — OpenAPI JSON and shared schemas (source of truth for clients).
- **packages/design-tokens** — Brand tokens (colors, spacing) shared across apps.
- **infra** — DevOps scripts, Docker, environment docs.

## Getting Started

### 1) API (.NET 8)
```bash
cd apps/api-dotnet
dotnet new webapi -n LegendsLeague.Api
dotnet dev-certs https --trust
dotnet run
```

### 2) Admin (Next.js)
```bash
cd apps/admin-next
npm create next-app@latest . -- --ts --app --eslint --src-dir --import-alias "@/*"
npm install
npm run dev
```

### 3) Mobile (Flutter)
```bash
cd apps/mobile-flutter
flutter create --org com.legendsleague --project-name legends_league .
flutter run
```

### 4) Contracts (OpenAPI)
- The API publishes `openapi.json` to `packages/contracts/` during CI.
- Admin and Mobile generate typed clients from that file.

## CI
GitHub Actions workflows are in `.github/workflows/*`.
