Imports System.Diagnostics
Imports Entropy.System
Imports System.Collections.Generic
Public Module Entropy.System
    Public Sub KillProcess(processId As Integer, processList As List(Of Process))
        For Each it As Process In processList
            If it.PID = processId Then
                it.Kill()
                Console.WriteLine("Killed process: " & processId)
                Exit For
            End If
        Next
    End Sub
End Module