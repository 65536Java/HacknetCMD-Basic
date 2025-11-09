Public Module Entropy.System
    Dim isActived As Boolean
    Dim PID As Integer
    Public Sub Start()
        isActived = True
        ProcessMain()
    End Sub
    Public Sub Kill()
        isActived = False
    End Sub
    Public Sub ProcessMain()
        While isActived
            ' 在這裡執行主要的處理邏輯
        End While
    End Sub
End Module