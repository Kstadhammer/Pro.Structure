// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Theme handling
document.addEventListener('DOMContentLoaded', function () {
    const darkModeToggle = document.getElementById('darkModeToggle');
    const html = document.documentElement;
    const icon = darkModeToggle.querySelector('i');
    
    // Load saved theme preference
    const savedTheme = localStorage.getItem('theme') || 'light';
    html.setAttribute('data-bs-theme', savedTheme);
    updateThemeUI(savedTheme === 'dark');

    darkModeToggle.addEventListener('click', function () {
        const isDark = html.getAttribute('data-bs-theme') === 'dark';
        const newTheme = isDark ? 'light' : 'dark';
        
        // Animate icon
        icon.style.transform = 'scale(0)';
        
        setTimeout(() => {
            // Update theme
            html.setAttribute('data-bs-theme', newTheme);
            localStorage.setItem('theme', newTheme);
            updateThemeUI(!isDark);
            
            // Animate icon back
            icon.style.transform = 'scale(1)';
        }, 150);
    });
});

function updateThemeUI(isDark) {
    const icon = document.querySelector('#darkModeToggle i');
    icon.className = isDark ? 'bi bi-sun-fill' : 'bi bi-moon-stars-fill';
    
    const toggle = document.getElementById('darkModeToggle');
    toggle.setAttribute('title', isDark ? 'Switch to light mode' : 'Switch to dark mode');
    
    // Add transition for icon
    icon.style.transition = 'transform 0.3s ease';
}
