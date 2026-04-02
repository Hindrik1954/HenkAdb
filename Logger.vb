Imports System.IO
Imports System.Linq

Public Module Logger

    Private logPath As String = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "HenkAdb.log")
    Private Const MaxLogSizeBytes As Long = 10 * 1024 * 1024 ' 10MB (oud)
    Private Const BewaarDagen As Integer = 2

    Public Sub Log(message As String, Optional level As String = "INFO")
        Dim timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim logLine = $"[{timestamp}] [{level}] {message}{Environment.NewLine}"

        Try
            CleanupOudeLogs()  ' ← NIEUW: Ruim op vóór append
            RotateLogIfNeeded()
            File.AppendAllText(logPath, logLine)
        Catch ex As Exception
            ' Silent fail voor logging
        End Try
    End Sub

    Public Sub LogError(ex As Exception, context As String)
        Log($"ERROR in {context}: {ex.Message}{Environment.NewLine}{ex.StackTrace}", "ERROR")
    End Sub

    ' ← NIEUW: Verwijder regels ouder dan 2 dagen
    Private Sub CleanupOudeLogs()
        If Not File.Exists(logPath) Then Return

        Try
            Dim cutoffDate = DateTime.Now.AddDays(-BewaarDagen).Date
            Console.WriteLine($"Cleanup: cutoff = {cutoffDate:yyyy-MM-dd}")  ' ← DEBUG

            Dim lines = File.ReadAllLines(logPath)
            Dim oudeTeller = 0
            Dim recenteLines = New List(Of String)

            For Each line In lines
                If String.IsNullOrWhiteSpace(line) OrElse Not line.StartsWith("[") Then
                    recenteLines.Add(line)
                    Continue For
                End If

                Try
                    ' Timestamp: [2026-02-21 19:20:03] → posities 1-19
                    Dim timestampStr = line.Substring(1, 19)
                    Dim logDate = DateTime.ParseExact(timestampStr, "yyyy-MM-dd HH:mm:ss", Nothing)

                    Console.WriteLine($"Line: {timestampStr} → {logDate:yyyy-MM-dd} ({If(logDate >= cutoffDate, "BEWAAR", "VERWIJDER")}")  ' ← DEBUG

                    If logDate.Date >= cutoffDate Then
                        recenteLines.Add(line)
                    Else
                        oudeTeller += 1
                    End If
                Catch parseEx As Exception
                    Console.WriteLine($"Parse ERROR: '{line.Substring(0, 30)}...' → keep")  ' ← DEBUG
                    recenteLines.Add(line)  ' Onparsebare regels behouden
                End Try
            Next

            Console.WriteLine($"Cleanup: {oudeTeller} oude regels verwijderd, {recenteLines.Count} bewaard")  ' ← DEBUG

            If recenteLines.Count < lines.Length Then
                File.WriteAllLines(logPath, recenteLines)
            End If
        Catch ex As Exception
            Console.WriteLine($"Cleanup CRASH: {ex.Message}")  ' ← DEBUG
        End Try
    End Sub


    Private Sub RotateLogIfNeeded()
        If File.Exists(logPath) AndAlso New FileInfo(logPath).Length > MaxLogSizeBytes Then
            Dim backupPath = logPath & ".old"
            If File.Exists(backupPath) Then File.Delete(backupPath)
            File.Move(logPath, backupPath)
        End If
    End Sub

End Module
