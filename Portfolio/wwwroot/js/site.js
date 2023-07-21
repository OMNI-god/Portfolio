// Function to check if the user prefers dark mode and switch CSS accordingly
function checkAndSetDarkMode() {


    if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
        // User prefers dark mode
        document.getElementById('theme-stylesheet').setAttribute('href', '/css/dark.css');
        document.querySelector(".nav").style.backgroundColor = '#302f30';
        
    } else {
        // User prefers light mode
        document.getElementById('theme-stylesheet').setAttribute('href', '/css/light.css');
        document.querySelector(".nav").style.backgroundColor = '#950bb8';
       
    }
}
 //Call the function to set the initial mode when the page content has loaded
document.addEventListener('DOMContentLoaded', checkAndSetDarkMode);
