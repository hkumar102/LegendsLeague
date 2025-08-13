# Roadmap & Phases

## Phase 1 — Core Setup & Seeding
- Backend: Auth (JWT), Series/Leagues CRUD, OpenAPI publish
- Admin: Login, Series/Teams/Players CRUD
- Mobile: Login, view series/leagues

## Phase 2 — Draft System
- Backend: Snake draft, timers, SignalR draft events
- Admin: Draft control
- Mobile: Join league via invite, live draft room

## Phase 3 — Live Scoring
- Integrate provider, ingest to RawBallEvents → BallEvents
- Scoring rules engine → FantasyPointsLog
- Live standings via SignalR

## Phase 4 — Lineups & Locking
- Backend: lineup submit, locks by fixture
- Mobile: XI management
- Admin: override tooling

## Phase 5 — Polishing & Ops
- Notifications, telemetry, FAAB, trades, waivers v2
- Performance tuning and scaling
