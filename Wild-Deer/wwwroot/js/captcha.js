// Function to generate random captcha
function generateCaptcha() {
    var captcha = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    for (var i = 0; i < 6; i++) {
        captcha += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return captcha;
}

// Function to display captcha
function displayCaptcha() {
    var captchaElement = document.getElementById('captcha');
    var captcha = generateCaptcha();
    captchaElement.textContent = captcha;
}

// Function to validate captcha
function validateCaptcha() {
    var userInput = document.getElementById('userInput').value;
    var captcha = document.getElementById('captcha').textContent;

    if (userInput === captcha) {
        document.getElementById('result').textContent = "Captcha validated successfully!";
        document.getElementById('result').style.color = 'green';
    } else {
        document.getElementById('result').textContent = "Invalid captcha, please try again.";
        document.getElementById('result').style.color = 'red';
        // Refresh captcha
        displayCaptcha();
    }
}

// Initialize captcha on page load
window.onload = function () {
    displayCaptcha();
};




var password = document.getElementById("password")
    , confirm_password = document.getElementById("confirm_password");

function validatePassword() {
    if (password.value != confirm_password.value) {
        confirm_password.setCustomValidity("Passwords Don't Match");
    } else {
        confirm_password.setCustomValidity('');
    }
}

password.onchange = validatePassword;
confirm_password.onkeyup = validatePassword;