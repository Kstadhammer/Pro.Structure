// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Dark mode functionality
document.addEventListener('DOMContentLoaded', () => {
    const darkModeToggle = document.getElementById('darkModeToggle');
    const html = document.documentElement;
    const darkModeIcon = darkModeToggle.querySelector('i');
    
    // Check for saved dark mode preference
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme) {
        html.setAttribute('data-bs-theme', savedTheme);
        updateIcon(savedTheme === 'dark');
    }

    // Toggle dark mode
    darkModeToggle.addEventListener('click', () => {
        const currentTheme = html.getAttribute('data-bs-theme');
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        
        html.setAttribute('data-bs-theme', newTheme);
        localStorage.setItem('theme', newTheme);
        updateIcon(newTheme === 'dark');
    });

    // Update icon based on theme
    function updateIcon(isDark) {
        if (isDark) {
            darkModeIcon.classList.remove('bi-moon-stars');
            darkModeIcon.classList.add('bi-sun');
        } else {
            darkModeIcon.classList.remove('bi-sun');
            darkModeIcon.classList.add('bi-moon-stars');
        }
    }
});
