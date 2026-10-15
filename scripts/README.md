# 📦 Scripts - StockControl v1.0

Esta pasta contém todos os scripts necessários para compilar e empacotar a versão 1.0 do StockControl.

## 📂 Estrutura

```
scripts/
├── BUILD-RELEASES.bat           ← Compila para todas as plataformas
├── COMPRESS-RELEASES.bat        ← Comprime os releases
├── BUILD-LAUNCHER.vbs           ← Menu interativo (Sim/Não)
├── SHORTCUT-GUIDE.md            ← Como criar atalho na área de trabalho
└── README.md                    ← Este arquivo
```

## 🚀 Uso Rápido

### Opção 1: Clicar Duplo (Mais Fácil)

1. Clique **duplo** em `BUILD-RELEASES.bat`
2. Aguarde a compilação terminar (5-20 minutos)
3. Clique **duplo** em `COMPRESS-RELEASES.bat`
4. Pronto! Uploads prontos em `releases-compressed/`

### Opção 2: Menu Interativo

1. Clique **duplo** em `BUILD-LAUNCHER.vbs`
2. Escolha entre **Compilar** ou **Comprimir**
3. Execute a ação desejada

### Opção 3: Terminal (PowerShell)

```powershell
cd C:\Users\moreira\RiderProjects\StockControl\scripts
.\BUILD-RELEASES.bat
.\COMPRESS-RELEASES.bat
```

## 📋 Scripts Detalhados

### 1️⃣ BUILD-RELEASES.bat
- **O que faz**: Compila o StockControl para 5 plataformas
- **Tempo**: 5-20 minutos (primeira vez é mais lenta)
- **Output**: Pasta `releases/` com executáveis

**Plataformas compiladas:**
- ✅ Windows x64 (64-bit moderno)
- ✅ Windows x86 (32-bit antigo)
- ✅ Linux x64
- ✅ macOS x64 (Intel)
- ✅ macOS ARM64 (Apple Silicon)

### 2️⃣ COMPRESS-RELEASES.bat
- **O que faz**: Comprime cada pasta em `.zip`
- **Tempo**: 1-5 minutos
- **Output**: Pasta `releases-compressed/` com `.zip` files

**Arquivos criados:**
- 📦 StockControl-v1.0-Windows-x64.zip
- 📦 StockControl-v1.0-Windows-x86.zip
- 📦 StockControl-v1.0-Linux-x64.zip
- 📦 StockControl-v1.0-macOS-x64.zip
- 📦 StockControl-v1.0-macOS-ARM64.zip

### 3️⃣ BUILD-LAUNCHER.vbs
- **O que faz**: Menu interativo (VBScript)
- **Vantagem**: Oferece menu Sim/Não
- **Uso**: Duplo clique para abrir menu

---

## 🔧 Pré-requisitos

Antes de executar, certifique-se de ter:

- ✅ .NET 10 SDK instalado
- ✅ Git configurado
- ✅ ~5-10 GB de espaço em disco
- ✅ Conexão com internet

**Verificar:**
```powershell
dotnet --version
git --version
```

---

## 📊 Fluxo Completo

```
┌─────────────────────────────────────────┐
│ 1. Clique duplo em BUILD-RELEASES.bat  │
│    → Compila 5 plataformas              │
│    → Cria pasta releases/               │
│    ↓                                     │
│ 2. Clique duplo em COMPRESS-RELEASES   │
│    → Comprime em .zip                   │
│    → Cria pasta releases-compressed/    │
│    ↓                                     │
│ 3. Upload em GitHub Releases            │
│    → Compartilhe com o mundo! 🎉       │
└─────────────────────────────────────────┘
```

---

## 📤 Upload no GitHub

Após comprimir:

1. Vá para: `https://github.com/seu-usuario/StockControl/releases`
2. Clique em **"Create a new release"**
3. Preencha:
   - Tag: `v1.0`
   - Title: `StockControl v1.0`
   - Description: (veja template em QUICK-START.md)
4. **Upload** dos arquivos de `releases-compressed/`
5. Clique em **"Publish release"**

---

## 🎨 Criar Atalho com Ícone

Veja o arquivo `SHORTCUT-GUIDE.md` para instruções detalhadas sobre como criar um atalho visual na área de trabalho com ícone.

**Resumo rápido:**
1. Clique direito em `BUILD-RELEASES.bat`
2. "Enviar para" → "Área de Trabalho (criar atalho)"
3. Renomeie e personalize o ícone

---

## 🐛 Troubleshooting

### ❌ "dotnet: command not found"
**Solução**: Instale o .NET 10 SDK

### ❌ Compilação muito lenta
**Normal!** Primeira vez precisa baixar dependências (~1-2 GB)

### ❌ Arquivo .bat fica aberto
**Normal!** Pressione qualquer tecla para fechar

### ❌ Pasta de release incompleta
**Solução**: Delete e execute novamente
```powershell
rmdir /s /q ..\releases
.\BUILD-RELEASES.bat
```

---

## 📈 Próximas Versões

- **v1.0**: Controle de Estoque ✅
- **v2.0**: Com funcionalidade de Vendas (em desenvolvimento)
- **v3.0+**: Novos recursos

---

## 📝 Informações Técnicas

| Info | Detalhe |
|------|---------|
| **.NET** | 10.0 |
| **Build** | Release (otimizado) |
| **Runtime** | Self-contained (incluído) |
| **Tamanho** | ~200-300 MB por plataforma |
| **Versão** | 1.0.0 |

---

## 💡 Dicas Úteis

✅ Mantenha a pasta `scripts/` intacta

✅ Execute como Administrador se tiver problemas

✅ Use `BUILD-LAUNCHER.vbs` para interface gráfica

✅ Verifique a conexão com internet antes de compilar

✅ Aproveite para tomar um café durante a compilação! ☕

---

**Desenvolvido em**: Outubro 2026  
**Para**: StockControl v1.0  
**Por**: Build System

