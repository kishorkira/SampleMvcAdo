
let form = document.forms['user-form'];
form.addEventListener("keypress", function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        //console.log(e.keyCode);
        return false;
    }
});
$('#user-form').validate({
    rules: {
        UserName: {
            required: true,
            email: true
        },
        Password: {
            required: true,
            minlength: 6
        },
        CompanyId: {
            required: true,
        },
        AccountId: {
            required: true,
        },
        LicenseKey: {
            required: true,
            minlength: 12,
            maxlength:12

        },
        MasterId: {
            required: true,
        },
        Address1: {
            required: true,
        },
        Address2: {
            required: true,
        },
        CityCode: {
            required: true,
            minlength: 2,
            maxlength: 3

        },
        RegionCode: {
            required: true,
            minlength: 2,
            maxlength: 4

        },
        CountryCode: {
            required: true,
            minlength: 2,
            maxlength: 4

        },
        PostalCode: {
            required: true,
            minlength: 4,
            maxlength: 6

        }
    },
    messages: {

        UserName: {
            required: "Please enter your email.",
            email: "Your email address must be in the format of name@domain.com"
        },
        Password: {
            required: "Please enter your password.",
            minlength: "Password should be atleast 6 chars.",

        },
        CompanyId: {
            required: "Please select your company.",
        },
        AccountId: {
            required: "Please enter your AccountId.",
        },
        LicenseKey: {
            required: "Please enter your License key.",
            minlength: "Key should be 12 chars.",
            maxlength: "Key should be 12 chars."

        },
        MasterId: {
            required: "Please select Master.",
        },
        Address1: {
            required: "Please enter your Address1.",
        },
        Address2: {
            required: "Please enter your Address2.",
        },
        CityCode: {
            required: "Please enter your City Code.",
            minlength: "City Code should be atleast 2 chars.",
            maxlength: "City Code should be 3 chars."

        },
        RegionCode: {
            required: "Please enter your Region Code.",
            minlength: "Region Code should be atleast 2 chars.",
            maxlength: "Region Code should be 4 chars."

        },
        CountryCode: {
            required: "Please enter your Country Code.",
            minlength: "Country Code should be atleast 2 chars.",
            maxlength: "Country Code should be 4 chars."

        },
        PostalCode: {
            required: "Please enter your Postal Code.",
            minlength: "Postal Code should be atleast 4 chars.",
            maxlength: "Postal Code should be 6 chars."

        }
    }


});

function passwordIconClick(event) {
    let password = document.getElementById("Password");
    password.type = password.type === "password" ? "text" : "password";
    event.target.className = password.type === "password" ? "fas fa-eye-slash" : "fas fa-eye"
}
let status = document.getElementById('status');
let savebtn = document.getElementById('save');
let emailInput = document.getElementById("UserName");

emailInput.addEventListener('focus', function (e) {
    status.innerHTML = "";


});
emailInput.addEventListener('blur', function (e) {

    axios.get('http://localhost:51254/api/user/GetCount',
         {
             params: {
                 username: e.target.value
             }
         })
        .then(function (response) {
            if (response.data > 0) {
                status.innerHTML = "User exist with this email";
                status.style.color = "red";
                savebtn.disabled = true;

            } else if (response.data === 0) {
                status.innerHTML = "";
                savebtn.disabled = false;

            } else {
                status.innerHTML = "Server busy please try later";
                savebtn.disabled = false;
            }

        })
        .catch(function (error) {
            console.log(error);
        });

});
