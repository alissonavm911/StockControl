# 📝 Changelog - StockControl

Todas as mudanças notáveis deste projeto serão documentadas neste arquivo.

O formato é baseado em [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/).

## [2.0.0] - 2026-10-15 - **VERSÃO FINAL**

> **Status: Projeto Finalizado.** Não haverá novas versões ou atualizações futuras. Esta é a versão definitiva.

### ✨ Added

- **Módulo de Vendas completo:**
  - `feat: add sale and saleDto structs` (`Sale`, `SaleItem`, `SaleDto`)
  - `feat: add sales data saving and exception handling` (persistência em `sales.json`)
  - `feat: add crud methods in sales repository` (`SalesRepository` com add/view/remove)
  - `feat: implement add, view and remove sale methods` no `SaleController` / `SaleService`
  - `feat: add sale registration with date-only persistence` (uso de `DateOnly`)
  - Validação de existência de produto antes da venda (`feat: validator exist product`)
  - Cálculo automático de estoque e valor total na criação da venda (`feat: implement complete sale creation with stock calculation and validation`)
- **Relatório Financeiro:**
  - `feat: implement export financial report in PDF or TXT` (`FinancialReportExporter`)
  - Receita total, ticket médio, total de vendas e unidades vendidas
  - Top 5 produtos mais vendidos
  - Receita por categoria
  - Alerta de produtos para reabastecer (< 3 unidades)
  - Lista completa de transações
- **Menu expandido de 6 para 10 opções:**
  - 7 - Register Sale
  - 8 - Remove Sale
  - 9 - View Sales
  - 10 - Financial Report (TXT/PDF)
- **Releases multi-plataforma:**
  - Windows x64 / x86, Linux x64, macOS x64 / ARM64 (self-contained)
  - Scripts `BUILD-RELEASES.bat` e `COMPRESS-RELEASES.bat` atualizados para `v2.0`

### 🔄 Changed

- `refactor: update models for dictionary` - modelos ajustados para dicionário
- `refactor: remove IDE configurations files and update .gitignore` - limpeza de arquivos de IDE
- `refactor: remove white lines` + `refactor: remove a line unsed` - limpeza de código
- `StockControl.csproj` atualizado para `Version 2.0.0` (`AssemblyVersion` / `FileVersion`)
- `README.md` reescrito no padrão v1.0 com badges, guia de uso, tabela de downloads e seção de versão final
- `QUICK-START.md` atualizado para v2.0 (sistema completo Estoque + Vendas)

### 🐛 Fixed

- `fix: validate sales and stabilize menu navigation` - validação de vendas e estabilidade do menu
- Correção de formatação em `FinancialReportExporter` e `InventoryReportExporter` (TXT/PDF pt-BR)

### 🗑️ Removed

- **Roadmap Futuro removido permanentemente** - Interface gráfica (WPF/MAUI), API REST, Banco SQL, autenticação, dashboard, importação CSV/Excel, audit log e backup em nuvem **não serão implementados**. Projeto encerrado na v2.0.
- Branch `feat/sales` removida após merge em `main` (PR finalizado)
- Arquivos temporários `temp-*.md` removidos

---

## [1.0.0] - Data da tag v1.0

### ✨ Added

- Controle de estoque básico (CRUD de Produtos)
- Controle de quantidade, preço e categoria (11 categorias)
- Relatório de inventário em TXT e PDF
- Persistência em `inventory.json` (`%APPDATA%\StockControl\`)
- Menu console com 6 opções
- Tratamento centralizado de exceções (`HandlerException`)
- Scripts de build `scripts/` + docs `README.md`, `QUICK-START.md`, `RELEASES.md`, `SETUP-SUMMARY.md`

---

## 📦 Releases

| Versão | Tag | Downloads |
|--------|-----|-----------|
| **2.0.0 (Final)** | `v2.0` | [StockControl-v2.0-Windows-x64.zip](https://github.com/alissonavm911/StockControl/releases/download/v2.0/StockControl-v2.0-Windows-x64.zip) • [StockControl-v2.0-Windows-x86.zip](https://github.com/alissonavm911/StockControl/releases/download/v2.0/StockControl-v2.0-Windows-x86.zip) • [StockControl-v2.0-Linux-x64.zip](https://github.com/alissonavm911/StockControl/releases/download/v2.0/StockControl-v2.0-Linux-x64.zip) • [StockControl-v2.0-macOS-x64.zip](https://github.com/alissonavm911/StockControl/releases/download/v2.0/StockControl-v2.0-macOS-x64.zip) • [StockControl-v2.0-macOS-ARM64.zip](https://github.com/alissonavm911/StockControl/releases/download/v2.0/StockControl-v2.0-macOS-ARM64.zip) |
| 1.0.0 | `v1.0` | Ver página de [Releases](https://github.com/alissonavm911/StockControl/releases) |

**Tamanho:** ~200-300 MB por plataforma (runtime .NET 10 incluído, self-contained)
