# 📦 StockControl - Sistema de Controle de Estoque

<div align="center">

![Version](https://img.shields.io/badge/version-1.0.0-blue)
![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![License](https://img.shields.io/badge/license-MIT-green)

**Um sistema completo de controle de estoque em C# com interface de console**

[🚀 Quick Start](#-quick-start) • [📋 Requisitos](#-requisitos) • [📦 Releases](#-releases) • [📝 Documentação](#-documentação)

</div>

---

## ✨ Funcionalidades

### v1.0 - Controle de Estoque
- ✅ **Gerenciamento de Produtos**
  - Criar novos produtos
  - Visualizar lista de produtos
  - Atualizar informações
  - Deletar produtos

- ✅ **Controle de Estoque**
  - Adicionar quantidade ao estoque
  - Remover quantidade do estoque
  - Visualizar estoque atual
  - Validações automáticas

- ✅ **Relatórios**
  - Exportar inventário em PDF
  - Exportar inventário em TXT
  - Layout responsivo por tamanho de console

- ✅ **Persistência de Dados**
  - Salvamento automático em JSON
  - Carregamento automático ao iniciar

### v2.0 (Em Desenvolvimento)
- 🔄 Sistema de Vendas
- 📊 Relatórios de Vendas
- 💰 Cálculo de Lucro

---

## 🚀 Quick Start

### Pré-requisitos
- .NET 10 SDK ou Runtime
- 200-300 MB de espaço em disco (para versão self-contained)

### Opção 1: Usar Executável (Recomendado)

1. Baixe a versão correspondente ao seu sistema no [GitHub Releases](https://github.com/seu-usuario/StockControl/releases)
2. Extraia o arquivo `.zip`
3. Execute o `StockControl.exe` (Windows) ou `StockControl` (Linux/macOS)

### Opção 2: Compilar do Código-Fonte

```bash
git clone https://github.com/seu-usuario/StockControl.git
cd StockControl
dotnet run
```

### Opção 3: Gerar Sua Própria Release

```bash
cd scripts
.\BUILD-RELEASES.bat          # Windows
# ou
bash build-releases.sh         # Linux/macOS
```

Veja [scripts/README.md](scripts/README.md) para mais detalhes.

---

## 📋 Requisitos

### Para Executar
- Windows, Linux ou macOS
- 100 MB de RAM
- Sem requisitos adicionais (runtime incluído)

### Para Compilar
- .NET 10 SDK
- Git (opcional)
- ~5-10 GB de espaço em disco para compilar todas as plataformas

---

## 📦 Releases

### v1.0 Disponível Para

| Plataforma | Download | Status |
|-----------|----------|--------|
| **Windows x64** | [StockControl-v1.0-Windows-x64.zip](#) | ✅ Pronto |
| **Windows x86** | [StockControl-v1.0-Windows-x86.zip](#) | ✅ Pronto |
| **Linux x64** | [StockControl-v1.0-Linux-x64.zip](#) | ✅ Pronto |
| **macOS x64** | [StockControl-v1.0-macOS-x64.zip](#) | ✅ Pronto |
| **macOS ARM64** | [StockControl-v1.0-macOS-ARM64.zip](#) | ✅ Pronto |

**Tamanho**: ~200-300 MB por plataforma (runtime incluído)

---

## 📁 Estrutura do Projeto

```
StockControl/
├── Controllers/           # Controladores da aplicação
├── Models/               # Modelos de dados
├── Services/             # Lógica de negócio
├── Data/                 # Persistência de dados
├── Utils/                # Utilitários
├── Exceptions/           # Tratamento de exceções
├── Handlers/             # Tratadores
├── scripts/              # Scripts de build e deploy
│   ├── BUILD-RELEASES.bat
│   ├── COMPRESS-RELEASES.bat
│   ├── BUILD-LAUNCHER.vbs
│   └── README.md
├── Program.cs            # Entrada da aplicação
├── StockControl.csproj   # Configuração do projeto
├── QUICK-START.md        # Guia rápido
├── RELEASES.md           # Detalhes das releases
└── README.md             # Este arquivo
```

---

## 🏃 Como Usar

### Menu Principal

```
╔════════════════════════════════════════╗
║       SISTEMA DE CONTROLE DE ESTOQUE   ║
╠════════════════════════════════════════╣
║  1. Gerenciar Produtos                 ║
║  2. Gerenciar Estoque                  ║
║  3. Relatórios                         ║
║  0. Sair                               ║
╚════════════════════════════════════════╝
```

### Exemplo: Adicionar Produto

```
1. Selecione "1. Gerenciar Produtos"
2. Escolha "1. Criar Novo Produto"
3. Insira o nome do produto
4. Insira a categoria
5. Insira o preço
6. Produto criado com sucesso!
```

---

## 🛠️ Compilar Releases

### Método 1: Clique Duplo (Mais Fácil)

1. Abra a pasta `scripts/`
2. Clique duplo em `BUILD-RELEASES.bat`
3. Aguarde terminar

### Método 2: Terminal

```powershell
cd scripts
.\BUILD-RELEASES.bat
.\COMPRESS-RELEASES.bat
```

Para mais detalhes, veja:
- [scripts/README.md](scripts/README.md) - Documentação completa dos scripts
- [scripts/SHORTCUT-GUIDE.md](scripts/SHORTCUT-GUIDE.md) - Como criar atalho com ícone
- [QUICK-START.md](QUICK-START.md) - Guia rápido em português

---

## 📝 Documentação

| Arquivo | Conteúdo |
|---------|----------|
| **[QUICK-START.md](QUICK-START.md)** | Guia rápido em português 🇧🇷 |
| **[RELEASES.md](RELEASES.md)** | Informações detalhadas sobre releases |
| **[scripts/README.md](scripts/README.md)** | Como usar os scripts de build |
| **[scripts/SHORTCUT-GUIDE.md](scripts/SHORTCUT-GUIDE.md)** | Como criar atalho com ícone |

---

## 🐛 Problemas Comuns

### Erro: "dotnet: command not found"
Instale o .NET 10 Runtime: https://dotnet.microsoft.com/download

### Erro ao carregar dados
Verifique as permissões da pasta do projeto.

### Interface com problemas de cores
Abra o terminal em modo compatibilidade (Windows).

---

## 🔄 Versionamento

- **v1.0** - Controle de Estoque (Atual)
- **v2.0** - Com Sistema de Vendas (Em desenvolvimento)
- **v3.0+** - Novas funcionalidades

---

## 📊 Tecnologias

- **Linguagem**: C# 13
- **.NET**: 10.0
- **UI**: Console (Windows, Linux, macOS)
- **Persistência**: JSON
- **Exportação**: PDF (PDFSharp), TXT

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para detalhes.

---

## 👥 Contribuir

Contribuições são bem-vindas! Para contribuir:

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

## 💬 Suporte

Se tiver dúvidas ou encontrar problemas:

1. Verifique a [documentação](QUICK-START.md)
2. Procure nos [Issues](https://github.com/seu-usuario/StockControl/issues)
3. Crie um novo Issue com detalhes do problema

---

## 🎉 Agradecimentos

Desenvolvido com ❤️ em C# e .NET

---

<div align="center">

**[⬆ Voltar ao Topo](#-stockcontrol---sistema-de-controle-de-estoque)**

Made with ❤️ by StockControl Team

</div>

