function setTheme(theme) {
    console.log(localStorage.getItem("theme"));
    
        const themeStylesheet = document.getElementById('theme-stylesheet');
        const nav = document.querySelector(".nav");
    const ui = document.querySelector("#ui");
    console.log(themeStylesheet.getAttribute('href'));
    if (!themeStylesheet.getAttribute('href').includes('/css/dark.css')) {
        themeStylesheet.setAttribute('href', `/css/${theme}.css`);
        nav.style.backgroundColor = theme === 'dark' ? '#302f30' : '#950bb8';
        ui.innerHTML = theme === 'dark' ? "🌝" : "☀";
        localStorage.setItem("theme", theme);
    }
}

function toggleTheme() {
    const currentTheme = localStorage.getItem("theme") === 'dark' ? 'light' : 'dark';
    setTheme(currentTheme);
}

function initializeTheme() {
    const userPrefersDark = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    const theme = localStorage.getItem("theme") || (userPrefersDark ? 'dark' : 'light');
    setTheme(theme);
}

document.addEventListener('DOMContentLoaded', initializeTheme);
document.querySelector("#ui").addEventListener("click", toggleTheme);
