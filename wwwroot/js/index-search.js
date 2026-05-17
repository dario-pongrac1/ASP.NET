/**
 * Index Search JavaScript
 * Real-time search sa AJAX pozivima
 */

document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput');
    const searchResults = document.getElementById('searchResults');
    const tableContainer = document.getElementById('tableContainer');

    if (!searchInput || !searchResults || !tableContainer) return;

    const endpoint = searchInput.dataset.endpoint;
    let debounceTimer;

    // Input event sa debounce-om
    searchInput.addEventListener('input', function () {
        clearTimeout(debounceTimer);

        const query = this.value.trim();

        // Ako je unos prekratak, prikaži originalnu tabelu
        if (query.length < 2) {
            searchResults.style.display = 'none';
            tableContainer.style.display = 'block';
            tableContainer.style.opacity = '1';
            return;
        }

        // Debounce AJAX poziv (300ms)
        debounceTimer = setTimeout(() => {
            performSearch(endpoint, query, searchResults, tableContainer);
        }, 300);
    });

    /**
     * Fetch search rezultate sa servera
     */
    function performSearch(endpoint, query, resultsContainer, originalTable) {
        // Prikaži loading spinner
        resultsContainer.innerHTML = '<div class="text-center py-3"><span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Pretraga...</div>';
        resultsContainer.style.display = 'block';
        originalTable.style.display = 'none';
        originalTable.style.opacity = '1';

        fetch(`${endpoint}?q=${encodeURIComponent(query)}`)
            .then(response => {
                if (!response.ok) throw new Error('Network response was not ok');
                return response.text();
            })
            .then(html => {
                resultsContainer.innerHTML = html;
                highlightMatches(resultsContainer, query);
                resultsContainer.style.display = 'block';
                originalTable.style.display = 'none';
            })
            .catch(error => {
                console.error('Search error:', error);
                resultsContainer.innerHTML = '<div class="alert alert-danger">Greška pri pretrazi</div>';
                resultsContainer.style.display = 'block';
            });
    }

    function highlightMatches(root, query) {
        const escapedQuery = query.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
        const matcher = new RegExp(`(${escapedQuery})`, 'ig');
        const walker = document.createTreeWalker(root, NodeFilter.SHOW_TEXT, null);
        const textNodes = [];

        while (walker.nextNode()) {
            textNodes.push(walker.currentNode);
        }

        textNodes.forEach(node => {
            const parent = node.parentElement;
            if (!parent || ['SCRIPT', 'STYLE'].includes(parent.tagName) || parent.closest('mark.search-highlight')) {
                return;
            }

            if (!matcher.test(node.nodeValue)) {
                return;
            }

            matcher.lastIndex = 0;
            const wrapper = document.createElement('span');
            wrapper.innerHTML = node.nodeValue.replace(matcher, '<mark class="search-highlight">$1</mark>');
            node.parentNode.replaceChild(wrapper, node);
        });
    }
});
