/**
 * Hide an element after a delay
 * @param {HTMLElement} element The element to hide
 * @param {int} delay The delay in ms before hiding
 */
function hideAfterDelay(element, delay) {
    setTimeout(() => {
        element.style = "opacity: 0";
    }, delay);
}

/**
 * Show an element after a delay
 * @param {HTMLElement} element The element to show
 * @param {int} delay The delay in ms before showing
 */
 function showAfterDelay(element, delay) {
    setTimeout(() => {
        element.style = "opacity: 100";
    }, delay);
}

/**
 * Scroll back to the top of the page
 */
function resetScroll() {
    setTimeout(() => window.scrollTo(0, 0), 100);
}

/**
 * Automatically resize on height change
 * @param {HTMLElement} element 
 */
function autoSetHeight(element) {
    if (element) {
        element.style.height = '';
        element.style.height = element.scrollHeight + 'px';
    }
}

/**
 * Reload the page
 */
function reload() {
    location.reload();
}

/**
 * Show a shadow underneath the navigation bar when
 * the user begins scrolling
 */
document.addEventListener('scroll', () => {
    var nav = document.getElementsByTagName('nav')[0];
    if (window.scrollY) {
        nav.classList.add('nav-active');
    } else {
        nav.classList.remove('nav-active');
    }
});
