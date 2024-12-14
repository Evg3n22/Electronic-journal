// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    navigator.geolocation.getCurrentPosition(function (position) {
        var latitude = position.coords.latitude;
        var longitude = position.coords.longitude;

        // Відправляю координати
        fetch(`/weather?latitude=${latitude}&longtude=${longitude}`)
            .then(response => response.json())
            .then(data => {
                // Відобразити дані про погоду на сторінці
                const weatherDisplay = document.getElementById('weather-info');
                const weatherDescription = data.weather[0].main;
                const temperature = data.main.temp;

                weatherDisplay.textContent = `Погода: ${weatherDescription}, Температура: ${temperature}°C`;
            })
            .catch(error => {
                console.error('Error fetching weather:', error);
            });
    });
});