# Stock Control

Aplicação de **console** em **C# / .NET 10** para controle de estoque e registro de vendas. A interface é textual (menus desenhados no terminal). Os dados são persistidos em JSON na pasta de dados do usuário.

Não há banco de dados, API HTTP nem interface gráfica. O fluxo é: **Controller (entrada no console) → Service (regras) → Data (memória + arquivo no encerramento)**.

---

## Visão geral da arquitetura

```
Program
  └── MenuController  ──►  MenuService  ──►  ProductController / SaleController / Exit
                                              │                    │
                                              ▼                    ▼
                                         ProductService       SaleService
                                              │                    │
                                              ▼                    ▼
                                            Stock            SalesRepository
                                              └──────── FileManager ────────┘
                                                         (inventory.json / sales.json)
```

| Camada | Pasta | Papel |
| --- | --- | --- |
| Entrada | `Program.cs` | Sobe o menu e captura falha fatal de inicialização. |
| Controllers | `Controllers/` | Telas, leitura de teclado e orquestração da UX. |
| Services | `Services/` | Validação e regras de negócio. |
| Data | `Data/` | Listas em memória; carga no start, gravação na saída. |
| Models | `Models/` | Entidades e DTOs. |
| Utils | `Utils/` | Console, arquivos, PDF, categorias, validação de venda. |
| Exceptions / Handlers | `Exceptions/`, `Handlers/` | Erros de domínio e exibição no terminal. |

Persistência: `%APPDATA%\StockControl\` no Windows (`inventory.json` e `sales.json`). A gravação ocorre **ao sair** (`Exit.ExitProgram`), não a cada operação.

---

## Funcionalidades atuais

| Menu | Status |
| --- | --- |
| 1. Add Product | Implementado |
| 2. Remove Product | Implementado (por GUID) |
| 3. Update Product | Implementado (campos opcionais) |
| 4. View Products | Implementado (nome e preço, paginado) |
| 6. View Inventory | Implementado (detalhe completo, paginado) |
| 7. Export Inventory Report | Implementado (TXT ou PDF) |
| 8. Register Sale | Implementado no `SaleController` / `SaleService` |
| 9. Remove Sale | Não implementado |
| 10. View Sales | Em implementação (listagem das vendas em memória/arquivo) |
| 11. Financial Report | Não implementado |
| 0. Exit | Implementado (salva estoque e vendas) |

A opção **5** aparece no `MenuService` (registro de venda), mas **não aparece no menu visual**. O rótulo “Register Sale” está na opção **8**, que no serviço ainda não dispara o cadastro. A listagem de vendas alinha o despacho do menu com as opções exibidas.

---

## Arquivos na raiz

### `Program.cs`

Ponto de entrada. Chama `MenuController.Menu()` dentro de `try/catch`. Qualquer exceção vira `InitializeSystemException` e passa pelo `HandlerException`.

### `StockControl.csproj`

Projeto executável (`OutputType` = Exe), `net10.0`, usings implícitos e nullable habilitados. Dependência: **PDFsharp 6.2.4** (relatório de estoque em PDF).

### `README.md`

Este documento: mapa do repositório, responsabilidade de cada arquivo e notas de operação.

### `.gitignore`

Ignora `bin/`, `obj/` e `/packages/`.

### `.vscode/settings.json`

Arquivo vazio; não define comportamento do editor.

---

## Controllers

### `Controllers/MenuController.cs`

Loop infinito do menu principal. Garante tamanho mínimo do terminal, aplica cores (`DarkCyan` / `White`), desenha borda e imprime as opções. Lê a linha digitada e envia para `MenuService.HandleMenu`. Trata `ArgumentOutOfRangeException` (cursor fora da janela).

### `Controllers/ProductController.cs`

Telas de produto e inventário:

- **AddProduct** — nome, preço, quantidade e categoria (`EProductCategory`); monta `ProductDto` e chama o service.
- **RemoveProduct** / **UpdateProduct** — identificam o item por `Guid`; update permite campo em branco para manter o valor atual.
- **ViewProducts** — lista paginada (nome e preço).
- **ViewInventory** — lista paginada com id, nome, quantidade, preço e categoria.
- **ExportInventoryReport** — mostra totais, pede formato (1 TXT / 2 PDF) e caminho de destino.
- Helpers privados: parse opcional de preço/quantidade/categoria, tamanho de página e largura da borda.

### `Controllers/SaleController.cs`

Fluxo de **registro de venda**:

1. Data (`AddSaleDate`: ano, mês, dia com validação).
2. Editor: adicionar produtos (nome + quantidade) ou salvar.
3. Monta `SaleDto` (`Date` + dicionário nome → quantidade) e chama `SaleService.AddSale`.

Ainda não havia tela de consulta das vendas gravadas.

---

## Services

### `Services/MenuService.cs`

Converte a opção do menu em `uint` (entrada inválida vira `10`) e faz o `switch` para controllers. Opção `0` chama `Exit.ExitProgram`. Opção desconhecida vira `OptionNotFoundException`.

### `Services/ProductService.cs`

Regras de produto: nome obrigatório e único (case-insensitive), preço ≥ 0, quantidade e categoria válidas. Encaminha CRUD para `Stock`. Monta `InventoryReport` (totais gerais e por categoria).

### `Services/SaleService.cs`

Registra venda: `VerifyProductsSale` (produto existe e nome não está duplicado no estoque), baixa quantidade, calcula valor dos itens e total, persiste `Sale` em `SalesRepository` e atualiza as quantidades em `Stock`. Falhas viram `ErrorOperationException`.

---

## Data

### `Data/Stock.cs`

Repositório em memória de `Product`. Construtor estático lê `inventory` via `FileManager`. Se a leitura falhar, `IsInitialized` permanece `false` e as operações recusam gravar (para não sobrescrever arquivo corrompido na saída). Expõe add/remove/update, `GetProducts` (lista viva) e `GetProductsSnapshot` (cópia para consulta).

### `Data/Sales.cs` (`SalesRepository`)

Mesmo padrão para `Sale`, arquivo `sales`. Métodos: `GetSales`, `AddSale`, `RemoveSale`, `UpdateSale`. `UpdateSale` ainda usa `ProductNotFoundException` em venda inexistente (mensagem de produto). A listagem de vendas passa a usar um snapshot, no mesmo espírito do estoque.

---

## Models

### `Models/Product.cs`

`record struct`: `Id` (`Guid`, gerado na criação, serializado com `[JsonInclude]`), `Name`, `Price`, `Quantity`, `Category`.

### `Models/ProductDTO.cs` (`ProductDto`)

Entrada de formulário com campos anuláveis (create e update parcial).

### `Models/Sale.cs`

- `SaleItem`: produto, quantidade vendida e valor do item.
- `Sale`: `Id`, `Date` (`DateOnly`), `Items`, `TotalValue`.

O item guarda o `Product` no estado **após** a baixa de estoque (quantidade restante no cadastro), além da quantidade vendida em `SaleItem.Quantity`.

### `Models/SaleDto.cs`

Entrada da venda: data + `Dictionary<string, uint>` (nome → quantidade). Nomes repetidos no editor são somados.

### `Models/InventoryReport.cs`

- `CategoryInventorySummary` — totais por categoria.
- `InventoryReport` — produtos, unidades, valor total e resumos; `ProductCount` derivado da lista.

---

## Utils

### `Utils/FileManager.cs`

JSON em `%APPDATA%\StockControl`. `SaveToFile` escreve arquivo temporário e faz `Move` atômico. `ReadFromFile` devolve lista vazia se o arquivo ainda não existe; `null` se o caminho/leitura falhar. Inclui conversor de `DateOnly` (`yyyy-MM-dd`).

### `Utils/PathValidator.cs`

Rejeita caminho vazio ou com caracteres inválidos; cria o diretório pai se não existir.

### `Utils/VerifyProductsSale.cs`

Antes de fechar a venda: cada nome deve existir no estoque; se houver dois produtos com o mesmo nome, aborta e pede para renomear.

### `Utils/Background.cs`

`MenuColor` define fundo/texto. `MenuBorder` desenha o retângulo ASCII usado em todas as telas.

### `Utils/ConsoleSize.cs`

Mínimo **103×22**. No Windows tenta redimensionar buffer e janela; fora disso pede ajuste manual. `Q` abandona.

### `Utils/Exit.cs`

Encerra o programa. Se o estoque não inicializou, sai **sem gravar**. Caso contrário salva `inventory` e `sales`. Se o save falhar, permanece no aplicativo.

### `Utils/EProductCategory.cs`

Categorias 1–11: Electronics, Clothing, Food, Furniture, Books, Toys, Sports, Beauty, Automotive, Health, Other.

### `Utils/InventoryReportExporter.cs`

Exporta `InventoryReport` em `.txt` (UTF-8) ou `.pdf` (PDFsharp, A4, Arial). Textos do relatório em pt-BR. Quebra de linha no PDF para palavras longas. Escrita via arquivo temporário + `Move`.

---

## Exceptions e Handlers

### `Exceptions/Exceptions.cs`

Exceções de domínio (todas herdam `Exception`):

| Tipo | Uso típico |
| --- | --- |
| `ProductInformationInvalidException` | Dados de produto inválidos ou nome duplicado |
| `ProductNotFoundException` | Produto inexistente (estoque ou venda) |
| `OptionNotFoundException` | Opção de menu/formato inválida |
| `ErrorOperationException` | Falha genérica de operação |
| `InitializeStockException` / `InitializeSalesException` / `InitializeSystemException` | Carga inicial |
| `ExitException` | Falha ao encerrar |
| `FileOperationException` / `InvalidPathException` | I/O e caminhos |
| `SaleNotFoundException` | Venda não encontrada no repositório |

### `Handlers/HandlerException.cs`

Mapeia o tipo para um prefixo curto, pinta a tela de vermelho, mostra a mensagem e espera uma tecla. Com `returnToMenu: false` (falha na carga de arquivo), só escreve em `Console.Error`.

---

## Dados e ciclo de vida

1. **Start:** `Stock` e `SalesRepository` tentam ler JSON.
2. **Uso:** alterações só em memória.
3. **Exit:** `FileManager.SaveToFile` grava as duas coleções.

Implicação: fechar o terminal com Ctrl+C **não** persiste. Relatórios PDF/TXT são arquivos escolhidos pelo usuário, independentes do JSON interno.

---

## Observações técnicas (análise)

- Camadas Controller / Service / Data estão consistentes; UI não acessa arquivo direto, exceto exportação de relatório.
- `Product` e `Sale` são `record struct`: cópias por valor. Por isso o estoque usa índice + reatribuição na lista, não mutação compartilhada por referência.
- `GetProducts()` devolve a lista interna; consultas de tela usam snapshot para não expor mutação acidental.
- Venda não valida estoque insuficiente em `VerifyProductsSale` (só existência/duplicidade); a quantidade é checada em `SaleService`.
- `SaleService` não persiste JSON imediatamente; depende do Exit.
- Numeração do menu visual e do `switch` do `MenuService` estavam desalinhadas (venda cadastrada no case 5, rótulo no 8; opção 10 caía no `default`).
- Itens ainda não feitos: remover venda, relatório financeiro, e persistência incremental (opcional).

---

## Como executar

Na raiz do repositório:

```bash
dotnet run
```

Terminal com pelo menos 103 colunas e 22 linhas. No Windows o próprio programa tenta redimensionar a janela.
