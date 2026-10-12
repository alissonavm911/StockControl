# 🎯 Como Criar um Atalho com Ícone na Área de Trabalho

## 🪟 Windows - Método 1: Atalho Direto (Recomendado)

### Passo a Passo

1. **Abra o Explorer** e navegue até:
   ```
   C:\Users\moreira\RiderProjects\StockControl\scripts
   ```

2. **Clique com botão direito** em `BUILD-RELEASES.bat`

3. **Selecione** "Enviar para" → "Área de Trabalho (criar atalho)"

4. **Personalize o atalho**:
   - Clique com botão direito no novo atalho da área de trabalho
   - Selecione "Propriedades"
   - Em "Atalho" procure por "Mudar ícone..."
   - Escolha um ícone do Windows ou use um customizado

5. **Pronto!** Agora pode clicar duas vezes para compilar

---

## 🎯 Windows - Método 2: Atalho via Script VBS (Menu Interativo)

### Opção A: Criar Atalho do BUILD-LAUNCHER.vbs

1. **Navegue até**:
   ```
   C:\Users\moreira\RiderProjects\StockControl\scripts
   ```

2. **Clique com botão direito** em `BUILD-LAUNCHER.vbs`

3. **Selecione** "Enviar para" → "Área de Trabalho (criar atalho)"

4. **Renomeie o atalho** para algo como "StockControl Build"

5. **Personalize**:
   - Clique direito → Propriedades
   - Em "Executar:" mude para "Minimizado" (opcional)
   - Mude o ícone se desejar

6. **Duplo clique** para abrir um menu interativo!

---

## 📝 Opção B: Criar um Atalho Customizado

Se preferir criar um atalho com mais controle:

1. **Clique direito na Área de Trabalho** → "Novo" → "Atalho"

2. **Na caixa de localização, cole**:
   ```
   cmd.exe /k "C:\Users\moreira\RiderProjects\StockControl\scripts\BUILD-RELEASES.bat"
   ```

3. **Próximo** e nomeie como "StockControl Build"

4. **Finalizar**

5. **Personalize** (clique direito → Propriedades):
   - **Ícone**: Clique em "Mudar ícone..."
   - **Janela**: Mude para "Normal" ou "Minimizado"
   - **Iniciar em**: 
     ```
     C:\Users\moreira\RiderProjects\StockControl
     ```

---

## 🎨 Ícones Recomendados

### Para Build
- `C:\Windows\System32\imageres.dll` (ícone 54 - caixa/pacote)
- `C:\Program Files\dotnet\...` (se tiver o ícone do .NET)

### Para Compress
- `C:\Windows\System32\shell32.dll` (ícone 260 - pasta comprimida)

---

## 📦 Scripts Disponíveis na Pasta

| Arquivo | Descrição |
|---------|-----------|
| **BUILD-RELEASES.bat** | Compila para todas as plataformas |
| **COMPRESS-RELEASES.bat** | Comprime os releases para upload |
| **BUILD-LAUNCHER.vbs** | Menu interativo (Sim/Não) |

---

## ⚡ Uso Rápido

### Via Atalho da Área de Trabalho
Simplesmente **duplo clique** no atalho criado!

### Via Terminal
```powershell
cd C:\Users\moreira\RiderProjects\StockControl\scripts
.\BUILD-RELEASES.bat
```

---

## 🎯 Fluxo Recomendado

```
1. Clique no atalho "BUILD-RELEASES"
   ↓
2. Aguarde a compilação terminar
   ↓
3. Clique no atalho "COMPRESS-RELEASES"
   ↓
4. Upload no GitHub Releases
```

---

## 🔄 Atualizar o Atalho

Se o caminho do projeto mudar:

1. Clique direito no atalho → "Propriedades"
2. Na aba "Atalho", atualize o campo de localização
3. Clique "Aplicar" e "OK"

---

## 💡 Dicas

✅ **Se quiser silencioso**: Use o BUILD-LAUNCHER.vbs (não mostra janela preta)

✅ **Se quiser ver detalhes**: Use BUILD-RELEASES.bat (mostra tudo no terminal)

✅ **Se quiser menu**: Duplo clique no BUILD-LAUNCHER.vbs (pergunta qual ação fazer)

---

## 🆘 Troubleshooting

### ❌ "Arquivo não encontrado"
- Verifique se o caminho está correto
- Atualize as propriedades do atalho

### ❌ "Acesso negado"
- Execute como Administrador (clique direito → "Executar como administrador")
- Verifique as permissões da pasta

### ❌ "Arquivo .bat fica aberto após execução"
- É normal! Pressione qualquer tecla para fechar
- Se quiser fechar automaticamente, remova a linha `pause` do .bat

---

## 📸 Print do Menu VBS

Quando executa BUILD-LAUNCHER.vbs, aparece:

```
┌─────────────────────────────────┐
│ StockControl v1.0 - Menu        │
├─────────────────────────────────┤
│ Escolha uma opção:              │
│                                 │
│ Clique 'Sim' para COMPILAR      │
│ Clique 'Não' para COMPRIMIR     │
│ Clique 'Cancelar' para sair     │
│                                 │
│  [Sim]  [Não]  [Cancelar]      │
└─────────────────────────────────┘
```

---

**Pronto! Agora você tem um atalho visual para compilar! 🎉**

