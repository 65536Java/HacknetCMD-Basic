Imports System.IO
Public Module HNFiles
    ' 讀取檔案內容
    Public Function ReadFile(filePath As String) As String
        If Not File.Exists(filePath) Then Return String.Empty
        Return File.ReadAllText(filePath)
    End Function

    ' 寫入檔案內容
    Public Sub WriteFile(filePath As String, content As String)
        File.WriteAllText(filePath, content)
    End Sub
    
    ' 檢查檔案是否存在
    Public Function FileExists(filePath As String) As Boolean
        Return File.Exists(filePath)
    End Function
    ' 列出目錄中的所有檔案和子目錄
    Public Function ListDirAndFolders(directoryPath As String) As String()
        If Not Directory.Exists(directoryPath) Then Return New String() {}
        Return Directory.GetFileSystemEntries(directoryPath)
    End Function
    Public Function DirExists(directoryPath As String) As Boolean
        Return Directory.Exists(directoryPath)
    End Function
End Module