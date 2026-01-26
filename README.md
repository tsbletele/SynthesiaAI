# SynthesiaAI

SynthesiaAI is a C#/.NET-based project inspired by Synthesia-style piano visualizations.  
The goal is to analyze piano audio, extract musical notes, and visualize them as a falling-key animation in a web interface — while learning modern C#, .NET, and software architecture along the way.

This project is intentionally built step by step with a strong focus on **learning**, **clean architecture**, and **good GitHub practices**.

---

## Projects Overview

### 🔹 Synthesia.Api
- ASP.NET Core Web API
- Acts as the backend entry point
- Exposes endpoints for:
  - Audio upload
  - Note detection results
  - Future AI-related processing

### 🔹 Synthesia.Web
- Blazor WebAssembly frontend
- Responsible for:
  - UI rendering
  - Piano roll / falling notes visualization
  - Communicating with the API via HTTP

### 🔹 Synthesia.Core
- Pure domain and business logic
- Contains:
  - Note models
  - Timing data
  - Shared interfaces
- Has **no dependency** on UI or infrastructure

### 🔹 Synthesia.Audio
- Audio-related logic
- Responsibilities:
  - Reading audio files (MP3/WAV)
  - Pitch detection
  - Converting frequencies to musical notes
- Depends only on `Synthesia.Core`

---

## Tech Stack

- **Language:** C#
- **Framework:** .NET 10
- **Backend:** ASP.NET Core Web API
- **Frontend:** Blazor WebAssembly
- **Architecture:** Layered / Clean Architecture
- **Version Control:** Git & GitHub

---

## Current Status

- ✅ Solution structure created
- ✅ Projects wired with correct dependencies
- 🚧 Audio processing logic (in progress)
- 🚧 Visualization & AI logic (planned)
