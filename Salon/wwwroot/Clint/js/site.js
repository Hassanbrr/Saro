document.addEventListener("DOMContentLoaded", function () {
    const backButtonContainer = document.getElementById("backButtonContainer");

    // بررسی URL برای صفحه اصلی
    if (window.location.pathname === "/") {
        backButtonContainer.style.display = "none"; // مخفی کردن دکمه در صفحه اصلی
    } else {
        backButtonContainer.style.display = "block"; // نمایش دکمه در سایر صفحات
    }

    // تنظیم عملکرد دکمه برگشت
    document.getElementById("backButton").addEventListener("click", function () {
        window.history.back(); // برگشت به صفحه قبلی
    });
});