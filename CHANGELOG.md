# CHANGELOG - Stock Control

## v2.0.0 (2026-10-15)

### ✨ Adicionado (Added)
- **Módulo de Vendas Completo**: Sistema de CRUD para vendas (registrar, remover, visualizar histórico)
- **Exportação de Relatório Financeiro**: Novo recurso para gerar relatórios em TXT ou PDF com análises detalhadas
- **Relatórios Financeiros Completos**: Inclui ganhos totais, produtos vendidos por categoria, estoque baixo (< 3 unidades), top produtos mais vendidos e receita por categoria
- **Validação de Estoque**: Verificação automática de disponibilidade antes de concretizar vendas
- **Persistência de Dados de Vendas**: Arquivo `sales.json` para salvar histórico de transações
- **10 Opções no Menu**: Novas opções Registrar Venda (7), Remover Venda (8) e Ver Vendas (9)
- **Tratamento de Exceções Centralizado**: Exceções customizadas para vendas (`SaleNotFoundException`, `SaleInformationInvalidException`)
- **Interface de Menu Expandida**: De 6 para 10 opções com navegação aprimorada
- **Exportação Multiplataforma**: Suporte a 5 plataformas (Windows x64/x86, Linux x64, macOS x64/ARM64)

### 🔄 Alterado (Changed)
- **README.md**: Atualização completa com documentação profissional, releases disponíveis e versionamento v2.0
- **QUICK-START.md**: Instruções atualizadas para compilação e releases v2.0
- **RELEASES.md**: Detalhes técnicos atualizados para a nova versão
- **SETUP-SUMMARY.md**: Resumo de setup com foco na v2.0 final
- **.csproj**: Atualização da versão do assembly para 2.0.0
- **Versionamento**: Migração de v1.0 para v2.0 em todos os arquivos de configuração

### 🐛 Corrigido (Fixed)
- **README.md**: Remoção do roadmap futuro (projeto finalizado)
- **Version tags**: Tag v2.0 preparada para release no GitHub
- **Build scripts**: Atualização dos scripts BUILD-RELEASES e COMPRESS-RELEASES para v2.0
- **MenuController.cs**: Correções de validação e estabilidade na navegação
- **FinancialReportExporter.cs**: Otimizações na formatação de PDF e TXT
- **InventoryReportExporter.cs**: Correções em relatórios de estoque

### 📦 Removido (Removed)
- **Roadmap Futuro**: Removido todas as issues planejadas (Interface gráfica, API REST, Banco de dados SQL, etc.) - este é o projeto final e definitivo

---

## v1.0.0 (2026-10-01)

### ✨ Adicionado
- Controle de estoque básico
- Gerenciamento de produtos (CRUD)
- Relatórios de inventário em TXT e PDF
- Persistência em JSON
- Menu de 6 opções

### 🔄 Alterado
- Estrutura inicial do projeto

---

## 📦 Releases Disponíveis

| Plataforma | Download | Status |
|------------|----------|--------|
| **Windows x64** | [StockControl-v2.0-Windows-x64.zip](#) | ✅ Pronto |
| **Windows x86** | [StockControl-v2.0-Windows-x86.zip](#) | ✅ Pronto |
| **Linux x64** | [StockControl-v2.0-Linux-x64.zip](#) | ✅ Pronto |
| **macOS x64** | [StockControl-v2.0-macOS-x64.zip](#) | ✅ Pronto |
| **macOS ARM64** | [StockControl-v2.0-macOS-ARM64.zip](#) | ✅ Pronto |

**Tamanho**: ~200-300 MB por plataforma (runtime incluído)