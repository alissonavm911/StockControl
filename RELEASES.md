# StockControl v1.0 - Guia de Releases

Este documento explica como gerar executáveis para diferentes sistemas operacionais.

## 🎯 Plataformas Suportadas

- **Windows x64** (64-bit) - Versão de 64 bits para Windows moderno
- **Windows x86** (32-bit) - Versão de 32 bits para Windows antigo
- **Linux x64** - Para servidores e desktops Linux
- **macOS x64** - Para Macs Intel
- **macOS ARM64** - Para Macs Apple Silicon (M1/M2/M3)

## 🚀 Como Gerar as Releases

### Opção 1: Windows (PowerShell)

```powershell
# Abra o PowerShell no diretório do projeto e execute:
.\build-releases.ps1
```

### Opção 2: Linux/macOS (Bash)

```bash
# Abra o terminal no diretório do projeto e execute:
bash build-releases.sh
```

## 📦 Estrutura de Saída

Após a execução, você terá uma estrutura assim:

```
releases/
├── StockControl-v1.0-Windows-x64/
│   ├── StockControl.exe
│   ├── *.dll (todas as dependências)
│   └── ...
├── StockControl-v1.0-Windows-x86/
│   ├── StockControl.exe
│   ├── *.dll
│   └── ...
├── StockControl-v1.0-Linux-x64/
│   ├── StockControl
│   ├── *.so (dependências)
│   └── ...
├── StockControl-v1.0-macOS-x64/
│   ├── StockControl
│   ├── *.dylib
│   └── ...
└── StockControl-v1.0-macOS-ARM64/
    ├── StockControl
    ├── *.dylib
    └── ...
```

## 💾 Tamanho dos Arquivos

Cada executável é **self-contained**, ou seja, inclui o .NET Runtime:
- **Windows**: ~200-300 MB
- **Linux**: ~150-250 MB
- **macOS**: ~180-280 MB

## 🔧 Uso dos Executáveis

### Windows
```cmd
StockControl-v1.0-Windows-x64\StockControl.exe
```

### Linux
```bash
./StockControl-v1.0-Linux-x64/StockControl
# ou
chmod +x ./StockControl-v1.0-Linux-x64/StockControl
./StockControl-v1.0-Linux-x64/StockControl
```

### macOS
```bash
./StockControl-v1.0-macOS-x64/StockControl
# ou para Apple Silicon
./StockControl-v1.0-macOS-ARM64/StockControl
```

## 📤 Para Publicar no GitHub

1. **Crie as releases**:
   ```powershell
   .\build-releases.ps1
   ```

2. **Comprima cada pasta**:
   - Windows x64: `StockControl-v1.0-Windows-x64.zip`
   - Windows x86: `StockControl-v1.0-Windows-x86.zip`
   - Linux: `StockControl-v1.0-Linux-x64.tar.gz`
   - macOS x64: `StockControl-v1.0-macOS-x64.zip`
   - macOS ARM64: `StockControl-v1.0-macOS-ARM64.zip`

3. **Faça upload no GitHub Releases**:
   - Vá para: https://github.com/seu-usuario/StockControl/releases
   - Clique em "Create a new release"
   - Tag: `v1.0`
   - Title: `StockControl v1.0 - Stock Management System`
   - Upload os arquivos .zip e .tar.gz

## ℹ️ Informações da Versão

- **Versão**: 1.0.0
- **.NET Target**: .NET 10.0
- **Tipo de Build**: Release (otimizado)
- **Runtime**: Self-contained (nenhuma dependência externa necessária)

## ⚠️ Pré-requisitos

- .NET 10 SDK instalado
- Git instalado (para verificar commits)
- Espaço em disco: ~5-10 GB para compilar todas as plataformas

## 🐛 Troubleshooting

### Erro: "dotnet command not found"
- Instale o .NET 10 SDK
- Reinicie o terminal/PowerShell

### Erro: "RID not supported"
- Verifique a versão do .NET SDK: `dotnet --version`
- Certifique-se de que tem .NET 10.0 ou superior

### Pasta de Release Incompleta
- Delete a pasta de release e tente novamente
- Verifique a conexão de internet (algumas dependências podem ser baixadas)

## 📝 Próximas Versões

- **v2.0**: Quando a funcionalidade de vendas (Sales) for concluída
- **v3.0+**: Novos recursos e melhorias

---

**Criado**: Outubro 2026
**Desenvolvedor**: StockControl Team

