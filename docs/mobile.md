# Mobile (Flutter) — Setup

## Prereqs
- Flutter 3 (stable), Xcode 15+, Android SDK 34, JDK 17

## Create project
```bash
cd apps/mobile-flutter
flutter create --org com.legendsleague --project-name legends_league .
```

## Packages
- State: **Riverpod** (or Bloc)
- HTTP: **dio** (+ retrofit/json_serializable)
- Realtime: **signalr_core**
- Push: **firebase_messaging** (+ APNs)

## Structure
```
lib/
  features/
    auth/
    leagues/
    draft/
    lineup/
  services/      # api, signalr, storage
  shared/        # widgets, theme, utils
  main.dart
```

## Flavors
- dev / staging / prod with separate bundle IDs and configs.

