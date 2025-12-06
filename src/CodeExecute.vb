Imports System.IO
Imports System.Reflection
Imports System
Imports Game

Public Module CodeExecute
    Public Sub RunScript()
        ' 取得應用程式啟動時的基礎目錄 (通常就是 EXE 所在的目錄)
        Dim executionDirectory As String = System.AppDomain.CurrentDomain.BaseDirectory
        Game.GameMain(executionDirectory)
    End Sub
End Module