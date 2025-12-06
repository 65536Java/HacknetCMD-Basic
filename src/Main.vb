Imports CodeExecute
Imports System
Imports System.Threading
Imports System.Media
Imports System.IO
Public Module Main
    Sub Main()
        Splash()
        Try
            CodeExecute.RunScript()
        Catch ex As Exception
            ' 輸出詳細的錯誤訊息
            ' *** FIX: 將 &+ex.Message 修正為 & ex.Message ***
            Console.WriteLine("ERROR: Failed to run script. Error: " & ex.Message)
            Console.WriteLine("StackTrace: " & ex.StackTrace) ' 這裡也修正一下，使用 &

            If ex.InnerException IsNot Nothing Then
                Console.WriteLine("Internal Error: " & ex.InnerException.Message) ' 這裡也修正一下
            End If
        End Try
    End Sub
    Sub Splash()
        Console.Clear()
        Console.WriteLine("                             Retrayer v1.2.1 By Nullable Software(C)")
        Console.WriteLine("                                     Made with RetroShell(TM)")
        Console.WriteLine("                                       All rights reserved.")
        Threading.Thread.Sleep(3000)
    End Sub
End Module