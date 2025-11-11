Imports System.Threading
Imports Server
Imports System
Namespace Entropy.System
    Public MustInherit Class Process
        Public needRam As Integer
        Public Shared CurrentComputer As HNServer = Server.root
        Public isActived As Boolean
        Public PID As Integer
        Public Sub Start()
            isActived = True
            Dim proc As Thread = New Thread(AddressOf ProcessMain)
            proc.Start()
            While isActived
                Thread.Sleep(50)
            End While
            proc.Interrupt()
        End Sub
        Public Sub Kill()
            isActived = False
        End Sub
        Public MustOverride Sub ProcessMain()
        Public Shared Sub StartProcess(process As Process, maxRAM As Integer, ByRef usedRAM As Integer)
            If usedRAM + process.needRam <= maxRAM Then
                usedRAM += process.needRam
                Console.Title = "HacknetCMD[Basic] : RAM Usage: " & usedRAM & " / " & maxRAM
                process.Start()
                usedRAM -= process.needRam
                Console.Title = "HacknetCMD[Basic] : RAM Usage: " & usedRAM & " / " & maxRAM
            Else
                Console.WriteLine("Not enough RAM to start the process.")
            End If
        End Sub
    End Class
End Namespace