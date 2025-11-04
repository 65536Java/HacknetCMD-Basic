Imports CodeExecute
Imports System

Public Module Main
    Sub Main()
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
End Module