// Function to check if the user prefers dark mode and switch CSS accordingly
function checkAndSetDarkMode() {

    
    if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
        // User prefers dark mode
        document.getElementById('theme-stylesheet').setAttribute('href', '/css/dark.css');
        document.querySelector(".nav").style.backgroundColor = '#302f30';
        ui.innerHTML = "🌝";
        localStorage.setItem("theme", "dark");
    } else {
        // User prefers light mode
        document.getElementById('theme-stylesheet').setAttribute('href', '/css/light.css');
        document.querySelector(".nav").style.backgroundColor = '#950bb8';
        ui.innerHTML = "☀"
        localStorage.setItem("theme", "light");
    }
    localStorage.setItem("run", 1);
}

function getTheme() {
    
    if (ui.innerHTML == "🌝") {
        document.getElementById('theme-stylesheet').setAttribute('href', '/css/light.css');
        document.querySelector(".nav").style.backgroundColor = '#950bb8';
        ui.innerHTML = "☀"
        localStorage.setItem("theme", "light");
    } else {
        document.getElementById('theme-stylesheet').setAttribute('href', '/css/dark.css');
        document.querySelector(".nav").style.backgroundColor = '#302f30';
        ui.innerHTML = "🌝";
        localStorage.setItem("theme", "dark");
    }
    firstRun = 1;
}

function currentTheme() {
    if (localStorage.getItem("theme") == "dark") {
        // User prefers dark mode
        document.getElementById('theme-stylesheet').setAttribute('href', '/css/dark.css');
        document.querySelector(".nav").style.backgroundColor = '#302f30';
        ui.innerHTML = "🌝";
        localStorage.setItem("theme", "dark");
    } else {
        document.getElementById('theme-stylesheet').setAttribute('href', '/css/light.css');
        document.querySelector(".nav").style.backgroundColor = '#950bb8';
        ui.innerHTML = "☀"
        localStorage.setItem("theme", "light");
    }
}

var ui = document.querySelector("#ui");
console.log(ui);
console.log(localStorage.getItem("theme"));
//localStorage.clear();
var data = localStorage.getItem("run");
//Call the function to set the initial mode when the page content has loaded

if (data!=1) {
    document.addEventListener('DOMContentLoaded', checkAndSetDarkMode);
}
document.addEventListener('DOMContentLoaded', currentTheme);
document.querySelector("#ui").addEventListener("click", getTheme);

