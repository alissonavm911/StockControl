# 📤 Como Publicar no GitHub Release

## 🎯 Passo a Passo

### 1️⃣ Preparar os Arquivos

```bash
cd C:\Users\moreira\RiderProjects\StockControl\scripts
.\BUILD-RELEASES.bat      # Compila para 5 plataformas
.\COMPRESS-RELEASES.bat   # Cria .zip prontos
```

Resultado: Arquivos em `releases-compressed/`

---

### 2️⃣ Ir para GitHub

Acesse: **https://github.com/seu-usuario/StockControl/releases**

---

### 3️⃣ Criar Nova Release

Clique em **"Create a new release"**

---

### 4️⃣ Preencher Dados

#### **Tag Version**
```
v1.0
```

#### **Release Title**
```
StockControl v1.0 - Sistema de Controle de Estoque
```

#### **Description** (Cole o conteúdo de GITHUB-RELEASE-NOTES.md)

Abra o arquivo em editor de texto:
```
C:\Users\moreira\RiderProjects\StockControl\scripts\GITHUB-RELEASE-NOTES.md
```

Copie **TODO** o conteúdo e cole na caixa de descrição do GitHub.

---

### 5️⃣ Upload dos Arquivos

Clique em **"Attach binaries"** ou arraste os arquivos:

**Arquivos para Upload:**

```
releases-compressed/
├── StockControl-v1.0-Windows-x64.zip     ← Clique e arraste
├── StockControl-v1.0-Windows-x86.zip     ← Clique e arraste
├── StockControl-v1.0-Linux-x64.zip       ← Clique e arraste
├── StockControl-v1.0-macOS-x64.zip       ← Clique e arraste
└── StockControl-v1.0-macOS-ARM64.zip     ← Clique e arraste
```

---

### 6️⃣ Publicar

Clique em **"Publish release"**

---

## ✅ Resultado Final

Seu GitHub Release ficará assim:

```
🎉 StockControl v1.0 - Sistema de Controle de Estoque

[Descrição bonitinha com badges, funcionalidades, etc]

📥 DOWNLOADS:
  • Windows x64 (250 MB)
  • Windows x86 (220 MB)
  • Linux x64 (200 MB)
  • macOS x64 (240 MB)
  • macOS ARM64 (230 MB)

Published por: seu-usuario
```

---

## 📋 Checklist

- [x] Compilar releases: `.\BUILD-RELEASES.bat`
- [x] Comprimir releases: `.\COMPRESS-RELEASES.bat`
- [x] Criar arquivo GITHUB-RELEASE-NOTES.md
- [ ] Ir para GitHub
- [ ] Clique "Create a new release"
- [ ] Tag: v1.0
- [ ] Title: StockControl v1.0
- [ ] Description: Cole GITHUB-RELEASE-NOTES.md
- [ ] Upload 5 arquivos .zip
- [ ] Clique "Publish release"

---

## 🎨 Extras (Opcional)

### Adicionar Badges no GitHub

Na raiz do seu repositório, edite o `README.md` e adicione:

```markdown
![Downloads](https://img.shields.io/github/downloads/seu-usuario/StockControl/v1.0/total?style=flat-square)
```

### Adicionar Changelog

Se quiser, pode adicionar à descrição:

```markdown
## 📝 Changelog

### v1.0.0 - Controle de Estoque (Inicial)
- ✅ Gerenciamento de Produtos
- ✅ Controle de Estoque
- ✅ Relatórios (PDF/TXT)
- ✅ Persistência em JSON

### v2.0 (Em Breve)
- 🔄 Sistema de Vendas
- 📊 Relatórios de Vendas
- 💰 Cálculo de Lucro
```

---

## 💡 Dicas

✅ Use **Markdown** na descrição

✅ Certifique que os arquivos .zip têm bom tamanho (150-300 MB é normal)

✅ Teste um arquivo antes de publicar (extraia e rode)

✅ A descrição suporta imagens: `![alt](url)`

✅ Pode editar a release depois se esquecer algo

---

## 🔗 Links Úteis

- [GitHub Releases Docs](https://docs.github.com/en/repositories/releasing-projects-on-github/about-releases)
- [Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet)
- [Semantic Versioning](https://semver.org/)

---

**Pronto! Sua v1.0 estará viva no GitHub! 🚀**

