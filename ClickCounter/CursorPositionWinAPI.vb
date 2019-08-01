Module CursorPositionWinAPI
    Structure PointAPI
        Public x As Int32
        Public y As Int32
    End Structure

    Public Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As PointAPI) As Boolean

    Declare Function ScreenToClient Lib "user32" (ByVal hwnd As Int32, ByRef lpPoint As PointAPI) As Int32

    Public Function MouseXposition()
        Dim lpPoint As PointAPI
        Dim MousePOS As String = GetCursorPos(lpPoint)
        Dim MouseX As String = CStr(lpPoint.x)
        Return MouseX
    End Function

    Public Function MouseYposition()
        Dim lpPoint As PointAPI
        Dim MousePOS As String = GetCursorPos(lpPoint)
        Dim MouseY As String = CStr(lpPoint.y)
        Return MouseY
    End Function
End Module