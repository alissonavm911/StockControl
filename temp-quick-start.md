# 🚀 Guia Rápido - Compilar StockControl v1.0

## ⚡ Resumo

Para criar executáveis da v1.0 do StockControl (apenas com estoque, sem vendas) para Windows x64, Windows x86, Linux e macOS, siga os passos abaixo.

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
cd C:\Users\moreira\RiderProjects\StockControl\scripts
.\BUILD-RELEASES.bat
```

#### 🪟 Windows - Método 3: Menu Interativo

```powershell
cd C:\Users\moreira\RiderProjects\StockControl\scripts
.\BUILD-LAUNCHER.vbs
```

#### 🐧 Linux/macOS (Bash)

```bash
cd ~/RiderProjects/StockControl/scripts
bash build-releases.sh
```

**Tempo esperado**: 10-20 minutos (depende da internet)

---

### 3️⃣ Comprimir os Arquivos (Opcional)

Para facilitar o upload no GitHub:

```powershell
cd C:\Users\moreira\RiderProjects\StockControl\scripts
.\COMPRESS-RELEASES.bat
```

Ou simplesmente clique **duplo** em `COMPRESS-RELEASES.bat`

Isto criará a pasta `releases-compressed/` com arquivos `.zip` prontos.

---

## 📦 Estrutura Gerada

```
StockControl/
├── releases/
│   ├── StockControl-v1.0-Windows-x64/      ← Windows 64-bit
│   ├── StockControl-v1.0-Windows-x86/      ← Windows 32-bit
│   ├── StockControl-v1.0-Linux-x64/        ← Linux 64-bit
│   ├── StockControl-v1.0-macOS-x64/        ← macOS Intel
│   └── StockControl-v1.0-macOS-ARM64/      ← macOS Apple Silicon
└── releases-compressed/
    ├── StockControl-v1.0-Windows-x64.zip
    ├── StockControl-v1.0-Windows-x86.zip
    ├── StockControl-v1.0-Linux-x64.zip
    ├── StockControl-v1.0-macOS-x64.zip
    └── StockControl-v1.0-macOS-ARM64.zip
```

---

## 🎯 Usar os Executáveis

### Windows
```cmd
StockControl-v1.0-Windows-x64\StockControl.exe
```

### Linux
```bash
chmod +x StockControl-v1.0-Linux-x64/StockControl
./StockControl-v1.0-Linux-x64/StockControl
```

### macOS
```bash
chmod +x StockControl-v1.0-macOS-x64/StockControl
./StockControl-v1.0-macOS-x64/StockControl
```

---

## 📤 Upload no GitHub

1. Acesse: `https://github.com/seu-usuario/StockControl/releases`
2. Clique em **"Create a new release"**
3. Preencha:
   - **Tag**: `v1.0`
   - **Title**: `StockControl v1.0 - Stock Management System`
   - **Description**: 
     ```
     ## 📝 Versão 1.0 - Sistema de Controle de Estoque
     
     ### ✨ Funcionalidades
     - ✅ CRUD de Produtos
     - ✅ Gerenciamento de Estoque
     - ✅ Relatórios de Inventário (PDF e TXT)
     - ✅ Interface em Console
     
     ### 📊 Plataformas Suportadas
     - Windows (x64 e x86)
     - Linux (x64)
     - macOS (Intel e Apple Silicon)
     
     ### ⚠️ Nota
     Todos os executáveis já incluem o .NET Runtime integrado.
     Não é necessário instalar nada adicional!
     ```
4. Faça upload dos arquivos .zip de `releases-compressed/`
5. Clique em **"Publish release"**

---

## 📊 Informações Técnicas

| Informação | Detalhe |
|---|---|
| **Versão** | 1.0.0 |
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
- [ ] Comprimir arquivos com `compress-releases.ps1`
- [ ] Fazer upload no GitHub Releases
- [ ] Testar um dos executáveis

---

## 🎉 Pronto!

Agora você tem a v1.0 pronta para compartilhar! 

**Próximas versões**:
- v2.0 → Quando terminar a feature de Vendas
- v3.0+ → Novas funcionalidades

---

**Dúvidas?** Verifique o arquivo `RELEASES.md` para mais detalhes!

