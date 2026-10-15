<div align="center">

# 📦 Stock Control

**Sistema de controle de estoque e vendas em console, desenvolvido em C# com .NET 10.**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![PDFsharp](https://img.shields.io/badge/PDFsharp-6.2.4-C0392B?style=for-the-badge&logo=adobeacrobatreader&logoColor=white)](https://www.pdfsharp.com/)

</div>

---

## 📋 Visão Geral

**Stock Control** é uma aplicação de propósito geral para pequenos e médios negócios que necessitam controlar inventário e registrar transações de vendas. O sistema oferece gerenciamento completo de produtos, cadastro de vendas, relatórios analíticos e exportação em múltiplos formatos (TXT e PDF).

### Principais características

- ✅ **Gestão de Produtos**: Criar, atualizar, remover e consultar produtos com categorização
- ✅ **Controle de Estoque**: Visualização em tempo real das quantidades e valores
- ✅ **Registro de Vendas**: Cadastro de transações com múltiplos itens por venda
- ✅ **Relatórios Avançados**: Exportação de dados em TXT ou PDF com análises consolidadas
- ✅ **Alertas de Reabastecimento**: Identificação automática de produtos com baixo estoque
- ✅ **Persistência Segura**: Arquivos JSON com validação e backup automático

---

## 🏗️ Arquitetura

```
Stock Control
├── Controllers/       → Camada de apresentação (entrada/saída console)
├── Services/          → Camada de negócio (validação e regras)
├── Data/              → Camada de dados (memória + persistência)
├── Models/            → Entidades e DTOs
├── Utils/             → Utilitários (PDF, validação, categorias)
├── Exceptions/        → Definições de erros do domínio
├── Handlers/          → Tratamento centralizado de exceções
└── Program.cs         → Ponto de entrada
```

### Fluxo de dados

```
Entrada (Console)
       ↓
   Controllers
       ↓
   Services (Validação)
       ↓
   Data Layer (Stock/SalesRepository)
       ↓
   FileManager (JSON)
       ↓
%APPDATA%\StockControl\
```

---

## 🚀 Começar Rápido

### Requisitos

- **.NET 10 SDK** ou superior
- **Windows** (font setup para PDF) ou **Linux/macOS** (suporte experimental)
- **Terminal** com suporte a cores e redimensionamento (mínimo 103×22 caracteres)

### Instalação

1. Clone o repositório:
```bash
git clone <repository-url>
cd StockControl
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Compile o projeto:
```bash
dotnet build
```

4. Execute:
```bash
dotnet run
```

### Primeira execução

Na primeira execução, o sistema criará automaticamente:
- Pasta de dados: `%APPDATA%\StockControl\` (Windows)
- Arquivo de estoque: `inventory.json`
- Arquivo de vendas: `sales.json`

---

## 📖 Guia de Uso

### Menu Principal

| Opção | Funcionalidade | Descrição |
|-------|---|---|
| **1** | Add Product | Cadastrar novo produto (nome, preço, quantidade, categoria) |
| **2** | Remove Product | Remover produto por ID |
| **3** | Update Product | Atualizar dados do produto (campos opcionais) |
| **4** | View Products | Listar todos os produtos (paginado) |
| **5** | View Inventory | Consultar estoque detalhado por produto |
| **6** | Export Inventory Report | Gerar relatório de estoque em TXT ou PDF |
| **7** | Register Sale | Registrar uma nova venda com múltiplos itens |
| **8** | Remove Sale | Remover venda registrada por ID |
| **9** | View Sales | Consultar histórico de vendas (paginado) |
| **10** | Financial Report | Gerar relatório financeiro em TXT ou PDF |
| **0** | Exit | Encerrar e salvar dados |

### Categorias de Produto

O sistema inclui 11 categorias padrão:

1. Electronics
2. Clothing
3. Food
4. Furniture
5. Books
6. Toys
7. Sports
8. Beauty
9. Automotive
10. Health
11. Other

### Relatórios

#### Inventory Report (Estoque)
- Quantidade de tipos de produto
- Unidades totais em estoque
- Valor total do inventário
- Resumo por categoria
- Detalhamento completo de cada produto

#### Financial Report (Vendas)
- Total de vendas realizadas
- Receita total
- Unidades vendidas
- Ticket médio de venda
- Primeiro e último registro de venda
- **Produtos para reabastecer** (< 3 unidades)
- **Top 5 produtos mais vendidos**
- **Receita por categoria**
- Lista completa de todas as transações

---

## 🏢 Estrutura do Projeto

### Controllers

#### `MenuController.cs`
Apresenta o menu principal, valida tamanho da tela e orquestra chamadas aos demais controllers.

#### `ProductController.cs`
Interface de CRUD para produtos e inventário:
- `AddProduct()` → Cadastro com validações
- `RemoveProduct()` → Remove por GUID
- `UpdateProduct()` → Atualização parcial
- `ViewProducts()` → Listagem paginada
- `ViewInventory()` → Visão completa do estoque
- `ExportInventoryReport()` → Exportação em TXT/PDF

#### `SaleController.cs`
Gerenciamento de transações de vendas:
- `AddSale()` → Fluxo de registro com data, produtos e quantidades
- `RemoveSale()` → Remove venda por GUID
- `ViewSales()` → Consulta de histórico (paginada)
- `ExportFinancialReport()` → Exportação de relatório financeiro

### Services

#### `ProductService.cs`
Regras de negócio para produtos:
- Validação de nome único (case-insensitive)
- Preço e quantidade não-negativos
- Categoria válida
- Geração de `InventoryReport`

#### `SaleService.cs`
Orquestração de vendas:
- Verificação de disponibilidade de produtos
- Cálculo de valor total
- Atualização de quantidades em estoque
- Geração de `FinancialReport`

#### `MenuService.cs`
Despachante central de opções do menu.

### Data Layer

#### `Stock.cs`
Repositório em memória de produtos:
- Carrega `inventory.json` no startup
- Operações CRUD
- `GetProductsSnapshot()` para consultas seguras

#### `Sales.cs` (`SalesRepository`)
Repositório em memória de vendas:
- Carrega `sales.json` no startup
- Operações CRUD
- `GetSalesSnapshot()` para consultas seguras

### Models

#### `Product` (struct)
```csharp
record struct Product
{
    Guid Id              // Auto-gerado
    string Name          // Obrigatório, único
    decimal Price        // ≥ 0
    uint Quantity        // Quantidade em estoque
    EProductCategory Category
}
```

#### `Sale` e `SaleItem` (structs)
```csharp
record struct Sale
{
    Guid Id
    DateOnly Date
    List<SaleItem> Items
    decimal TotalValue
}

record struct SaleItem
{
    Product Product
    uint Quantity        // Quantidade vendida
    decimal Value        // Valor total do item
}
```

#### `InventoryReport`
Agregação de dados de estoque com totalizações por categoria.

#### `FinancialReport`
Agregação de dados de vendas com análises de receita, top produtos e alertas de reabastecimento.

### Utils

#### `InventoryReportExporter.cs`
Exportação de relatório de estoque em TXT ou PDF com:
- Quebra automática de linhas em PDF
- Formatação em português (pt-BR)
- Arquivo temporário + Move atômico

#### `FinancialReportExporter.cs`
Exportação de relatório financeiro em TXT ou PDF com mesmo padrão de segurança e formatação.

#### `FileManager.cs`
Gerenciamento de persistência JSON:
- `SaveToFile()` → Escrita com backup temporário
- `ReadFromFile()` → Leitura com tratamento de erros
- Conversor customizado para `DateOnly`

#### `PathValidator.cs`
Validação de caminhos de arquivo e criação de diretórios.

#### `Background.cs`
Utilitários de renderização de menu (cores, bordas ASCII).

#### `ConsoleSize.cs`
Verificação e ajuste de tamanho de terminal (mínimo 103×22).

#### `Exit.cs`
Encerramento seguro com persistência de dados.

#### `EProductCategory.cs`
Enumeração de categorias de produto (1-11).

#### `PdfFontSetup.cs`
Configuração de fontes Windows para PDFsharp.

---

## 💾 Persistência de Dados

### Localização

- **Windows**: `%APPDATA%\StockControl\`
- **Linux/macOS**: `~/.local/share/StockControl/` (experimental)

### Arquivos

- `inventory.json` → Lista de produtos
- `sales.json` → Lista de vendas

### Segurança

- Escrita via arquivo temporário + `File.Move()` atômico
- Validação de JSON na leitura
- Conversão segura de `DateOnly`
- Se arquivo estiver corrompido, sistema aborta writes para não sobrescrever

### Timing

Dados são salvos **apenas** no encerramento do programa (opção 0 ou fechamento do console). Operações em memória são imediatas, mas não persistem até a saída.

---

## 🔒 Tratamento de Erros

O projeto implementa exceções customizadas do domínio:

| Exceção | Contexto |
|---------|----------|
| `ProductInformationInvalidException` | Validação de produto |
| `ProductNotFoundException` | Produto não encontrado |
| `SaleNotFoundException` | Venda não encontrada |
| `SaleInformationInvalidException` | Validação de venda |
| `InvalidPathException` | Caminho de arquivo inválido |
| `FileOperationException` | Erro ao ler/escrever arquivo |
| `OptionNotFoundException` | Opção de menu inválida |
| `ErrorOperationException` | Erro genérico de operação |

Todas são tratadas centralmente por `HandlerException` que exibe mensagem ao usuário e aguarda confirmação.

---

## 🔧 Desenvolvimento

### Compilação

```bash
dotnet build
```

### Execução com Debug

```bash
dotnet run --configuration Debug
```

### Publicação (Release)

```bash
dotnet publish -c Release -r win-x64 --self-contained
```

### Dependências

- **PDFsharp 6.2.4** → Geração de relatórios em PDF

---

## 📋 Checklist de Funcionalidades

### Produtos
- ✅ Criar produto
- ✅ Remover produto
- ✅ Atualizar produto
- ✅ Listar produtos
- ✅ Categorizar produto
- ✅ Validação de nome único

### Inventário
- ✅ Visualizar estoque
- ✅ Controlar quantidade
- ✅ Calcular valor total
- ✅ Resumo por categoria
- ✅ Exportar relatório (TXT/PDF)

### Vendas
- ✅ Registrar venda com múltiplos itens
- ✅ Remover venda
- ✅ Consultar histórico
- ✅ Validar disponibilidade de estoque
- ✅ Atualizar quantidades automaticamente

### Relatórios
- ✅ Relatório de estoque detalhado
- ✅ Análise financeira de vendas
- ✅ Top 5 produtos mais vendidos
- ✅ Receita por categoria
- ✅ Alertas de reabastecimento
- ✅ Exportação em TXT e PDF

### UX/Sistema
- ✅ Menu navegável
- ✅ Paginação em listagens
- ✅ Validação de entrada
- ✅ Tratamento de erro centralizado
- ✅ Verificação de tamanho de terminal
- ✅ Persistência segura

---

## 🤝 Contribuição

Para contribuir ao projeto:

1. Faça fork do repositório
2. Crie uma branch para sua feature (`git checkout -b feature/NovaFuncionalidade`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Push para a branch (`git push origin feature/NovaFuncionalidade`)
5. Abra um Pull Request

### Padrões de código

- Siga as convenções C# (PascalCase para classes, camelCase para variáveis)
- Utilize `record` para modelos imutáveis
- Mantenha a separação entre camadas (Controllers → Services → Data)
- Adicione tratamento de exceção apropriado
- Documente métodos públicos com XML comments quando necessário

---

## 📝 Licença

Este projeto está sob licença MIT. Veja o arquivo `LICENSE` para detalhes.

---

## 📞 Suporte

Para relatórios de bugs, sugestões ou dúvidas:

- Abra uma issue no repositório
- Descreva o problema com precisão
- Inclua passos para reproduzir (se aplicável)
- Forneça versão do .NET e SO

---

## 🎯 Roadmap Futuro

- [ ] Interface gráfica (WPF/MAUI)
- [ ] API REST para integração
- [ ] Banco de dados SQL
- [ ] Autenticação de usuários
- [ ] Dashboard com gráficos
- [ ] Importação em lote (CSV/Excel)
- [ ] Histórico de alterações (audit log)
- [ ] Backup automático em nuvem

---

## 📊 Versão

**Stock Control v1.0.0**
- Lançamento: Outubro 2026
- Framework: .NET 10
- Linguagem: C#

---

Desenvolvido com ❤️ para gestão eficiente de estoque e vendas.
