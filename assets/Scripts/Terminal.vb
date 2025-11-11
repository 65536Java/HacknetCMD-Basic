Imports System
Imports System.Threading
Imports System.Random
Namespace Terminals
    Public Class Terminal
        Public Shared Function terminal(ServersAvailable As HNServer(),ByRef CurrentComputer As HNServer) As String
            Dim cmd As String = "None"
            Dim Args As String()
            Dim temp As String
            Console.Write(CurrentComputer.IP & "@> ")
            temp = Console.ReadLine()
            If String.IsNullOrWhiteSpace(temp) Then Return "None"

            Dim parts() As String = temp.Split(" "c)
            cmd = parts(0)

            ' 手動建立 Args，避免在舊編譯器或無 LINQ 擴充時出錯
            If parts.Length > 1 Then
                ReDim Args(parts.Length - 2)
                For i As Integer = 1 To parts.Length - 1
                    Args(i - 1) = parts(i)
                Next
            Else
                Args = New String() {}
            End If

            If cmd = "connect" Then
                If Args.Length = 0 Then
                    Console.WriteLine("Usage: connect [IP]")
                End If
                For Each server As HNServer In ServersAvailable
                    If server.IP = Args(0) Then
                        CurrentComputer = server
                        Console.WriteLine("Connected to " & server.Name)
                        Exit For
                    End If
                Next
            ElseIf cmd = "scan" Then
                CurrentComputer.ScanServer(CurrentComputer)
            ElseIf cmd = "dc" OrElse cmd = "disconnect" Then
                CurrentComputer = Server.root
                Console.WriteLine("Disconnected.")
            ElseIf cmd = "probe" OrElse cmd = "nmap" Then
                Console.WriteLine("Probing " & CurrentComputer.IP & "...")
                Console.WriteLine("---------------------------------------")
                Thread.Sleep(1000)
                Console.WriteLine("---------------------------------------")
                Console.WriteLine("Probe complete - open ports:")
                Console.WriteLine("---------------------------------------")
                Dim portType As String
                For Each p As Integer In CurrentComputer.OpenPorts
                    Select Case p
                        Case 80
                            portType = "HTTP WebServer"
                        Case 25
                            portType = "SMTP Mail Server"
                        Case 21
                            portType = "FTP Server"
                        Case 22
                            portType = "SSH"
                        Case Else
                            portType = "Unknown Service"
                    End Select
                    Console.WriteLine("Port#: " & p & " - " & portType)
                Next
                Console.WriteLine("Open ports required for crack: " & CurrentComputer.NeedCrackPortsCount)
            ElseIf cmd = "porthack" Then
                Entropy.System.Process.StartProcess(New Porthack(CurrentComputer), Game.GetMaxRam(), Game.GetUsedRam())
            Else
                Console.WriteLine("Unknown command: " & cmd)
            End If
            Return cmd
        End Function
        
    End Class
End Namespace