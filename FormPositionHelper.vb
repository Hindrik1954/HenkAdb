Imports System.Windows.Forms
Imports System.Drawing

Module FormPositionHelper

    Public Sub LoadFormPosition(frm As Form, ini As IniFile)
        frm.StartPosition = FormStartPosition.Manual

        Dim section As String = frm.Name

        Dim screenIndex As Integer = 0
        Dim offsetX As Integer = 100
        Dim offsetY As Integer = 100
        Dim w As Integer = frm.Width
        Dim h As Integer = frm.Height

        Integer.TryParse(ini.ReadValue(section, "ScreenIndex", "0"), screenIndex)
        Integer.TryParse(ini.ReadValue(section, "OffsetX", offsetX.ToString()), offsetX)
        Integer.TryParse(ini.ReadValue(section, "OffsetY", offsetY.ToString()), offsetY)
        Integer.TryParse(ini.ReadValue(section, "Width", w.ToString()), w)
        Integer.TryParse(ini.ReadValue(section, "Height", h.ToString()), h)

        Dim screens = Screen.AllScreens
        If screenIndex < 0 OrElse screenIndex >= screens.Length Then
            screenIndex = 0
        End If

        Dim s = screens(screenIndex)
        Dim wa = s.WorkingArea

        ' ← NIEUW: Verklein form als hij te groot is voor dit scherm
        Dim maxW = Math.Min(w, wa.Width - 50)   ' 50px marge
        Dim maxH = Math.Min(h, wa.Height - 50)

        Dim newBounds As New Rectangle(wa.Left + offsetX, wa.Top + offsetY, maxW, maxH)

        If IsVisibleOnAnyScreen(newBounds) Then
            frm.Bounds = newBounds
        Else
            ' ← FALLBACK: centreer op primair scherm
            frm.StartPosition = FormStartPosition.CenterScreen
        End If
    End Sub

    Public Sub SaveFormPosition(frm As Form, ini As IniFile)
        Dim section As String = frm.Name

        Dim currentScreen = Screen.FromControl(frm)
        Dim screens = Screen.AllScreens
        Dim screenIndex As Integer = Array.IndexOf(screens, currentScreen)
        If screenIndex < 0 Then screenIndex = 0

        Dim wa = currentScreen.WorkingArea
        Dim offsetX As Integer = frm.Left - wa.Left
        Dim offsetY As Integer = frm.Top - wa.Top

        ini.WriteValue(section, "ScreenIndex", screenIndex.ToString())
        ini.WriteValue(section, "OffsetX", offsetX.ToString())
        ini.WriteValue(section, "OffsetY", offsetY.ToString())
        ini.WriteValue(section, "Width", frm.Width.ToString())
        ini.WriteValue(section, "Height", frm.Height.ToString())
    End Sub

    Private Function IsVisibleOnAnyScreen(bounds As Rectangle) As Boolean
        For Each s As Screen In Screen.AllScreens
            If s.WorkingArea.IntersectsWith(bounds) Then
                Return True
            End If
        Next
        Return False
    End Function

End Module