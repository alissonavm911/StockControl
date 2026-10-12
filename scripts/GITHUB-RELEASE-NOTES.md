# 🎉 StockControl v1.0 - Release Notes

<div align="center">

![Version](https://img.shields.io/badge/version-1.0.0-blue?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-10.0-purple?style=for-the-badge)
![License](https://img.shields.io/badge/license-MIT-green?style=for-the-badge)

**Sistema completo de controle de estoque em C# - Multiplataforma**

[📥 Download](#-download) • [🚀 Como Usar](#-como-usar) • [❓ FAQ](#-faq)

</div>

---

## ✨ O que é StockControl?

**StockControl** é um sistema de gerenciamento de estoque desenvolvido em **C# com .NET 10** que oferece uma solução completa e intuitiva para controlar produtos e inventário.

### 🎯 Funcionalidades Principais

#### ✅ Gerenciamento de Produtos
- Criar novos produtos com nome, categoria e preço
- Visualizar lista completa de produtos
- Atualizar informações de produtos existentes
- Deletar produtos do sistema
- Validações automáticas

#### ✅ Controle de Estoque
- Adicionar quantidade ao estoque
- Remover quantidade (com validação)
- Visualizar estoque atual por produto
- Alertas de estoque baixo
- Histórico de movimentações

#### ✅ Relatórios Profissionais
- Exportar inventário em **PDF** formatado
- Exportar inventário em **TXT** simples
- Layout responsivo (se adapta ao tamanho do terminal)
- Data e hora nos relatórios

#### ✅ Persistência de Dados
- Salvamento automático em JSON
- Carregamento ao iniciar o programa
- Sem necessidade de banco de dados externo

---

## 📥 Download

### 🔍 Escolha seu Sistema Operacional

#### 🪟 Windows

**Windows 64-bit (Recomendado)**
- [StockControl-v1.0-Windows-x64.zip](#)
- Tamanho: ~250 MB
- Para: Windows 10/11 moderno

**Windows 32-bit (Compatibilidade)**
- [StockControl-v1.0-Windows-x86.zip](#)
- Tamanho: ~220 MB
- Para: Windows 7/8/XP antigo

#### 🐧 Linux

**Linux 64-bit**
- [StockControl-v1.0-Linux-x64.zip](#)
- Tamanho: ~200 MB
- Para: Ubuntu, Debian, Fedora, etc

#### 🍎 macOS

**macOS Intel (x64)**
- [StockControl-v1.0-macOS-x64.zip](#)
- Tamanho: ~240 MB
- Para: Mac Intel (2014+)

**macOS Apple Silicon (ARM64)**
- [StockControl-v1.0-macOS-ARM64.zip](#)
- Tamanho: ~230 MB
- Para: Mac M1/M2/M3 (2020+)

---

## 💾 Como Instalar e Executar

### 🪟 Windows

#### 1️⃣ Download e Extração
```
1. Baixe: StockControl-v1.0-Windows-x64.zip
2. Clique direito → Extrair tudo
3. Selecione a pasta de destino
```

#### 2️⃣ Executar
```
Simplesmente clique duplo em: StockControl.exe
```

✨ **Pronto! O programa abre no terminal**

**Atalho Rápido:**
```cmd
C:\Caminho\StockControl-v1.0-Windows-x64\StockControl.exe
```

---

### 🐧 Linux

#### 1️⃣ Download e Extração
```bash
# Via terminal
unzip StockControl-v1.0-Linux-x64.zip
cd StockControl-v1.0-Linux-x64
```

#### 2️⃣ Dar Permissão de Execução
```bash
chmod +x StockControl
```

#### 3️⃣ Executar
```bash
./StockControl
```

**Ou**
```bash
bash StockControl
```

---

### 🍎 macOS

#### 1️⃣ Download e Extração
```bash
# Via Finder ou terminal
unzip StockControl-v1.0-macOS-x64.zip
cd StockControl-v1.0-macOS-x64
```

#### 2️⃣ Dar Permissão (primeira vez)
```bash
chmod +x StockControl
```

#### 3️⃣ Executar
```bash
./StockControl
```

**Se der erro de "arquivo não é confiável":**
```bash
# Abra Segurança & Privacidade
# Ou use:
sudo spctl --add --label "My Label" ./StockControl
```

---

## 🚀 Como Usar

### 📋 Menu Principal

Ao abrir o programa, você verá:

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

### 1️⃣ Gerenciar Produtos

```
Submenu:
  1. Criar Novo Produto
  2. Visualizar Produtos
  3. Atualizar Produto
  4. Deletar Produto
  0. Voltar
```

**Exemplo: Criar Produto**
```
Nome: Notebook
Categoria: Eletrônicos
Preço: 3500.00

✓ Produto criado com sucesso!
```

### 2️⃣ Gerenciar Estoque

```
Submenu:
  1. Adicionar ao Estoque
  2. Remover do Estoque
  3. Visualizar Estoque
  0. Voltar
```

**Exemplo: Adicionar ao Estoque**
```
Selecione o produto: 1
Quantidade: 5

✓ 5 unidades adicionadas!
Estoque atual: 5
```

### 3️⃣ Relatórios

```
Submenu:
  1. Exportar Inventário (PDF)
  2. Exportar Inventário (TXT)
  0. Voltar
```

**Salva na pasta do programa com data/hora**

---

## ⚠️ Requisitos

### Para Executar

✅ **Sem requisitos adicionais!**

O arquivo já vem com tudo incluído:
- ✓ .NET 10 Runtime (integrado)
- ✓ Todas as dependências
- ✓ Tudo que precisa funcionar

Basta **descompactar e executar!**

### Espaço em Disco
- Aproximadamente **200-300 MB** para a pasta descompactada
- Alguns MB adicionais para dados (JSON)

### Memória RAM
- Mínimo: **100 MB**
- Recomendado: **512 MB**

---

## 🔧 Troubleshooting

### ❌ "Permission denied" (Linux/macOS)

**Solução:**
```bash
chmod +x StockControl
./StockControl
```

### ❌ "arquivo não é confiável" (macOS)

**Solução 1:**
```bash
sudo spctl --add --label "StockControl" ./StockControl
./StockControl
```

**Solução 2:**
- Abra Segurança & Privacidade
- Localize StockControl
- Clique "Abrir mesmo assim"

### ❌ Programa não abre no Windows

**Possíveis causas:**
- Arquivo corrompido (baixe novamente)
- Antivírus bloqueando (desative temporariamente)
- Caminho com caracteres especiais (mude para C:\StockControl\)

**Solução:**
```cmd
cd C:\Caminho\StockControl-v1.0-Windows-x64
StockControl.exe
```

### ❌ Dados não estão sendo salvos

**Solução:**
1. Verifique permissões da pasta
2. Libere acesso de escrita para a pasta
3. Não mude os arquivos JSON manualmente
4. Reinicie o programa

### ❌ Cores estranhas no terminal

**Windows:**
- Clique direito no CMD → Propriedades
- Aba "Cores" → escolha cores
- Ou aumente o zoom do terminal

**Linux/macOS:**
- Terminal já suporta cores natively
- Se problemas, tente: `export TERM=xterm-256color`

---

## 📊 Exemplos de Uso

### Exemplo Completo: Cadastrar e Vender

```
1. Menu → 1 (Gerenciar Produtos)
   └─ 1 (Criar Novo Produto)
   └─ Nome: Teclado Mecânico
   └─ Categoria: Periféricos
   └─ Preço: 450.00

2. Menu → 2 (Gerenciar Estoque)
   └─ 1 (Adicionar ao Estoque)
   └─ Selecione: Teclado Mecânico
   └─ Quantidade: 20 unidades

3. Menu → 3 (Relatórios)
   └─ 1 (Exportar PDF)
   └─ Arquivo gerado: Inventário_2026-10-12.pdf

✓ Tudo funcionando!
```

---

## 📝 Dados e Persistência

### Onde são salvos os dados?

Os dados são salvos em arquivos JSON na **mesma pasta do programa:**

```
StockControl-v1.0-Windows-x64/
├── StockControl.exe
├── ... (outros arquivos)
└── products.json          ← Dados dos produtos
└── sales.json             ← Dados de vendas (v2.0)
```

### Fazer Backup

```bash
# Copie a pasta inteira ou apenas os .json
cp -r StockControl-v1.0-Windows-x64 ~/Backup/
```

### Limpar Dados

```bash
# Delete os arquivos JSON
rm products.json
# Ao abrir novamente, será criado novo
```

---

## ❓ FAQ

### P: Preciso instalar o .NET?
**R:** Não! O runtime já está incluído no arquivo. Basta extrair e usar.

### P: Funciona offline?
**R:** Sim! 100% offline. Sem conexão com internet necessária.

### P: É seguro?
**R:** Código aberto no GitHub. Sem permissões administrativas necessárias. Dados salvos localmente em JSON.

### P: Posso usar em produção?
**R:** v1.0 é estável para uso pessoal/pequeno negócio. Para produção, considere melhorias futuras.

### P: Como exporto os dados?
**R:** Use os relatórios em PDF ou TXT. Ou copie os arquivos JSON manualmente.

### P: Quando sai a v2.0?
**R:** Com a funcionalidade de Vendas. Data estimada: próximas semanas.

### P: Posso reportar bugs?
**R:** Sim! Abra uma issue no GitHub com detalhes.

### P: Código aberto?
**R:** Sim! Disponível em: https://github.com/seu-usuario/StockControl

---

## 🎯 O que Vem na v2.0?

- 📊 Sistema de Vendas completo
- 📈 Relatórios de vendas
- 💰 Cálculo de lucro
- 🔄 Histórico de transações
- 📱 Interface melhorada

---

## 🙏 Créditos

Desenvolvido com ❤️ em **C# e .NET 10**

**Dependências:**
- PDFSharp 6.2.4 - Para geração de PDF

---

## 📄 Licença

Este projeto está sob a licença **MIT**. Veja LICENSE para detalhes.

---

## 🚀 Links Úteis

- 📖 [Documentação Completa](https://github.com/seu-usuario/StockControl)
- 🐛 [Reportar Bugs](https://github.com/seu-usuario/StockControl/issues)
- ⭐ [Deixe uma Star](https://github.com/seu-usuario/StockControl)
- 💬 [Discussões](https://github.com/seu-usuario/StockControl/discussions)

---

<div align="center">

### 🎉 Pronto para usar!

**Obrigado por usar StockControl v1.0! 🙏**

[⬆ Voltar ao Topo](#-stockcontrol-v10---release-notes)

</div>

---

**Versão:** 1.0.0  
**Data:** Outubro 2026  
**Status:** Estável ✅

