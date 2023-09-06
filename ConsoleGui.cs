using Terminal.Gui;
public class ConsoleGui
{
    public Result? ShowLoginOrSignup()
    {
        Application.Init();

        var top = Application.Top;
        var win = new Window("Login or Signup")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        top.Add(win);

        var radioGroup = new RadioGroup(1, 1, new NStack.ustring[] { "_Login", "_Signup" });
        win.Add(radioGroup);

        var lblUsername = new Label(1, 3, "Username:");
        var txtUsername = new TextField(15, 3, 40, "");
        win.Add(lblUsername, txtUsername);

        var lblPassword = new Label(1, 5, "Password:");
        var txtPassword = new TextField(15, 5, 40, "") { Secret = true };
        win.Add(lblPassword, txtPassword);

        var lblConfirmPassword = new Label(1, 7, "Confirm Password:");
        var txtConfirmPassword = new TextField(15, 7, 40, "") { Secret = true, Visible = false };
        lblConfirmPassword.Visible = false;
        win.Add(lblConfirmPassword, txtConfirmPassword);

        Result? result = null;

        var btnAction = new Button(1, 9, "Login");
        btnAction.Clicked += () =>
        {
            HandleBtnAction(ref result, radioGroup, txtUsername, txtPassword, txtConfirmPassword);
            Application.RequestStop();
        };
        win.Add(btnAction);

        radioGroup.SelectedItemChanged += (prev) =>
        {
            if (radioGroup.SelectedItem == 0)
            {
                btnAction.Text = "Login";
                lblConfirmPassword.Visible = false;
                txtConfirmPassword.Visible = false;
            }
            else
            {
                btnAction.Text = "Signup";
                lblConfirmPassword.Visible = true;
                txtConfirmPassword.Visible = true;
            }
        };

        Application.Run();
        return result;
    }

    private void HandleBtnAction(ref Result? result, RadioGroup radioGroup, TextField txtUsername, TextField txtPassword, TextField txtConfirmPassword)
    {
        string? username = txtUsername.Text.ToString();
        string? password = txtPassword.Text.ToString();

        if (radioGroup.SelectedItem == 0)
        {
            if(username is not null && password is not null)
                result = new Result("Login", username, password);
            else
                MessageBox.ErrorQuery("Error", "username and passoword might null", "Ok");
                return;
        }
        else
        {
            string? confirmPassword = txtConfirmPassword.Text.ToString();
            if (password != confirmPassword)
            {
                MessageBox.ErrorQuery("Error", "Passwords do not match!", "Ok");
                return;
            }
            if(username is not null && password is not null)
                result = new Result("Signup", username, password);
            else
                MessageBox.ErrorQuery("Error", "username and passoword might null", "Ok");
                return;
        }
    }
}

public class Result
{
    public string? Action { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public Result(string action, string username, string password)
    {
        this.Action = action;
        this.Username = username;
        this.Password = password;
    }
}
