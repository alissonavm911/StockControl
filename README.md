<div align="center">

# 📦 StockControl

**Sistema de controle de estoque e vendas em console, desenvolvido em C# com .NET.**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![PDFsharp](https://img.shields.io/badge/PDFsharp-6.2.4-C0392B?style=for-the-badge&logo=adobeacrobatreader&logoColor=white)](https://www.pdfsharp.com/)
[![Aplicação](https://img.shields.io/badge/Aplica%C3%A7%C3%A3o-Console-4D4D4D?style=for-the-badge&logo=windowsterminal&logoColor=white)](#-como-executar)

[![Curso](https://img.shields.io/badge/Curso-Fundamentos%20de%20C%23-7B2CBF?style=for-the-badge)](https://balta.io)
[![balta.io](https://img.shields.io/badge/Feito%20com%20base%20no%20curso-balta.io-7B2CBF?style=for-the-badge)](https://balta.io)
[![Status](https://img.shields.io/badge/Status-Em%20desenvolvimento-F4B400?style=for-the-badge)](#-roadmap)

</div>

---

## 📖 Sobre o projeto

O **StockControl** é uma aplicação de console para gerenciar o estoque de uma loja: cadastro, edição, remoção e consulta de produtos, com visão do inventário por categoria e **exportação de relatórios em TXT e PDF**.

O projeto foi desenvolvido **com base no curso _Fundamentos de C#_ da [balta.io](https://balta.io)**. A partir dos conceitos vistos no curso, a proposta aqui foi ir além do exercício básico: organizar o código em camadas, tratar erros de forma centralizada, persistir os dados em disco e gerar relatórios.

> 🎓 Este é um projeto de estudo e não possui vínculo oficial com a balta.io.

## ✨ Funcionalidades

| Menu | Funcionalidade | Status |
| :--: | -------------- | :----: |
| 1 | **Adicionar produto** (nome, preço, quantidade e categoria) | ✅ |
| 2 | **Remover produto** pelo ID (GUID) | ✅ |
| 3 | **Atualizar produto** — campos em branco mantêm o valor atual | ✅ |
| 4 | **Listar produtos** com paginação (nome e preço) | ✅ |
| 5 | Registrar venda | 🚧 Não implementado |
| 6 | **Ver inventário** detalhado com paginação (ID, nome, quantidade, preço e categoria) | ✅ |
| 7 | **Exportar relatório de estoque** em **TXT** ou **PDF** | ✅ |
| 8 | Ver vendas | 🚧 Não implementado |
| 9 | Relatório financeiro | 🚧 Não implementado |
| 0 | **Sair** salvando os dados | ✅ |

### Destaques

- **Interface de console colorida**, com bordas e posicionamento de cursor, exigindo um terminal de pelo menos **103 colunas × 22 linhas** (no Windows o redimensionamento é automático).
- **Persistência em JSON** na pasta de dados do usuário, com gravação atômica (arquivo temporário + `File.Move`) para evitar corromper o inventário.
- **Relatório de estoque** com resumo geral, totais por categoria e detalhes de cada produto, formatado em `pt-BR`.
- **Tratamento de erros centralizado**, com exceções próprias e uma tela de erro padronizada que devolve o usuário ao menu.
- **11 categorias** de produto: Electronics, Clothing, Food, Furniture, Books, Toys, Sports, Beauty, Automotive, Health e Other.

## 🛠️ Tecnologias

- [C#](https://learn.microsoft.com/dotnet/csharp/) com [.NET 10](https://dotnet.microsoft.com/) (`net10.0`)
- `System.Text.Json` para serialização do inventário
- [PDFsharp 6.2.4](https://www.pdfsharp.com/) para geração dos relatórios em PDF
- Recursos da linguagem: `record`, `record struct`, `enum`, *nullable reference types*, `required` members, LINQ e `ImplicitUsings`

## 🗂️ Estrutura do projeto

```text
StockControl/
├── Controllers/
│   ├── MenuController.cs       # Desenha o menu principal
│   └── ProductController.cs    # Telas de produto, inventário e exportação
├── Services/
│   ├── MenuService.cs          # Direciona a opção escolhida no menu
│   └── ProductService.cs       # Validações e regras de negócio
├── Data/
│   └── Stock.cs                # Estoque em memória + carga inicial do JSON
├── Models/
│   ├── Product.cs              # Entidade Product (Id GUID gerado automaticamente)
│   ├── ProductDTO.cs           # Dados de entrada (campos opcionais)
│   └── InventoryReport.cs      # Relatório e resumo por categoria
├── Exceptions/
│   └── Exceptions.cs           # Exceções personalizadas do domínio
├── Handlers/
│   └── HandlerException.cs     # Mensagens de erro padronizadas
├── Utils/
│   ├── Background.cs           # Cores e bordas do console
│   ├── ConsoleSize.cs          # Garante o tamanho mínimo do terminal
│   ├── Exit.cs                 # Encerramento e salvamento dos dados
│   ├── FileManager.cs          # Leitura e escrita do JSON
│   ├── InventoryReportExporter.cs  # Exportação TXT/PDF
│   ├── PathValidator.cs        # Validação de caminhos
│   └── ProductCategory.cs      # Enum de categorias
├── Program.cs                  # Ponto de entrada
├── StockControl.csproj
└── StockControl.sln
```

A organização segue uma separação simples em camadas: **Controllers** cuidam da interação com o usuário, **Services** validam e aplicam as regras, e **Data** mantém o estado do estoque.

## 🚀 Como executar

### Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download) instalado
- Um terminal com pelo menos **103 × 22** caracteres

### Passo a passo

```bash
# 1. Clone o repositório
git clone https://github.com/alissonavm911/StockControl.git

# 2. Entre na pasta do projeto
cd StockControl

# 3. Restaure as dependências
dotnet restore

# 4. Execute
dotnet run
```

Para gerar uma versão otimizada:

```bash
dotnet build -c Release
```

## 💾 Onde ficam os dados?

O inventário é salvo no arquivo `inventory.json`, dentro da pasta `StockControl` do diretório de dados do usuário (`Environment.SpecialFolder.ApplicationData`):

| Sistema | Caminho aproximado |
| ------- | ------------------ |
| Windows | `%APPDATA%\StockControl\inventory.json` |
| Linux | `~/.config/StockControl/inventory.json` |
| macOS | `~/.config/StockControl/inventory.json` |

> ⚠️ Os dados são gravados **somente ao sair pela opção `0` do menu**. Fechar a janela do terminal diretamente descarta as alterações feitas na sessão.

Se o arquivo existir, mas estiver inválido, o sistema **não o sobrescreve**: ele avisa que o inventário não pôde ser carregado e encerra sem salvar.

## 📄 Exemplo de relatório exportado

```text
RELATÓRIO DE ESTOQUE
Gerado em: 12/10/2026 09:35

RESUMO
Tipos de produto: 2
Unidades em estoque: 15
Valor total em estoque: R$ 1.250,00

TOTAIS POR CATEGORIA
Electronics: 1 produto(s), 5 unidade(s), R$ 1.000,00
...

PRODUTOS
ID: 3f2b8c1e-...
Nome: Teclado Mecânico
Categoria: Electronics
Quantidade: 5
Preço unitário: R$ 200,00
Valor em estoque: R$ 1.000,00
```

> Valores meramente ilustrativos.

## 🧠 Conceitos praticados

- Tipos, variáveis, operadores e estruturas de controle (`switch`, `while`, `for`, `foreach`)
- Enumeradores, `struct`, `record` e `record struct`
- Tratamento de exceções com classes personalizadas
- Coleções (`List<T>`), LINQ e *nullable types*
- Manipulação de arquivos e serialização JSON
- Organização de código com *namespaces* e classes estáticas

## 🗺️ Roadmap

- [x] Cadastro, edição, remoção e listagem de produtos
- [x] Inventário detalhado e paginado
- [x] Persistência em JSON
- [x] Exportação de relatório em TXT e PDF
- [ ] Registro de vendas
- [ ] Histórico de vendas
- [ ] Relatório financeiro

## 🤝 Contribuindo

Sugestões e melhorias são bem-vindas:

1. Faça um *fork* do projeto
2. Crie uma branch: `git checkout -b feature/minha-feature`
3. Faça o commit: `git commit -m "feat: minha feature"`
4. Envie a branch: `git push origin feature/minha-feature`
5. Abra um *Pull Request*

## 🙏 Agradecimentos

- À [balta.io](https://balta.io) e ao curso **Fundamentos de C#**, que serviu de base para este projeto
- À comunidade .NET pela documentação e pelo apoio

## 👤 Autor

Feito por **[@alissonavm911](https://github.com/alissonavm911)**.

<div align="center">

⭐ Se este projeto te ajudou de alguma forma, deixe uma estrela no repositório!

</div>
