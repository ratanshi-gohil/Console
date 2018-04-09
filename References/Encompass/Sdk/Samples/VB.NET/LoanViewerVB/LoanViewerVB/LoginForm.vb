Imports EllieMae.Encompass.Client

Public Class LoginForm

    Private Sub okBtn_Click(sender As Object, e As EventArgs) Handles okBtn.Click
        Dim serverName As String
        Dim loginName As String
        Dim password As String
        Dim session As Session

        ' Collect the values entered
        serverName = serverBox.Text
        loginName = loginNameBox.Text
        password = passwordBox.Text

        Try
            ' Start the session
            session = New Session
            If (serverName = String.Empty) Then
                session.StartOffline(loginName, password)
            Else
                session.Start(serverName, loginName, password)
            End If

            Globals.Session = session
            DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MessageBox.Show("Login Error: " & ex.Message)
        End Try


    End Sub
End Class