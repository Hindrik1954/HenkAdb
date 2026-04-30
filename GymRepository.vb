Imports System.Data
Imports MySql.Data.MySqlClient

Public Module GymRepository

    Public Function GetGyms() As DataTable
        Dim dt As New DataTable()

        Using conn As New MySqlConnection(DbConfigHelper.GetConnectionString())
            conn.Open()

            Dim sql As String =
                "SELECT GymNaam, Plaats, Raidroute, Coordinaten " &
                "FROM Gyms " &
                "ORDER BY Plaats, GymNaam"

            Using cmd As New MySqlCommand(sql, conn)
                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

End Module