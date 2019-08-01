Public Class congrats
    Private Sub congrats_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        My.Computer.Audio.Play(My.Resources.app_29, AudioPlayMode.Background)
    End Sub
End Class