# 🚀 Guia Rápido - Compilar StockControl v2.0

## ⚡ Resumo

Para criar executáveis da v2.0 do StockControl (Sistema completo com Estoque + Vendas + Relatórios Financeiros) para Windows x64, Windows x86, Linux e macOS, siga os passos abaixo.

---

## 📋 Passo a Passo

### 1️⃣ Pré-requisitos

Certifique-se de que tem:
- ✅ .NET 10 SDK instalado
- ✅ Git instalado
- ✅ ~5-10 GB de espaço em disco
- ✅ Conexão com a internet

Verifique com:
```bash
dotnet --version
git --version
```

---

### 2️⃣ Gerar os Executáveis

#### 🪟 Windows - Método 1: Duplo Clique (Mais Fácil!)

1. Abra a pasta `scripts/`
2. Clique **duplo** em `BUILD-RELEASES.bat`
3. Aguarde terminar ✓

#### 🪟 Windows - Método 2: PowerShell

```powershell
cd C:\Users\moreira\.cursor\projects\StockControl\scripts
.\BUILD-RELEASES.bat
```

#### 🪟 Windows - Método 3: Menu Interativo

```powershell
cd C:\Users\moreira\.cursor\projects\StockControl\scripts
.\BUILD-LAUNCHER.vbs
```

#### 🐧 Linux/macOS (Bash)

```bash
cd ~/StockControl/scripts
bash build-releases.sh
```

**Tempo esperado**: 10-20 minutos (depende da internet)

---

### 3️⃣ Comprimir os Arquivos (Opcional)

Para facilitar o upload no GitHub:

```powershell
cd C:\Users\moreira\.cursor\projects\StockControl\scripts
.\COMPRESS-RELEASES.bat
```

Ou simplesmente clique **duplo** em `COMPRESS-RELEASES.bat`

Isto criará a pasta `releases-compressed/` com arquivos `.zip` prontos.

---

## 📦 Estrutura Gerada

```
StockControl/
├── releases/
│   ├── StockControl-v2.0-Windows-x64/      ← Windows 64-bit
│   ├── StockControl-v2.0-Windows-x86/      ← Windows 32-bit
│   ├── StockControl-v2.0-Linux-x64/        ← Linux 64-bit
│   ├── StockControl-v2.0-macOS-x64/        ← macOS Intel
│   └── StockControl-v2.0-macOS-ARM64/      ← macOS Apple Silicon
└── releases-compressed/
    ├── StockControl-v2.0-Windows-x64.zip
    ├── StockControl-v2.0-Windows-x86.zip
    ├── StockControl-v2.0-Linux-x64.zip
    ├── StockControl-v2.0-macOS-x64.zip
    └── StockControl-v2.0-macOS-ARM64.zip
```

---

## 🎯 Usar os Executáveis

### Windows
```cmd
StockControl-v2.0-Windows-x64\StockControl.exe
```

### Linux
```bash
chmod +x StockControl-v2.0-Linux-x64/StockControl
./StockControl-v2.0-Linux-x64/StockControl
```

### macOS
```bash
chmod +x StockControl-v2.0-macOS-x64/StockControl
./StockControl-v2.0-macOS-x64/StockControl
```

---

## 📤 Upload no GitHub

1. Acesse: `https://github.com/seu-usuario/StockControl/releases`
2. Clique em **"Create a new release"**
3. Preencha:
   - **Tag**: `v2.0`
   - **Title**: `StockControl v2.0 - Stock Management + Sales System`
   - **Description**: 
     ```
     ## 📝 Versão 2.0 - Sistema Completo de Estoque e Vendas (Versão Final)
     
     ### ✨ Funcionalidades
     - ✅ CRUD de Produtos
     - ✅ Gerenciamento de Estoque
     - ✅ Relatórios de Inventário (PDF e TXT)
     - ✅ Sistema de Vendas Completo
     - ✅ Relatórios Financeiros (PDF e TXT)
     - ✅ Top 5 Produtos Mais Vendidos
     - ✅ Receita por Categoria
     - ✅ Alertas de Reabastecimento
     - ✅ Interface em Console
     
     ### 📊 Plataformas Suportadas
     - Windows (x64 e x86)
     - Linux (x64)
     - macOS (Intel e Apple Silicon)
     
     ### ⚠️ Nota
     Todos os executáveis já incluem o .NET Runtime integrado.
     Não é necessário instalar nada adicional!
     
     ### 🎯 Status
     Esta é a **versão final e definitiva** do projeto StockControl.
     ```
4. Faça upload dos arquivos .zip de `releases-compressed/`
5. Clique em **"Publish release"**

---

## 📊 Informações Técnicas

| Informação | Detalhe |
|---|---|
| **Versão** | 2.0.0 (Final) |
| **.NET** | .NET 10.0 |
| **Build** | Release (Otimizado) |
| **Runtime** | Self-contained (incluído) |
| **Tamanho** | ~200-300 MB por plataforma |

---

## 🐛 Troubleshooting

### ❌ Erro: "dotnet: command not found"
**Solução**: Instale o .NET 10 SDK e reinicie o terminal

### ❌ Erro: "RID not supported"
**Solução**: Verifique se tem .NET 10 ou superior: `dotnet --version`

### ❌ Compilação muito lenta
**Solução**: Normal na primeira vez (precisa baixar dependências)

### ❌ Arquivo .zip/tar.gz não criado
**Solução**: Verifique se as pastas de release foram criadas primeiro

---

## ✅ Checklist Final

- [ ] .NET 10 SDK instalado
- [ ] Git configurado
- [ ] Executar script de compilação
- [ ] Verificar pasta `releases/` com sucesso
- [ ] Comprimir arquivos com `COMPRESS-RELEASES.bat`
- [ ] Fazer upload no GitHub Releases
- [ ] Testar um dos executáveis

---

## 🎉 Pronto!

Agora você tem a v2.0 (versão final) pronta para compartilhar! 

**Status do Projeto**:
- ✅ v1.0 → Sistema de Estoque
- ✅ v2.0 → Sistema Completo (Estoque + Vendas) - **VERSÃO FINAL**

---

**Dúvidas?** Verifique o arquivo `RELEASES.md` para mais detalhes!
