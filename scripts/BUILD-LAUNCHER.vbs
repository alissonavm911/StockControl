' ═══════════════════════════════════════════════════════════════════════════
' StockControl v1.0 - Build Releases Launcher
' 
' Este script abre um menu para compilar ou comprimir os releases
' Pode ser convertido em um atalho com ícone para a área de trabalho
' ═══════════════════════════════════════════════════════════════════════════

Set objShell = CreateObject("WScript.Shell")
Set objFSO = CreateObject("Scripting.FileSystemObject")

' Obter o diretório onde o script está
strScriptPath = objFSO.GetParentFolderName(WScript.ScriptFullName)
strProjectPath = objFSO.GetParentFolderName(strScriptPath)

' Caminhos dos scripts
strBuildScript = strScriptPath & "\BUILD-RELEASES.bat"
strCompressScript = strScriptPath & "\COMPRESS-RELEASES.bat"

' Verificar se os scripts existem
If Not objFSO.FileExists(strBuildScript) Then
    MsgBox "Erro: BUILD-RELEASES.bat não encontrado!" & vbCrLf & _
           "Caminho esperado: " & strBuildScript, vbCritical, "StockControl - Erro"
    WScript.Quit 1
End If

' Criar o menu
Set objIE = CreateObject("InternetExplorer.Application")

' Alternativamente, usar uma caixa de diálogo simples
Dim objWMIService, objProcess
Set objWMIService = GetObject("winmgmts:")

' Menu simples com InputBox
intChoice = MsgBox("Escolha uma opção:" & vbCrLf & vbCrLf & _
                   "Clique 'Sim' para COMPILAR RELEASES (Build)" & vbCrLf & _
                   "Clique 'Não' para COMPRIMIR RELEASES (Compress)" & vbCrLf & _
                   "Clique 'Cancelar' para sair", _
                   vbYesNoCancel + vbQuestion, _
                   "StockControl v1.0 - Menu de Releases")

Select Case intChoice
    Case vbYes
        ' Executar BUILD
        Call ExecuteScript(strBuildScript)
    Case vbNo
        ' Executar COMPRESS
        Call ExecuteScript(strCompressScript)
    Case vbCancel
        ' Sair
        WScript.Quit 0
End Select

' ═══════════════════════════════════════════════════════════════════════════
' Subroutina para executar o script
' ═══════════════════════════════════════════════════════════════════════════
Sub ExecuteScript(strScript)
    On Error Resume Next
    
    Dim intReturnCode
    Set objWMI = GetObject("winmgmts:")
    Set objProcess = objWMI.ExecMethod("Win32_Process", "Create")
    
    ' Abrir CMD e executar o script
    strCommand = "cmd.exe /k """ & strScript & """"
    intReturnCode = objShell.Run(strCommand, 1, True)
    
    If Err.Number <> 0 Then
        MsgBox "Erro ao executar: " & Err.Description, vbCritical, "Erro"
    End If
    
    On Error GoTo 0
End Sub

WScript.Quit 0

