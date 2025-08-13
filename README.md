# Legends League — Monorepo

## 📌 Overview
Legends League is a **series-based fantasy cricket platform** with private, commissioner-led leagues, **snake drafts**, rolling lineup locks, and **live scoring**.

This repository contains **all apps and shared packages** in a single monorepo:
- **/apps/api-dotnet** — .NET 8 Web API (modular monolith, SignalR)
- **/apps/admin-next** — Next.js Admin (TypeScript)
- **/apps/mobile-flutter** — Flutter mobile app (iOS/Android)
- **/packages/contracts** — OpenAPI contracts (source of truth)
- **/packages/design-tokens** — Shared brand tokens
- **/infra** — DevOps scripts, Docker, environment docs

## 🛠 Tech Stack
**Backend:** .NET 8, MediatR, FluentValidation, AutoMapper, EF Core (Postgres), SignalR, Serilog  
**Admin:** Next.js 14 (TS), Tailwind, TanStack Query  
**Mobile:** Flutter 3 (Dart), Riverpod/Bloc, dio, Firebase Messaging  
**Shared:** OpenAPI contracts, design tokens

## 📂 Structure
```
apps/
  api-dotnet/
  admin-next/
  mobile-flutter/
packages/
  contracts/
  design-tokens/
infra/
.github/workflows/
docs/
```

## 🔄 Workflow
1) Define API **contracts** in .NET → export **OpenAPI** to `/packages/contracts/openapi.json`  
2) Generate clients in Admin (TS) & Mobile (Dart)  
3) Build features **end-to-end** (API → Admin → Mobile)  
4) Use **SignalR** for real-time updates where needed  

## 🚀 Phases
See **docs/roadmap.md** for milestone details.

## 📚 Docs
- **docs/architecture.md** — architecture & modules
- **docs/backend.md** — .NET API setup
- **docs/admin.md** — Next.js Admin setup
- **docs/mobile.md** — Flutter setup
- **docs/database.md** — schema & ERDs
- **docs/contributing.md** — branching, commits, PRs
- **docs/roadmap.md** — phases & milestones

## ✅ Next Steps
1. Scaffold API in `/apps/api-dotnet`  
2. Export first `openapi.json` to `/packages/contracts/`  
3. Bootstrap Next.js Admin & Flutter Mobile  
4. Implement **Phase 1** features
