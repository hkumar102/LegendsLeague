# Admin (Next.js) — Setup

## Prereqs
- Node 20, npm or pnpm
- Optional: Vercel CLI for deploys

## Create app
```bash
cd apps/admin-next
npm create next-app@latest . -- --ts --app --eslint --src-dir --import-alias "@/*"
npm install
npm run dev
```

## Structure
```
app/admin/
  leagues/
  series/
  players/
components/
lib/api/        # OpenAPI client & fetchers
lib/auth/       # JWT/session, RBAC guards
```

## Data fetching
- Use **TanStack Query** or **SWR** for caching & retries.
- Generate TS client from `/packages/contracts/openapi.json` (openapi-typescript).

