Public Module GPSHelper
    Public Function CalculateDistance(lat1 As Double, lon1 As Double, lat2 As Double, lon2 As Double) As Double
        Dim R = 6371.0
        Dim dLat = DegToRad(lat2 - lat1)
        Dim dLon = DegToRad(lon2 - lon1)
        Dim a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
        Dim c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))
        Return Math.Round(R * c, 2)
    End Function

    Public Function ParseCoord(coordText As String) As (lat As Double, lon As Double, isValid As Boolean)
        Try
            coordText = coordText.Trim()
            Dim commaIndex = coordText.IndexOf(","c)
            If commaIndex > 0 AndAlso commaIndex < coordText.Length - 1 Then
                Dim latStr = coordText.Substring(0, commaIndex).Trim().Replace(",", ".")
                Dim lonStr = coordText.Substring(commaIndex + 1).Trim().Replace(",", ".")
                Dim lat = Double.Parse(latStr, System.Globalization.CultureInfo.InvariantCulture)
                Dim lon = Double.Parse(lonStr, System.Globalization.CultureInfo.InvariantCulture)
                Return (lat, lon, True)
            End If
        Catch
        End Try
        Return (0, 0, False)
    End Function

    Public Function GetCooldownTime(distanceKm As Double) As String
        Dim cooldownTable = {
            (1, 1), (2, 1), (3, 2), (4, 2), (5, 3), (8, 4), (10, 6),
            (15, 8), (20, 11), (25, 14), (30, 16), (35, 17), (40, 18),
            (45, 19), (50, 20), (60, 21), (70, 22), (80, 23), (90, 24),
            (100, 26), (125, 28), (150, 31), (175, 33), (201, 36),
            (250, 41), (300, 46), (328, 48), (350, 49), (400, 54),
            (450, 58), (500, 61), (550, 65), (600, 69), (650, 73),
            (700, 76), (751, 81), (802, 83), (839, 88), (897, 90),
            (948, 94), (1007, 97), (1100, 101), (1180, 109), (1221, 112),
            (1300, 117), (1355, 119), (1403, 120)
        }

        For i = 0 To cooldownTable.Length - 1
            If distanceKm <= cooldownTable(i).Item1 Then
                Return cooldownTable(i).Item2.ToString() & " min"
            End If
        Next
        Return cooldownTable(cooldownTable.Length - 1).Item2.ToString() & " min"
    End Function

    Private Function DegToRad(degrees As Double) As Double
        Return degrees * Math.PI / 180.0
    End Function

End Module
