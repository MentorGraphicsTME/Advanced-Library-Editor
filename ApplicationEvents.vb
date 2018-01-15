Namespace My

    ' The following events are available for MyApplication:
    '
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim oAbout As New AboutLoader

            'vxEnv = CreateObject("MGCPCBReleaseEnvServer.Application")

            Dim bResults As Boolean = oAbout.Show(My.Application.Info.Version.ToString(), My.Application.Info.Title, Type.LibraryManager, True)

            If bResults = False Then

                MessageBox.Show("You must agree to the terms and conditions to run this program. If you feel as though you do not have the authority to make that choice, please contact to your supervisor and/or HR department.", "Terms and Agreement:",
    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End

            End If

        End Sub

    End Class

End Namespace