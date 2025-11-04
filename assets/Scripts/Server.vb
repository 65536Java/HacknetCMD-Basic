' ...existing code...
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Public Module Server
    Public Class HNServer
        Public Sub New()
            IP = "127.0.0.1"
            Name = "HNServer"
            Ports = New Integer() {80, 25, 21, 22}
            NeedCrackPortsCount = 0
            HasProxy = False
            HasFirewall = False
            HasTrace = False
            TraceSpeed = 1.0F
            ConnectedServers = New String() {}
        End Sub
        Public Property Contents() As String()
        Public Property IP As String
        Public Property Name As String
        Public Property Ports() As Integer()
        Public Property NeedCrackPortsCount As Integer
        Public Property HasProxy As Boolean
        Public Property HasFirewall As Boolean
        Public Property HasTrace As Boolean
        Public Property TraceSpeed As Single
        Public Property ConnectedServers() As String()
    End Class
    ' ...existing HNServer 類別 保留或放在這裡...
    Public Class ServerInfoParse
        ' 以手寫解析（不使用 DataContract / JsonSerializer）
        Public Function LoadServers(path As String) As List(Of HNServer)
            Dim result As New List(Of HNServer)()
            If String.IsNullOrEmpty(path) OrElse Not File.Exists(path) Then
                Return result
            End If

            Dim txt As String = File.ReadAllText(path)
            ' 找出每個物件區塊（簡單方式，適用於你的 JSON 結構）
            Dim objPattern As String = "\{(.*?)\}"
            For Each m As Match In Regex.Matches(txt, objPattern, RegexOptions.Singleline)
                Dim body As String = m.Groups(1).Value
                Dim s As New HNServer()

                Dim mName = Regex.Match(body, """name""\s*:\s*""([^""]*)""", RegexOptions.IgnoreCase)
                If mName.Success Then s.Name = mName.Groups(1).Value

                Dim mIp = Regex.Match(body, """ip""\s*:\s*""([^""]*)""", RegexOptions.IgnoreCase)
                If mIp.Success Then s.IP = mIp.Groups(1).Value

                Dim mProxy = Regex.Match(body, """proxy""\s*:\s*(true|false)", RegexOptions.IgnoreCase)
                If mProxy.Success Then s.HasProxy = (mProxy.Groups(1).Value.ToLower() = "true")

                Dim mFirewall = Regex.Match(body, """firewall""\s*:\s*(true|false)", RegexOptions.IgnoreCase)
                If mFirewall.Success Then s.HasFirewall = (mFirewall.Groups(1).Value.ToLower() = "true")

                Dim mNeed = Regex.Match(body, """need_crack_ports""\s*:\s*(\d+)", RegexOptions.IgnoreCase)
                If mNeed.Success Then
                    Integer.TryParse(mNeed.Groups(1).Value, s.NeedCrackPortsCount)
                End If

                ' 解析 contents 字串
                Dim mContents = Regex.Match(body, """contents""\s*:\s*""([^""]*)""", RegexOptions.IgnoreCase)
                If mContents.Success Then 
                    s.Contents = mContents.Groups(1).Value
                End If
                ' 解析 ports 陣列（如果有）
                Dim mPorts = Regex.Match(body, """ports""\s*:\s*\[([^\]]*)\]", RegexOptions.IgnoreCase)
                If mPorts.Success Then
                    Dim inner = mPorts.Groups(1).Value
                    Dim nums As New List(Of Integer)()
                    For Each numM As Match In Regex.Matches(inner, "-?\d+")
                        Dim v As Integer
                        If Integer.TryParse(numM.Value, v) Then nums.Add(v)
                    Next
                    If nums.Count > 0 Then s.Ports = nums.ToArray()
                End If

                ' 解析 connected_servers 陣列（字串）
                Dim mCs = Regex.Match(body, """connected_servers""\s*:\s*\[([^\]]*)\]", RegexOptions.IgnoreCase)
                If mCs.Success Then
                    Dim inner = mCs.Groups(1).Value
                    Dim listCs As New List(Of String)()
                    For Each strM As Match In Regex.Matches(inner, """([^""]+)""")
                        listCs.Add(strM.Groups(1).Value)
                    Next
                    s.ConnectedServers = listCs.ToArray()
                Else
                    s.ConnectedServers = New String() {}
                End If

                result.Add(s)
            Next

            Return result
        End Function
    End Class
End Module
' ...existing code...