# Investimentos

[![CI](https://github.com/AlexBrosso/Investimentos/actions/workflows/ci.yml/badge.svg)](https://github.com/AlexBrosso/Investimentos/actions/workflows/ci.yml)

A portfolio tracker for B3 (Brazilian stock exchange) investors: record your buy and sell trades and let the app calculate your position, average price and returns using real market quotes.

Investors often track their trades in spreadsheets, calculating average prices and gains by hand. This project replaces that spreadsheet with an API that does the math consistently, following the Brazilian tax rule for average cost (weighted average, as required by Receita Federal).

> **Status:** early development. This is a study project focused on modern .NET backend practices.

## Features

Planned for the MVP:

- User registration and login
- Multiple portfolios per user
- Buy and sell trades for stocks, REITs (FIIs) and ETFs, plus fixed-income contributions
- Position per asset with weighted average price
- Realized and unrealized gains, with real market quotes
- Trade history (immutable ledger: mistakes are fixed with reversals, never edits)

## Tech stack

- .NET 10 / ASP.NET Core Web API
- xUnit for tests
- GitHub Actions for CI

Planned: PostgreSQL + EF Core, Redis, RabbitMQ + MassTransit, Serilog + OpenTelemetry, Docker Compose, Azure Container Apps and a React + TypeScript front end.

## Project structure

```
src/
  Api/             ASP.NET Core Web API (entry point)
  Domain/          Business rules and entities (no dependencies)
  Infrastructure/  Database and external services
tests/
  Domain.Tests/    Unit tests for the domain
```

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (the exact version is pinned in `global.json`)

### Running the API

```bash
dotnet run --project src/Api
```

Then check that it is up:

```bash
curl http://localhost:5125/health
```

It should return `Healthy`.

### Running the tests

```bash
dotnet test
```
