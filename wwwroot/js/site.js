//JS đăng ký

$(document).ready(function () {
    $('#registerForm').submit(function (e) {
        if($('#password_regis').val() != $('#r_password').val()){
            Toast.fire({
                icon: 'error',
                title: 'Mật khẩu không khớp'
            })
            e.preventDefault();
        }

        const email = $('#email').val();
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(email)) {
            Toast.fire({
                icon: 'error',
                title: 'Email không hợp lệ'
            })
            e.preventDefault();
        }
    });
});
