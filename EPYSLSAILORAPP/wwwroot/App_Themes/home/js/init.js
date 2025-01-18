

    (function () {
   // M.AutoInit();

  //  loadProgressBar();

    $("#btn-signin").on('click', function (e) {
        $(this).addClass("disabled");
        setTimeout(function () {
            showLoader("Login..........");
        }, 100);

        e.preventDefault();
        
        var data = $("#login-form").serialize();

        axios.post('/account/login', data)
            .then(function (response) {
                hideLoader();
                if (response.data.statusCode == 200) {
                    M.toast({ html: 'Login successful!', classes: 'green darken-3' });
                    localStorage.setItem("token", response.data.accessToken);
                    window.location.href = "/dashboard/index";

                }
                else {

                    M.toast({ html: response.data.statusCode });

                }
               
            })
            .catch(function () {
                M.toast({ html: 'Invalid username or password !!' });
                $("#btn-signin").removeClass("disabled");
            });
    });
})
